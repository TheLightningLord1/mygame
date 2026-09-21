using System;
using System.Collections.Generic;
using System.Linq;
using Ruletka.Models;
using Ruletka.Systems;
using Ruletka.Combat;
using Ruletka.Data;

namespace Ruletka;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Hero myHero = null!;
        Inventory playerInventory = null!;

        // Проверка файла сохранения при старте
        if (SaveManager.SaveExists())
        {
            Console.WriteLine("=== ОБНАРУЖЕНО СОХРАНЕНИЕ ===");
            Console.WriteLine("1. Загрузить сохранённую игру");
            Console.WriteLine("2. Начать новую игру");
            Console.Write("Выбор: ");

            if (Console.ReadLine() == "1")
            {
                var (loadedHero, loadedInventory) = SaveManager.LoadGame();
                if (loadedHero != null && loadedInventory != null)
                {
                    myHero = loadedHero;
                    playerInventory = loadedInventory;
                    Console.WriteLine($"\nС возвращением, {myHero.Name}!");
                    Console.ReadKey();
                }
            }
        }

        if (myHero == null || playerInventory == null)
        {
            StartPrologue(out myHero, out playerInventory);
        }

        GameLoop(myHero, playerInventory);
    }

    static void StartPrologue(out Hero hero, out Inventory inventory)
    {
        inventory = new Inventory();
        
        Console.WriteLine("=== ПРОЛОГ: ПРИШЕСТВИЕ ===");
        Console.WriteLine("Солнце медленно садится за горизонт старого разрушенного города.");
        Console.ReadKey();
        Console.WriteLine("Холодный ветер гуляет по пустым улицам, а вдалеке слышен скрежет когтей...");
        Console.ReadKey();
        Console.WriteLine("Вы просыпаетесь в этом голом центральном парке.");
        Console.ReadKey();
        Console.WriteLine("Здесь словно совсем недавно была жизнь.");
        Console.ReadKey();
        Console.WriteLine("К вам из-за спины подходит странствующий дедушка и прикасается к плечу.");
        Console.ReadKey();
        Console.WriteLine("- Редко здесь встретишь людей.\n - Обычно только всякая нечесть ходит по городу.\n -Как тебя зовут путник?");
        Console.ReadKey();
        Console.Write("Введите имя вашего героя: ");
        string playerName = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "Странник";
        }

        Console.WriteLine($"\n- Интересное имя {playerName}!\n - На наших землях таких имён я не слыхал");
        Console.ReadKey();
        Console.WriteLine($"{playerName} - ч-что это за место?");
        Console.ReadKey();
        Console.WriteLine($"\n- Заброшенный город, ранее столица страны. С лушай а ты не простой путник. Посмотри на себя ты...");
        Console.ReadKey();

        while (true)
        {
            Console.WriteLine("\nВыберите ваш класс персонажа:");
            Console.WriteLine("1. Рыцарь: средний урон, высокий крит, средняя скорость атаки");
            Console.WriteLine("2. Паладин: высокий урон, малая скорость атаки");
            Console.WriteLine("3. Маг: малый урон, малая скорость атаки, усиленный магический урон");
            Console.WriteLine("4. Лучник: малый урон, высокая скорость атаки, увеличенный крит");
            string choice = Console.ReadLine()!;

            if (choice == "1")
            {
                hero = new Hero("Неудержимый мечник", HeroClass.Knight, 100, 100, 0, 0, 50, 12, 40, 10, 0, 0);
                break;
            }
            else if (choice == "2")
            {
                hero = new Hero("Святой Паладин", HeroClass.Paladin, 160, 160, 0, 0, 50, 23, 5, 3, 10, 0);
                break;
            }
            else if (choice == "3")
            {
                hero = new Hero("Мэрлин", HeroClass.Mage, 70, 70, 0, 0, 50, 5, 0, 5, 0, 0);
                break;
            }
            else if (choice == "4")
            {
                hero = new Hero("Меткий Эльф", HeroClass.Archer, 70, 70, 0, 0, 50, 7, 38, 18, 0, 0);
                break;
            }
            else
            {
                Console.WriteLine("Неверный выбор! Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        Console.WriteLine($"- Ты же {hero.Class}! Героев стало гораздо меньше.");
        Console.ReadKey();
        Console.WriteLine($"- Тебе нельзя долго находится здесь {playerName}! Отправляйся через главную улицу к конюшне.");
        Console.ReadKey();
        Console.WriteLine("- Держи меч, он поможет тебе в пути!");
        Console.ReadKey();
        Console.WriteLine("Вы берёте меч. Оглядываетесь по сторонам, но путника уже нету.");
        Console.ReadKey();

        Sword startingSword = new Sword(100, "старинный меч", ItemType.Sword, Rarity.Common, 62.7, 15, 0, 0, 0);
        inventory.AddItem(startingSword);
        Console.WriteLine($"\n⚔️ Вы получили стартовое снаряжение: {startingSword.Name}!");
        Console.WriteLine("Отправляйтесь к конюху, пробиваясь сквозь монстров!");
        Console.WriteLine("Для начала наденьте меч в инвентаре.");
        Console.ReadKey();
    }

    static void GameLoop(Hero hero, Inventory inventory)
    {
        bool inPrologue = true;
        
        while (inPrologue)
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("1. Инвентарь и экипировка");
            Console.WriteLine("2. Идти в бой");
            Console.WriteLine("3. 💾 Сохранить игру");
            Console.WriteLine("4. Выйти в главное меню");
            Console.Write("Выбор: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 1) 
                    inventory.OpenMenu(hero);
                else if (choice == 2) 
                    BattleSystem.StartBattle(hero, inventory);
                else if (choice == 3) 
                    SaveManager.SaveGame(hero, inventory);
                else if (choice == 4) 
                    inPrologue = false;
                else 
                    Console.WriteLine("Неверный выбор!");
            }
        }

        while (true)
        {
            Console.WriteLine($"\nЗолото: {inventory.Gold} 🪙");
            Console.WriteLine("=== ГЛАВНОЕ МЕНЮ ===");
            Console.WriteLine("1. 🎁 Открыть сундуки");
            Console.WriteLine("2. 🎒 Инвентарь и экипировка");
            Console.WriteLine("3. ⚔️ В бой!");
            Console.WriteLine("4. 🏪 Торговая лавка");
            Console.WriteLine("5. 📖 Правила игры");
            Console.WriteLine("6. 💾 Сохранить игру");
            Console.WriteLine("7. Выход");

            if (int.TryParse(Console.ReadLine(), out int spinChoice))
            {
                if (spinChoice == 1)
                    OpenChestsMenu(inventory);
                else if (spinChoice == 2)
                    inventory.OpenMenu(hero);
                else if (spinChoice == 3)
                    BattleSystem.StartBattle(hero, inventory);
                else if (spinChoice == 4)
                    ShopMenu(hero, inventory);
                else if (spinChoice == 5)
                    ShowRules();
                else if (spinChoice == 6)
                    SaveManager.SaveGame(hero, inventory);
                else if (spinChoice == 7)
                    break;
            }
        }
    }

    static void OpenChestsMenu(Inventory inventory)
    {
        Console.WriteLine("Выберите количество открытий:");
        Console.WriteLine("1. 1 открытие");
        Console.WriteLine("2. 10 открытий");
        Console.WriteLine("3. Выход");

        if (int.TryParse(Console.ReadLine(), out int spin))
        {
            if (spin == 1)
                SingleChest(inventory);
            else if (spin == 2)
                TenChests(inventory);
        }
    }

    static void SingleChest(Inventory inventory)
    {
        Console.Write("Выберите кейс:\\n 1. Мечи(1000🪙)\\n 2. Луки(1000🪙)\\n 3. Посохи(1000🪙)\\n 4. Щиты(1000🪙)\\n 5. Броня(1500🪙)\\n 6. Амулеты(1000🪙)\\n 7. Выход\\nВыбор: ");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            int cost = choice == 5 ? 1500 : 1000;
            if (choice >= 1 && choice <= 6 && inventory.Gold >= cost)
            {
                inventory.Gold -= cost;
                List<Item> itemsList = choice switch
                {
                    1 => ItemDatabase.GetSwords(),
                    2 => ItemDatabase.GetArcher(),
                    3 => ItemDatabase.GetStaff(),
                    4 => ItemDatabase.GetShield(),
                    5 => ItemDatabase.GetArmor(),
                    6 => ItemDatabase.GetAmulet(),
                    _ => new List<Item>()
                };

                Roulette roulette = new Roulette();
                Item droppedItem = roulette.SingleSpin(itemsList);
                inventory.AddItem(droppedItem);
            }
            else if (choice != 7)
            {
                Console.WriteLine("Недостаточно золота!");
            }
        }
    }

    static void TenChests(Inventory inventory)
    {
        Console.Write("Выберите кейс:\\n 1. Мечи(10000🪙)\\n 2. Луки(10000🪙)\\n 3. Посохи(10000🪙)\\n 4. Щиты(10000🪙)\\n 5. Броня(15000🪙)\\n 6. Амулеты(10000🪙)\\n 7. Выход\\nВыбор: ");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            int cost = choice == 5 ? 15000 : 10000;
            if (choice >= 1 && choice <= 6 && inventory.Gold >= cost)
            {
                inventory.Gold -= cost;
                List<Item> itemsList = choice switch
                {
                    1 => ItemDatabase.GetSwords(),
                    2 => ItemDatabase.GetArcher(),
                    3 => ItemDatabase.GetStaff(),
                    4 => ItemDatabase.GetShield(),
                    5 => ItemDatabase.GetArmor(),
                    6 => ItemDatabase.GetAmulet(),
                    _ => new List<Item>()
                };

                Roulette roulette = new Roulette();
                List<Item> tenItems = roulette.TestSpin(itemsList);
                inventory.AddItems(tenItems);
            }
            else if (choice != 7)
            {
                Console.WriteLine("Недостаточно золота!");
            }
        }
    }

    static void ShopMenu(Hero hero, Inventory inventory)
    {
        Console.WriteLine("=== ТОРГОВАЯ ЛАВКА ===");
        Console.WriteLine("Функционал в разработке...");
        Console.ReadKey();
    }

    static void ShowRules()
    {
        Console.Clear();
        Console.WriteLine("=== ПРАВИЛА ИГРЫ ===\n");
        Console.WriteLine("1. Выбирайте класс персонажа в начале игры");
        Console.WriteLine("2. Экипируйте предметы для увеличения характеристик");
        Console.WriteLine("3. Сражайтесь с монстрами, получайте золото и опыт");
        Console.WriteLine("4. Следите за прочностью предметов и ремонтируйте их");
        Console.WriteLine("5. Используйте комбо-атаки для увеличения урона");
        Console.WriteLine("6. Экспериментируйте со стихиями против врагов\n");
        Console.ReadKey();
    }
}
