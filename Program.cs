using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

[JsonDerivedType(typeof(Sword), "sword")]
[JsonDerivedType(typeof(Staff), "staff")]
[JsonDerivedType(typeof(Shield), "shield")]
[JsonDerivedType(typeof(Archer), "archer")]
[JsonDerivedType(typeof(Armor), "armor")]
[JsonDerivedType(typeof(Amulet), "amulet")]
[JsonDerivedType(typeof(Consumable), "consumable")]
public class SaveData
{
    public Hero Hero { get; set; } = null!;
    public Inventory Inventory { get; set; } = null!;
}

public static class SaveManager
{
    private const string SaveFilePath = "save.json";

    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        WriteIndented = true,
        IncludeFields = true,
        PropertyNameCaseInsensitive = true
    };

    public static void SaveGame(Hero hero, Inventory inventory)
    {
        try
        {
            var saveData = new SaveData
            {
                Hero = hero,
                Inventory = inventory
            };

            string json = JsonSerializer.Serialize(saveData, Options);
            File.WriteAllText(SaveFilePath, json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n💾 Прогресс успешно сохранён в save.json!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Ошибка при сохранении: {ex.Message}");
            Console.ResetColor();
        }
    }

    public static (Hero? hero, Inventory? inventory) LoadGame()
    {
        if (!File.Exists(SaveFilePath))
            return (null, null);

        try
        {
            string json = File.ReadAllText(SaveFilePath);
            var saveData = JsonSerializer.Deserialize<SaveData>(json, Options);
            return (saveData?.Hero, saveData?.Inventory);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Ошибка чтения сохранения: {ex.Message}");
            Console.ResetColor();
            return (null, null);
        }
    }

    public static bool SaveExists() => File.Exists(SaveFilePath);
}
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
        // ... (ваш старый код пролога и выбора класса)
        playerInventory = new Inventory();
        // ...
    }

Console.WriteLine("=== ПРОЛОГ: ПРИШЕСТВИЕ ===");
Console.WriteLine("Солнце медленно садится за горизонт старого разрушенного города.");
Console.ReadLine();
Console.WriteLine("Холодный ветер гуляет по пустым улицам, а вдалеке слышен скрежет когтей...");
Console.ReadLine();
Console.WriteLine("Вы просыпаетесь в этом голом центральном парке.");
Console.ReadLine();
Console.WriteLine("Здесь словно совсем недавно была жизнь.");
Console.ReadLine();
Console.WriteLine("К вам из-за спины подходит странствующий дедушка и прикасается к плечу.");
Console.ReadLine();
Console.WriteLine("- Редко здесь встретишь людей.\n - Обычно только всякая нечесть ходит по городу.\n -Как тебя зовут путник?");
Console.ReadLine();
Console.Write("Введите имя вашего героя: ");
string playerName = Console.ReadLine()!;

// Если игрок ничего не ввел, дадим ему имя по умолчанию
if (string.IsNullOrWhiteSpace(playerName))
{
    playerName = "Странник";
}

Console.WriteLine($"\n- Интересное имя {playerName}!\n - На наших землях таких имён я не слыхал");
Console.ReadLine();
Console.WriteLine($"{playerName} - ч-что это за место?");
Console.ReadLine();
Console.WriteLine($"\n- Заброшенный город, ранее столица страны. С лушай а ты не простой путник. Посмотри на себя ты...");
Console.ReadKey();
        while (myHero == null)
        {
            Console.WriteLine("Выберите ваш класс персонажа");
            Console.WriteLine("1. Рыцарь: имеет средний урон, высокий крит урон и средную скорость атаки");
            Console.WriteLine("2. Паладин: имеет высокий урон и малую скорость атаки");
            Console.WriteLine("3. Маг: имеет малый урон и малую скорость атаки, но усиленный магический урон");
            Console.WriteLine("4. Лучник: имеет малый урон и высокую скорость атаки, крит урон увеличен");
            string choice = Console.ReadLine()!;
            if (choice == "1")
            {
                myHero = new Hero("Неудержимый мечник", HeroClass.Knight, 100, 100, 0, 0, 50, 12, 40, 10, 0, 0);
            }
            else if (choice == "2")
            {
                myHero = new Hero("Святой Паладин", HeroClass.Paladin, 160, 160, 0, 0, 50, 23, 5, 3, 10, 0);
            }
            else if (choice == "3")
            {
                myHero = new Hero("Мэрлин", HeroClass.Mage, 70, 70, 0, 0, 50, 5, 0, 5, 0, 0);
            }
            else if (choice == "4")
            {
                myHero = new Hero("Меткий Эльф", HeroClass.Archer, 70, 70, 0, 0, 50, 7, 38, 18, 0, 0);
            }
            else
            {
                Console.WriteLine("Неверный выбор! Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
        
        Console.WriteLine($"- Ты же {myHero}! Героев стало гораздо меньше, если они вообще остались. Давно не был за пределами страны.");
        Console.ReadLine();
        Console.WriteLine($"- Тебе нельзя долго находится здесь {playerName}! Отправляйся через главную улицу, пока не дойдёшь до конющни. Конюху скажи что тебе нужно в соседний жилой город.");
        Console.ReadLine();
Console.WriteLine("- Держи меч он поможет тебе в пути!");
Console.ReadLine();
Console.WriteLine("Вы беретё меч. Олядываетесь по сторонам но путника уже нету.");
Console.ReadLine();

if (playerInventory == null)
{
    playerInventory = new Inventory();
}

Sword startingSword = new Sword(100, "стариный меч", ItemType.Sword, Rarity.Common, 62.7, 15, 0, 0, 0, 0);

    // 2. Добавляем в инвентарь и сразу экипируем
    playerInventory.AddItem(startingSword);

    Console.WriteLine($"\n⚔️ Вы получили стартовое снаряжение: {startingSword.Name}!");
   
Console.WriteLine("Отправляйтесь к конюху, пробиваясь сквозь монстров!");
Console.ReadLine();
Console.WriteLine("Для начала наденьте меч в инвентаре. Откройте инвентарь и выберите нужный предмет");
Console.ReadKey();
        bool inPrologue = true;
while (inPrologue)
{
    Console.WriteLine("\n1. Инвентарь и экипировка");
    Console.WriteLine("2. Идти к конюшне");
    Console.WriteLine("3. 💾 Сохранить игру");
    Console.WriteLine("4. Перейти в главное меню");
    Console.Write("Выбор: ");

    if (int.TryParse(Console.ReadLine(), out int Choice))
    {
        if (Choice == 1) playerInventory.OpenMenu(myHero);
        else if (Choice == 2) Arena.StartBattle(myHero, playerInventory);
        else if (Choice == 3) SaveManager.SaveGame(myHero, playerInventory);
        else if (Choice == 4) inPrologue = false;
        else Console.WriteLine("Неверный выбор!");
    }
}
        while (true)
        {
            Console.WriteLine($"\nЗолото: {playerInventory.Gold} 🪙");
            Console.WriteLine("Главное меню.");
            Console.WriteLine("1. сундуки");
            Console.WriteLine("2. Инвентарь и экипировка");
            Console.WriteLine("3. Выйти в бой");
            Console.WriteLine("4. ⛺ Торговая лавка (Купить/Продать)");
            Console.WriteLine("5. 📖 Правила игры и подсказки");
            Console.WriteLine("6. 💾 Сохранить игру");
            Console.WriteLine("7. Выход");
        
            if (int.TryParse(Console.ReadLine(), out int spinChoice))
            {     
                if (spinChoice == 1)
                {
                    Console.WriteLine("Выберите количество сундуков:");
                    Console.WriteLine("1. 1 открытие сундука");
                    Console.WriteLine("2. 10 открытий сундука");
                    Console.WriteLine("3. Выход");

                    if (int.TryParse(Console.ReadLine(), out int spin))
                    {
                        if (spin == 1)
                    {
                        Console.Write("Выберите кейс для прокрутки\n 1. Мечи(1000🪙).\n 2. Луки(1000🪙).\n 3. Посохи(1000🪙).\n 4. Щиты(1000🪙).\n 5. Броня(1500🪙).\n 6. Амулеты(1000🪙).\n 7. Выход.\n");

                    if (int.TryParse(Console.ReadLine(), out int singleChoice))
                    {
                        if (singleChoice == 1 && playerInventory.Gold >= 1000)
                        {
                            playerInventory.Gold -= 1000;
                            List<Item> ItemsList = ItemDatabase.GetSwords();
                            Roulette roulette = new Roulette();
                            Item droppedItem = roulette.SingleSpin(ItemsList);
                            playerInventory.AddItem(droppedItem);
                        }
                        else if (singleChoice == 2 && playerInventory.Gold >= 1000)
                        {
                            playerInventory.Gold -= 1000;
                            List<Item> ItemsList = ItemDatabase.GetArcher();
                            Roulette roulette = new Roulette();
                            Item droppedItem = roulette.SingleSpin(ItemsList);
                            playerInventory.AddItem(droppedItem); 
                        }
                        else if (singleChoice == 3 && playerInventory.Gold >= 1000)
                        {
                            playerInventory.Gold -= 1000;
                            List<Item> ItemsList = ItemDatabase.GetStaff();
                            Roulette roulette = new Roulette();
                            Item droppedItem = roulette.SingleSpin(ItemsList);
                            playerInventory.AddItem(droppedItem); 
                        }
                        else if (singleChoice == 4 && playerInventory.Gold >= 1000)
                        {
                            playerInventory.Gold -= 1000;
                            List<Item> ItemsList = ItemDatabase.GetShield();
                            Roulette roulette = new Roulette();
                            Item droppedItem = roulette.SingleSpin(ItemsList);
                            playerInventory.AddItem(droppedItem); 
                        }
                        else if(singleChoice == 5 && playerInventory.Gold >= 1500)
                        {
                            playerInventory.Gold -= 1500;
                            List<Item> ItemsList = ItemDatabase.GetArmor();
                            Roulette roulette = new Roulette();
                            Item droppedItem = roulette.SingleSpin(ItemsList);
                            playerInventory.AddItem(droppedItem);
                        }
                        else if(singleChoice == 6 && playerInventory.Gold >= 1000)
                        {
                            playerInventory.Gold -= 1000;
                            List<Item> ItemsList = ItemDatabase.GetAmulet();
                            Roulette roulette = new Roulette();
                            Item droppedItem = roulette.SingleSpin(ItemsList);
                            playerInventory.AddItem(droppedItem); 
                        }
                        else if (singleChoice == 7)
                        {
                            continue;
                        }
                        else
                        {
                            Console.Write("Недостаточно золота");
                        }
                    }
                    }
                    if (spin == 2)
                        {
                            Console.Write("Выберите кейс для прокрутки\n 1. Мечи(10000🪙).\n 2. Луки(10000🪙).\n 3. Посохи(10000🪙).\n 4. Щиты(10000🪙).\n 5. Броня(15000🪙).\n 6. Амулеты(10000🪙).\n 7. Выход.\n");

                    if (int.TryParse(Console.ReadLine(), out int userChoice))
                    {
                        if (userChoice == 1 && playerInventory.Gold >= 10000)
                        {
                            playerInventory.Gold -= 10000;
                            List<Item> ItemsList = ItemDatabase.GetSwords();
                            Roulette roulette = new Roulette();
                            List<Item> tenItems = roulette.TestSpin(ItemsList);
                            playerInventory.AddItems(tenItems);
                        }
                        else if (userChoice == 2 && playerInventory.Gold >= 10000)
                        {
                            playerInventory.Gold -= 10000;
                            List<Item> ItemsList = ItemDatabase.GetArcher();
                            Roulette roulette = new Roulette();
                            List<Item> tenItems = roulette.TestSpin(ItemsList);
                            playerInventory.AddItems(tenItems);
                        }
                        else if (userChoice == 3 && playerInventory.Gold >= 10000)
                        {
                            playerInventory.Gold -= 10000;
                            List<Item> ItemsList = ItemDatabase.GetStaff();
                            Roulette roulette = new Roulette();
                            List<Item> tenItems = roulette.TestSpin(ItemsList);
                            playerInventory.AddItems(tenItems); 
                        }
                        else if (userChoice == 4 && playerInventory.Gold >= 10000)
                        {
                            playerInventory.Gold -= 10000;
                            List<Item> ItemsList = ItemDatabase.GetShield();
                            Roulette roulette = new Roulette();
                            List<Item> tenItems = roulette.TestSpin(ItemsList);
                            playerInventory.AddItems(tenItems); 
                        }
                        else if(userChoice == 5 && playerInventory.Gold >= 15000)
                        {
                            playerInventory.Gold -= 15000;
                            List<Item> ItemsList = ItemDatabase.GetArmor();
                            Roulette roulette = new Roulette();
                            List<Item> tenItems = roulette.TestSpin(ItemsList);
                            playerInventory.AddItems(tenItems);
                        }
                        else if(userChoice == 6 && playerInventory.Gold >= 10000)
                        {
                            playerInventory.Gold -= 10000;
                            List<Item> ItemsList = ItemDatabase.GetAmulet();
                            Roulette roulette = new Roulette();
                            List<Item> tenItems = roulette.TestSpin(ItemsList);
                            playerInventory.AddItems(tenItems);
                        }
                        else if (userChoice == 7)
                        {
                            continue;
                        }
                        else
                        {
                            Console.Write("Недостаточно золота");
                        }
                    }
                        }
                    else if (spin == 3)
                        {
                            continue;
                        }
                    }                  
                }
                else if (spinChoice == 2)
                {
                    playerInventory.OpenMenu(myHero);
                }
                else if (spinChoice == 3)
                {
                    Arena.StartBattle(myHero, playerInventory);
                }
                else if (spinChoice == 4)
                {
                    Merchant.OpenShop(playerInventory); // Вызов лавки из главного меню
                }
                else if (spinChoice == 6)
            {
                SaveManager.SaveGame(myHero, playerInventory);
                Console.ReadKey();
            }
            else if (spinChoice == 7)
            {
                SaveManager.SaveGame(myHero, playerInventory);
                Console.WriteLine("Выход из программы...");
                break;
            }
                else if (spinChoice == 5)
                {
                    ShowHelp();
                }
                else
                {
                    Console.WriteLine("Неверный выбор!");
                }
            }
        }
    }
    static void ShowHelp()
    {
        Console.Clear();
        Console.WriteLine("=== 📖 РУКОВОДСТВО ДЛЯ НОВИЧКОВ ===");
        
        Console.WriteLine("\n🗡️ ОСНОВНАЯ ЦЕЛЬ:");
        Console.WriteLine("Побеждайте монстров на Арене, чтобы заработать золото и опыт. Золото тратится на рулетку, где можно выбить мощную экипировку.");
        
        Console.WriteLine("\n🎒 ЭКИПИРОВКА И КЛАССЫ:");
        Console.WriteLine("- Выбитые предметы сначала попадают в инвентарь (пункт 3). Их нужно обязательно НАДЕТЬ.");
        Console.WriteLine("- Учитывайте ваш класс: Паладин не стреляет из лука, а Магу нужны посохи для стихийного урона.");
        Console.WriteLine("- Щиты могут носить только Мечник и Паладин.");
        
        Console.WriteLine("\n🛡️ СЕТЫ БРОНИ:");
        Console.WriteLine("- Если собрать и надеть 4 части брони из одного комплекта, вы получите скрытый бонус к урону, защите или криту!");
        
        Console.WriteLine("\n💰 ЭКОНОМИКА:");
        Console.WriteLine("- Ненужные вещи теперь можно продать в Торговой лавке (пункт 5 главного меню).");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n⚠️ ВАЖНО: ХАРДКОРНЫЙ РЕЖИМ!");
        Console.WriteLine("- Внимательно следите за здоровьем (HP). Если оно упадет до нуля ИЛИ вы решите сбежать с поля боя...");
        Console.WriteLine("- ВЫ ПОТЕРЯЕТЕ ВСЮ НАДЕТУЮ НА ВАС ЭКИПИРОВКУ НАВСЕГДА!");
        Console.WriteLine("- Предметы, лежащие в инвентаре (в мешке), при этом останутся в безопасности.");
        Console.ResetColor();

        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
        Console.ReadKey();
        Console.Clear();
    }
}

public enum Rarity
{
    Common,      
    Rare,        
    Mythical,    
    Magical,     
    Legendary,   
    Unknown      
}

public enum ItemType
{
    Sword,
    Staff,
    Armor,
    Amulet,
    Shield,
    Archer,
    Consumable
}

public class Item
{
    public int Price { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }   
    public Rarity Rarity { get; set; }
    public double Weight { get; set; }
    public double Damage { get; set; }
    public double CreatDamage { get; set; }
    public int Protection { get; set; }
    public int MaxDurability { get; set; } = 100;
    public int CurrentDurability { get; set; } = 100;
    public bool IsBroken => CurrentDurability <= 0;

    public Item(int price, string name, ItemType type, Rarity rarity, double weight, double damage, double creatDamage, int protection)
    {
        Price = price;
        Name = name;
        Type = type;
        Rarity = rarity;
        Weight = weight;
        Damage = damage;
        CreatDamage = creatDamage;
        Protection = protection;
    }

    public virtual void PrintInfo()
    {
        SetColor();
        string status = IsBroken ? " ❌ [СЛОМАНО!]" : $" | 🛠️ {CurrentDurability}/{MaxDurability}";
        Console.Write($"[{RarityRu}] {Name}{status}");
    }
public int DurabilityLossPerBattle => Rarity switch
    {
        Rarity.Common => 20,
        Rarity.Rare => 15,
        Rarity.Mythical => 12,
        Rarity.Magical => 10,
        Rarity.Legendary => 7,
        Rarity.Unknown => 5,
        _ => 15
    };

    // Расчет стоимости ремонта для оттока золота
    public int RepairCost
    {
        get
        {
            if (CurrentDurability >= MaxDurability) return 0;
            double missingRatio = 1.0 - ((double)CurrentDurability / MaxDurability);
            int cost = (int)Math.Ceiling(missingRatio * Price * 0.4);
            return Math.Max(15, cost);
        }
    }
    public void SetColor()
    {
        Console.ForegroundColor = Rarity switch
        {
            Rarity.Common => ConsoleColor.Gray,
            Rarity.Rare => ConsoleColor.Blue,
            Rarity.Mythical => ConsoleColor.Magenta,
            Rarity.Magical => ConsoleColor.Red,
            Rarity.Legendary => ConsoleColor.DarkYellow,
            Rarity.Unknown => ConsoleColor.Black,
            _ => ConsoleColor.White
        };
    }

    public string RarityRu => Rarity switch
    {
        Rarity.Common => "Обычный",
        Rarity.Rare => "Редкий",
        Rarity.Mythical => "Мифический",
        Rarity.Magical => "Магический",
        Rarity.Legendary => "Легендарный",
        Rarity.Unknown => "Неизвестно",
        _ => "Не определено"
    };
}

public class Sword : Item
{
    public int Fire { get; set; }
    public int PassiveDebuff { get; set; }
    public Sword(int price, string name, ItemType type, Rarity rarity, double weight, double damage, double creatDamage, int protection, int fire, int passivedebuff) : base(price, name, type, rarity, weight, damage, creatDamage, protection)
    {
        Fire = fire;
        PassiveDebuff = passivedebuff;
    }
    
    public override void PrintInfo()
    {
        base.PrintInfo(); 
        Console.WriteLine($"⚔️ Выпал меч: {Name} [{RarityRu}] | Урон: {Damage} | Крит: {CreatDamage}");
        Console.ResetColor();
    }
}

class Roulette
{
    private Random random = new Random();
    public Item Spin(List<Item> weapons)
    {
        double totalWeight = 0;
        foreach(Item weapon in weapons)
        {
           totalWeight += weapon.Weight; 
        }
        
        double rolledNumber = totalWeight * random.NextDouble();
        
        foreach(Item Items in weapons)
        {
            rolledNumber -= Items.Weight;
            if (rolledNumber <= 0)
            {
                return Items;
            }
        }
        return weapons[0];
    }

    public Item SingleSpin(List<Item> weapons)
    {
        Item droppedItems = Spin(weapons); 
        droppedItems.PrintInfo();
        return droppedItems;
    }

    public List<Item> TestSpin(List<Item> weapons)
    {
        List<Item> droppedList = new List<Item>(); 
        for (int i = 0; i < 10; i++)
        {
            Item droppedItems = Spin(weapons);
            droppedItems.PrintInfo();
            droppedList.Add(droppedItems); 
        }
        return droppedList; 
    }
}

public class Staff : Item
{
    public int[] Elements { get; set; } 
    public enum Element
    {
        Water,   
        Fire,    
        Ice,     
        Light,   
        Venom,   
        BlackVoid 
    }
    public int PassiveDebuff { get; set; }
    public Staff(int price, string name, ItemType type, Rarity rarity, double weight, double damage, double creatDamage, int protection, int[] elements, int passivedebuff) : base(price, name, type, rarity, weight, damage, creatDamage, protection)
    {
        Elements = elements;
        PassiveDebuff = passivedebuff;
    }

    public string ElementRu(Element element) => element switch
    {
        Element.Water => "Вода",
        Element.Fire => "Огонь",
        Element.Ice => "Лёд",
        Element.Light => "Молния",
        Element.Venom => "Яд",
        Element.BlackVoid => "Чёрная пустота",
        _ => "Неизвестно"
    };

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"🔮 Выпал посох: {Name} [{RarityRu}] | Урон: {Damage} | Крит: {CreatDamage}");
        for (int i = 0; i < Elements.Length; i++)
        {
            if (Elements[i] > 0) 
            {
                Console.WriteLine($"   {ElementRu((Element)i)}: {Elements[i]}");
            }
        }
        Console.ResetColor();
    }
}

public class Shield : Item
{
    public int Ice { get; set; }
    public int Reflection { get; set; }
    public int PassiveDebuff { get; set; }
    public Shield(int price, string name, ItemType type, Rarity rarity, double weight, double damage, double creatDamage, int protection, int ice, int reflection, int passivedebuff) : base(price, name, type, rarity, weight, damage, creatDamage, protection)
    {
        Ice = ice;
        Reflection = reflection;
        PassiveDebuff = passivedebuff;
    }
    
    public override void PrintInfo()
    {
        base.PrintInfo(); 
        Console.WriteLine($"🛡️ Выпал щит: {Name} [{RarityRu}] | Урон: {Damage} | Защита: {Protection}");
        Console.ResetColor();
    }
}

public class Archer : Item
{
    public Archer(int price, string name, ItemType type, Rarity rarity, double weight, double damage, double creatDamage, int protection) : base(price, name, type, rarity, weight, damage, creatDamage, protection)
    {}
    
    public override void PrintInfo()
    {
        base.PrintInfo(); 
        Console.WriteLine($" Выпал лук: {Name} [{RarityRu}] | Урон: {Damage} | Защита: {Protection}");
        Console.ResetColor();
    }
}

public enum ArmorSlot
{
    Helmet,     
    Chestplate, 
    Leggings,   
    Gloves       
}

public class Armor : Item
{
    public string NameC { get; set; }
    public ArmorSlot Slot { get; set; }
    public Armor(int price, string name, ItemType type, Rarity rarity, double weight, double damage, double creatDamage, int protection, string nameC, ArmorSlot slot) : base(price, name, type, rarity, weight, damage, creatDamage, protection)
    {
        NameC = nameC;
        Slot = slot;
    }

    public override void PrintInfo()
    {
        base.PrintInfo(); 
        Console.WriteLine($"🛡️ Выпали доспехи: {Name} [{RarityRu}] | Защита: {Protection} | {NameC}");
        Console.ResetColor();
    }
}

public class Amulet : Item
{
    int MagAmulet { get; set; }
    public Amulet(int price, string name, ItemType type, Rarity rarity, double weight, double damage, double creatDamage, int protection, int magAmulet) : base(price, name, type, rarity, weight, damage, creatDamage, protection)
    {
        MagAmulet = magAmulet;
    }
    
    public override void PrintInfo()
    {
        base.PrintInfo(); 
        Console.WriteLine($"📿 Выпали амулеты: {Name} [{RarityRu}] | Защита: {Protection} | урон: {Damage} | крит: {CreatDamage}");
        Console.ResetColor();
    }
}

public enum ConsumableSize { None, Small, Medium, Large }
public enum ConsumableEffect { Food, Water, Strength, Regeneration, Magic, Defense, Speed }

public class Consumable : Item
{
    public ConsumableEffect Effect { get; set; }
    public ConsumableSize Size { get; set; }
    public int Power { get; set; } 
    public Consumable(int price, string name, Rarity rarity, ConsumableEffect effect, ConsumableSize size, int power)
        : base(price, name, ItemType.Consumable, rarity, 0.5, 0, 0, 0) 
    {
        Effect = effect;
        Size = size;
        Power = power;
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"🧪 {Name} [{RarityRu}] | Эффект: +{Power} | Цена: {Price} 🪙");
        Console.ResetColor();
    }
}

// Отдельный класс Торговца (вынесен наверх, чтобы был виден в Program)
public class Merchant
{
    public Merchant() { }
    private static List<Consumable> GenerateWares()
    {
        return new List<Consumable>
        {
            new Consumable(50, "Вкусная похлебка (Еда)", Rarity.Common, ConsumableEffect.Food, ConsumableSize.None, 20),
            new Consumable(30, "Фляга воды (Вода)", Rarity.Common, ConsumableEffect.Water, ConsumableSize.None, 10),

            new Consumable(100, "Малое зелье Силы", Rarity.Common, ConsumableEffect.Strength, ConsumableSize.Small, 15),
            new Consumable(250, "Среднее зелье Силы", Rarity.Rare, ConsumableEffect.Strength, ConsumableSize.Medium, 25),
            new Consumable(600, "Большое зелье Силы", Rarity.Mythical, ConsumableEffect.Strength, ConsumableSize.Large, 40),

            new Consumable(100, "Малое зелье Регенерации", Rarity.Common, ConsumableEffect.Regeneration, ConsumableSize.Small, 50),
            new Consumable(250, "Среднее зелье Регенерации", Rarity.Rare, ConsumableEffect.Regeneration, ConsumableSize.Medium, 150),
            new Consumable(600, "Большое зелье Регенерации", Rarity.Mythical, ConsumableEffect.Regeneration, ConsumableSize.Large, 300),

            new Consumable(100, "Малое зелье Магии", Rarity.Common, ConsumableEffect.Magic, ConsumableSize.Small, 10),
            new Consumable(250, "Среднее зелье Магии", Rarity.Rare, ConsumableEffect.Magic, ConsumableSize.Medium, 30),
            new Consumable(600, "Большое зелье Магии", Rarity.Mythical, ConsumableEffect.Magic, ConsumableSize.Large, 70),

            new Consumable(100, "Малое зелье Защиты", Rarity.Common, ConsumableEffect.Defense, ConsumableSize.Small, 5),
            new Consumable(250, "Среднее зелье Защиты", Rarity.Rare, ConsumableEffect.Defense, ConsumableSize.Medium, 15),
            new Consumable(600, "Большое зелье Защиты", Rarity.Mythical, ConsumableEffect.Defense, ConsumableSize.Large, 30),

            new Consumable(100, "Малое зелье Скорости", Rarity.Common, ConsumableEffect.Speed, ConsumableSize.Small, 5),
            new Consumable(250, "Среднее зелье Скорости", Rarity.Rare, ConsumableEffect.Speed, ConsumableSize.Medium, 15),
            new Consumable(600, "Большое зелье Скорости", Rarity.Mythical, ConsumableEffect.Speed, ConsumableSize.Large, 30),
        };
    }

    public static void OpenShop(Inventory inventory)
    {
        bool inShop = true;
        List<Consumable> wares = GenerateWares();

        while (inShop)
        {
            Console.Clear();
            Console.WriteLine("=== ⛺ ТОРГОВАЯ ЛАВКА ===");
            Console.WriteLine($"Ваше золото: {inventory.Gold} 🪙");
            Console.WriteLine("1. 🛒 Купить припасы и зелья");
            Console.WriteLine("2. 💰 Продать ненужные вещи (Освободить инвентарь)");
            Console.WriteLine("3. ❌ Уйти из лавки");
            Console.Write("Выбор: ");

            string choice = Console.ReadLine()!;

            if (choice == "1")
            {
                BuyMenu(inventory, wares);
            }
            else if (choice == "2")
            {
                Console.Clear();
                Console.WriteLine("=== СКУПКА ПРЕДМЕТОВ ===");
                inventory.SellMenu(); 
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
            else if (choice == "3")
            {
                inShop = false;
            }
        }
    }

    private static void BuyMenu(Inventory inventory, List<Consumable> wares)
    {
        Console.Clear();
        Console.WriteLine("=== ТОВАРЫ ===");
        for (int i = 0; i < wares.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {wares[i].Name} - {wares[i].Price} 🪙");
        }
        Console.WriteLine("0. Вернуться назад");
        Console.Write("\nВведите номер товара для покупки: ");

        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= wares.Count)
        {
            Consumable selected = wares[choice - 1];
            if (inventory.Gold >= selected.Price)
            {
                inventory.Gold -= selected.Price;
                inventory.AddItem(selected);
                Console.WriteLine($"\n✅ Вы успешно купили {selected.Name}!");
            }
            else
            {
                Console.WriteLine("\n❌ Недостаточно золота!");
            }
        }
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }
}

public enum HeroClass
{
    Knight,   
    Paladin,  
    Mage,     
    Archer    
}

public class Hero
{
    public Item? EquippedWeapon { get; set; } // Заменено с private set на set
    public string Name { get; set; }
    public HeroClass Class { get; set; }
    public double MaxHP {  get; set; }
    public double CurrentHP {  get; set; }
    public int Level { get; set; }
    public int XP { get; set; }
    public int RequiredXP { get; set; }
    public double Damage { get; set; }
    public double CreatDamage { get; set; }
    public double SpeedDamage { get; set; }
    public double BaseProtection { get; set; } 

    public Dictionary<ArmorSlot, Armor?> EquippedArmorSlots { get; set; }

    public double Protection
    {
        get
        {
            double bonus = BaseProtection;
            foreach (var slot in EquippedArmorSlots.Values)
            {
                if (slot != null) bonus += slot.Protection;
            }
            if (EquippedShield != null) bonus += EquippedShield.Protection;
            if (EquippedAmulet != null) bonus += EquippedAmulet.Protection;
            return bonus;
        }
    }

    public double TotalProtection
    {
        get
        {
            double armorBonus = 0;
            foreach (var slot in EquippedArmorSlots.Values)
            {
                if (slot != null)
                {
                    armorBonus += slot.Protection;
                }
            }
            return BaseProtection + armorBonus;
        }
    }

    public void DisplayEquipment()
    {
        Console.WriteLine($"\n=== 🛡️ ЭКИПИРОВКА ГЕРОЯ {Name} ===");
        Console.WriteLine($" Helmet     (Шлем):      {(EquippedArmorSlots[ArmorSlot.Helmet]?.Name ?? "Пусто")}");
        Console.WriteLine($" Chestplate (Нагрудник): {(EquippedArmorSlots[ArmorSlot.Chestplate]?.Name ?? "Пусто")}");
        Console.WriteLine($" Leggings   (Поножи):    {(EquippedArmorSlots[ArmorSlot.Leggings]?.Name ?? "Пусто")}");
        Console.WriteLine($" Boots      (Перчатки):  {(EquippedArmorSlots[ArmorSlot.Gloves]?.Name ?? "Пусто")}");
        Console.WriteLine($"-----------------------------------");
        Console.WriteLine($"Активный сет: {ActiveSetBonusName}");
        Console.WriteLine($"Итоговый урон: {TotalDamage:F1} | Крит: {TotalCreatDamage:F1} | Защита: {TotalProtection:F1}");
    }

    public int Regent { get; set; }
    public Sword? EquippedSword { get; set; } 
    public Amulet? EquippedAmulet { get; set; }
    public Staff? EquippedMagicStaff { get; set; }
    public Shield? EquippedShield { get; set; }
    public Archer? EquippedBow { get; set; }
public int SkillCooldown { get; set; } = 0; // Перезарядка навыка (в ходах)
    public Hero(string name, HeroClass heroClass, double maxHP, double currentHP, int level, int xp, int requiredXP, double damage, double creatDamage, double speedDamage, double baseProtection, int regent)
    {
        Name = name;
        Class = heroClass;
        MaxHP = maxHP;
        CurrentHP = currentHP;
        Level = level;
        XP = xp;
        RequiredXP = requiredXP;
        Damage = damage;
        CreatDamage = creatDamage;
        SpeedDamage = speedDamage;
        BaseProtection = baseProtection;
        EquippedArmorSlots = new Dictionary<ArmorSlot, Armor?>()
        {
            { ArmorSlot.Helmet, null },
            { ArmorSlot.Chestplate, null },
            { ArmorSlot.Leggings, null },
            { ArmorSlot.Gloves, null }
        };
        Regent = regent;
    }
    public string SkillName => Class switch
    {
        HeroClass.Knight => "Мощный удар (2x урон)",
        HeroClass.Paladin => "Святое исцеление (+50% HP)",
        HeroClass.Mage => "Огненный шар (Игнор брони)",
        HeroClass.Archer => "Меткий залп (Гарантированный крит)",
        _ => "Навык"
    };

    public int MaxSkillCooldown => Class switch
    {
        HeroClass.Knight => 3,
        HeroClass.Paladin => 5,
        HeroClass.Mage => 4,
        HeroClass.Archer => 3,
        _ => 3
    };

    // Метод уменьшения кулдауна в конце каждого хода
    public void DecrementCooldown()
    {
        if (SkillCooldown > 0)
        {
            SkillCooldown--;
        }
    }
    public List<Item> GetEquippedItems()
{
    var list = new List<Item>();
    if (EquippedWeapon != null) list.Add(EquippedWeapon);
    if (EquippedShield != null) list.Add(EquippedShield);
    if (EquippedAmulet != null) list.Add(EquippedAmulet);
    foreach (var armor in EquippedArmorSlots.Values)
    {
        if (armor != null) list.Add(armor);
    }
    return list;
}
    public void LoseAllEquipment()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("⚠️ ВЫ ПОТЕРЯЛИ ВСЮ СВОЮ ЭКИПИРОВКУ!");
        Console.ResetColor();

        if (EquippedWeapon != null)
        {
            Damage -= EquippedWeapon.Damage;
            CreatDamage -= EquippedWeapon.CreatDamage;
            if (EquippedWeapon is Staff staff && Class == HeroClass.Mage)
            {
                Damage -= staff.Elements.Sum();
            }
            EquippedWeapon = null;
        }

        if (EquippedShield != null)
        {
            Damage -= EquippedShield.Damage;
            CreatDamage -= EquippedShield.CreatDamage;
            EquippedShield = null;
        }

        if (EquippedAmulet != null)
        {
            Damage -= EquippedAmulet.Damage;
            CreatDamage -= EquippedAmulet.CreatDamage;
            EquippedAmulet = null;
        }

        EquippedArmorSlots[ArmorSlot.Helmet] = null;
        EquippedArmorSlots[ArmorSlot.Chestplate] = null;
        EquippedArmorSlots[ArmorSlot.Leggings] = null;
        EquippedArmorSlots[ArmorSlot.Gloves] = null;
    }

public void UseConsumable(Consumable consumable)
{
    switch (consumable.Effect)
    {
        case ConsumableEffect.Food:
        case ConsumableEffect.Water:
        case ConsumableEffect.Regeneration:
            CurrentHP = Math.Min(MaxHP, CurrentHP + consumable.Power);
            Console.WriteLine($"🧪 {Name} выпил {consumable.Name} и восстановил {consumable.Power} HP! Текущее HP: {CurrentHP}/{MaxHP}");
            break;

        case ConsumableEffect.Strength:
            Damage += consumable.Power;
            Console.WriteLine($"💪 {Name} выпил зелье силы! Урон увеличен на +{consumable.Power} (Всего урон: {Damage})");
            break;

        case ConsumableEffect.Defense:
            BaseProtection += consumable.Power;
            Console.WriteLine($"🛡️ {Name} выпил зелье защиты! Базовая защита увеличена на +{consumable.Power} (Всего защита: {GProtection})");
            break;

        case ConsumableEffect.Magic:
            // Если ваш герой маг или использует манию, можно поднять урон или особый параметр
            Damage += consumable.Power;
            Console.WriteLine($"🔮 {Name} выпил зелье магии! Сила магии/урон увеличена на +{consumable.Power}");
            break;

        case ConsumableEffect.Speed:
            SpeedDamage += consumable.Power;
            Console.WriteLine($"⚡ {Name} выпил зелье скорости! Скорость атаки увеличена на +{consumable.Power}");
            break;
        
        default:
            Console.WriteLine($"🧪 {Name} использовал {consumable.Name}, но ничего не произошло.");
            break;
    }
}

    public Item? UnequipWeapon()
    {
        if (EquippedWeapon == null) return null;
        Item oldWeapon = EquippedWeapon;
        Damage -= oldWeapon.Damage;
        CreatDamage -= oldWeapon.CreatDamage;

        if (oldWeapon is Staff staff && Class == HeroClass.Mage)
        {
            Damage -= staff.Elements.Sum();
        }

        Console.WriteLine($"🔻 {Name} снял {oldWeapon.Name}.");
        EquippedWeapon = null;
        return oldWeapon;
    }

    public Item? EquipWeapon(Item newWeapon)
    {
        if (newWeapon is Archer && Class == HeroClass.Paladin)
        {
            Console.WriteLine($"❌ Паладин не умеет обращаться с луком!");
            return null;
        }

        Item? oldWeapon = UnequipWeapon();
        EquippedWeapon = newWeapon;
        Damage += newWeapon.Damage;
        CreatDamage += newWeapon.CreatDamage;

        if (newWeapon is Staff staff)
        {
            if (Class == HeroClass.Mage)
            {
                int magicBonus = staff.Elements.Sum();
                Damage += magicBonus;
                Console.WriteLine($"🔮 {Name} активировал магию посоха! Доп. маг-урон: +{magicBonus}");
            }
            else
            {
                Console.WriteLine($"⚠️ {Name} не владеет магией, стихии посоха не работают!");
            }
        }

        Console.WriteLine($"⚔️ {Name} экипировал {newWeapon.Name}! Урон: {Damage} | Крит: {CreatDamage}");
        return oldWeapon;
    }

    public Armor? EquipArmor(Armor newArmor)
    {
        ArmorSlot targetSlot = newArmor.Slot;
        Armor? oldArmor = EquippedArmorSlots[targetSlot];
        EquippedArmorSlots[targetSlot] = newArmor;
        Console.WriteLine($"🛡️ Вы одели {newArmor.Name} в слот [{targetSlot}]!");
        return oldArmor;
    }

    public void AddXP(int amount)
    {
        XP += amount;
        Console.WriteLine($"✨ Вы получили {amount} опыта! (Всего: {XP}/{RequiredXP})");

        while (XP >= RequiredXP)
        {
            XP -= RequiredXP;         
            Level++;                  
            RequiredXP = (int)(RequiredXP * 1.5); 

            MaxHP += 20;        
            Damage += 5;
            BaseProtection += 2;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n🎉 УРОВЕНЬ ПОВЫШЕН! Теперь у вас {Level} уровень!");
            Console.WriteLine($"📈 Макс. HP: {MaxHP} | Урон: {Damage} | Защита: {GProtection}\n");
            Console.ResetColor();
        }
    }

    public void TakeDamage(double rawDamage)
    {
        double actualDamage = Math.Max(1, rawDamage - GProtection);
        CurrentHP = Math.Max(0, CurrentHP - actualDamage);
        Console.WriteLine($"💥 {Name} получил {actualDamage:F1} урона! Осталось HP: {CurrentHP:F1}/{MaxHP}");
    }

    public Shield? EquipShield(Shield newShield)
    {
        if (Class != HeroClass.Knight && Class != HeroClass.Paladin)
        {
            Console.WriteLine($"❌ {Name} не может носить щиты! (Только Мечник и Паладин)");
            return null;
        }

        Shield? oldShield = EquippedShield;
        if (oldShield != null)
        {
            Damage -= oldShield.Damage;
            CreatDamage -= oldShield.CreatDamage;
            Console.WriteLine($"🔻 {Name} снял {oldShield.Name}.");
        }

        EquippedShield = newShield;
        Damage += newShield.Damage;
        CreatDamage += newShield.CreatDamage;
        Console.WriteLine($"🛡️ {Name} экипировал щит {newShield.Name}!");
        return oldShield;
    }

    public Amulet? EquipAmulet(Amulet newAmulet)
    {
        Amulet? oldAmulet = EquippedAmulet;
        if (oldAmulet != null)
        {
            Damage -= oldAmulet.Damage;
            CreatDamage -= oldAmulet.CreatDamage;
            Console.WriteLine($"🔻 {Name} снял {oldAmulet.Name}.");
        }

        EquippedAmulet = newAmulet;
        Damage += newAmulet.Damage;
        CreatDamage += newAmulet.CreatDamage;
        Console.WriteLine($"📿 {Name} надел амулет {newAmulet.Name}!");
        return oldAmulet;
    }

    public string ActiveSetBonusName
    {
        get
        {
            var helmet = EquippedArmorSlots[ArmorSlot.Helmet];
            var chest = EquippedArmorSlots[ArmorSlot.Chestplate];
            var legs = EquippedArmorSlots[ArmorSlot.Leggings];
            var gloves = EquippedArmorSlots[ArmorSlot.Gloves];

            if (helmet == null || chest == null || legs == null || gloves == null)
                return "Нет";

            if (!string.IsNullOrEmpty(helmet.NameC) &&
                helmet.NameC == chest.NameC &&
                helmet.NameC == legs.NameC &&
                helmet.NameC == gloves.NameC)
            {
                return helmet.NameC; 
            }

            return "Нет";
        }
    }

    public double SetBonusDamage => ActiveSetBonusName switch
    {
        "часть брони комплекта: Мечник" => 30, 
        "часть брони комплекта: лучник" => 20, 
        _ => 0
    };

    public double SetBonusProtection => ActiveSetBonusName switch
    {
        "часть брони комплекта: паладин" => 80, 
        _ => 0
    };

    public double SetBonusCrit => ActiveSetBonusName switch
    {
        "часть брони комплекта: лучник" => 50, 
        "часть брони комплекта: маг" => 40,    
        _ => 0
    };
public double TotalDamage
    {
        get
        {
            double dmg = Damage + SetBonusDamage;
            if (EquippedWeapon != null && !EquippedWeapon.IsBroken) dmg += EquippedWeapon.Damage;
            if (EquippedShield != null && !EquippedShield.IsBroken) dmg += EquippedShield.Damage;
            if (EquippedAmulet != null && !EquippedAmulet.IsBroken) dmg += EquippedAmulet.Damage;
            foreach (var slot in EquippedArmorSlots.Values)
            {
                if (slot != null && !slot.IsBroken) dmg += slot.Damage;
            }
            return dmg;
        }
    }
    public double TotalCreatDamage => CreatDamage + SetBonusCrit;

   public double GProtection
    {
        get
        {
            double bonus = BaseProtection + SetBonusProtection;
            foreach (var slot in EquippedArmorSlots.Values)
            {
                if (slot != null && !slot.IsBroken) bonus += slot.Protection;
            }
            if (EquippedShield != null && !EquippedShield.IsBroken) bonus += EquippedShield.Protection;
            if (EquippedAmulet != null && !EquippedAmulet.IsBroken) bonus += EquippedAmulet.Protection;
            return bonus;
        }
    }
    public void ReduceDurabilityAfterBattle()
    {
        foreach (var item in GetEquippedItems())
        {
            int oldDurability = item.CurrentDurability;
            item.CurrentDurability = Math.Max(0, item.CurrentDurability - item.DurabilityLossPerBattle);

            if (item.IsBroken && oldDurability > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"💥 Предмет '{item.Name}' СЛОМАЛСЯ! Его бонусы отлючены.");
                Console.ResetColor();
            }
        }
    }
}

public class Inventory
{
    public int Gold { get; set; } = 4000;
    public List<Item> Items { get; set; } = new List<Item>();

    public void AddItem(Item item)
    {
        Items.Add(item);
        Console.WriteLine($"📦 Предмет {item.Name} добавлен в инвентарь!");
    }

public bool UseConsumableInBattle(Hero hero)
{
    var consumables = Items.Where(i => i.Type == ItemType.Consumable).ToList();
    if (consumables.Count == 0)
    {
        Console.WriteLine("\n❌ У вас в инвентаре нет зелий или еды!");
        Console.ReadKey();
        return false;
    }

    Console.Clear();
    Console.WriteLine("=== ВЫБЕРИТЕ ЗЕЛЬЕ ДЛЯ БОЯ ===");
    for (int i = 0; i < consumables.Count; i++)
    {
        consumables[i].SetColor();
        Console.WriteLine($"{i + 1}. {consumables[i].Name}");
        Console.ResetColor();
    }
    Console.WriteLine("0. Отмена");
    Console.Write("Выбор: ");

    if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= consumables.Count)
    {
        Consumable selected = (Consumable)consumables[choice - 1];
        hero.UseConsumable(selected);
        Items.Remove(selected); // Расходуем предмет
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
        return true;
    }
    return false;
}

    public void ShowInventory()
    {
        Console.WriteLine("\n--- 🎒 ВАШ ИНВЕНТАРЬ ---");
        if (Items.Count == 0)
        {
            Console.WriteLine("Инвентарь пуст!");
            return;
        }

        foreach (Item item in Items)
        {
            item.PrintInfo(); 
        }
        Console.WriteLine("------------------------\n");
    }
public List<Item> GetAllItems()
{
    return Items;
}
    public void AddItems(List<Item> newItems)
    {
        Items.AddRange(newItems); 
        Console.WriteLine($"📦 Добавлено предметов: {newItems.Count} шт.");
    }

    public void SellItem(Item item)
    {
        Gold += item.Price;
        Items.Remove(item);
        Console.WriteLine($"💰 Вы продали {item.Name}!");
    }

    public void SellMenu()
    {
        if (Items.Count == 0)
        {
            Console.WriteLine("Инвентарь пуст, продавать нечего!");
            return; 
        }

        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].SetColor(); 
            Console.WriteLine($"{i + 1}. {Items[i].Name} ({Items[i].Price} 🪙)");
            Console.ResetColor();
        }

        Console.Write("Введите номер предмета для продажи: ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            int index = choice - 1;
            if (index >= 0 && index < Items.Count)
            {
                SellItem(Items[index]);
            }
            else
            {
                Console.WriteLine("Данного предмета не найдено");
            }
        }
    }

    public void OpenMenu(Hero myHero)
{
    bool isOpen = true;

    while (isOpen)
    {
        Console.Clear();
        Console.WriteLine("=== ИНВЕНТАРЬ И ЭКИПИРОВКА ===");
        Console.WriteLine($"Герой: {myHero.Name} | Урон: {myHero.Damage} | Крит: {myHero.CreatDamage} | Защита: {myHero.Protection}");
        Console.WriteLine($"Золото: {Gold} 🪙");
        Console.WriteLine("------------------------------");
        Console.WriteLine("1. Оружие (Мечи, Посохи, Луки)");
        Console.WriteLine("2. Щиты");
        Console.WriteLine("3. Амулеты");
        Console.WriteLine("4. Броня");
        Console.WriteLine("5. Расходники (Зелья, Еда)");
        Console.WriteLine("6. Выйти в главное меню");
        Console.Write("Выберите категорию: ");

        string input = Console.ReadLine()!;

        if (input == "1")
        {
            var weapons = Items.Where(i => i.Type == ItemType.Sword || i.Type == ItemType.Staff || i.Type == ItemType.Archer).ToList();
            EquipMenu(myHero, weapons);
        }
        else if (input == "2")
        {
            var shields = Items.Where(i => i.Type == ItemType.Shield).ToList();
            EquipMenu(myHero, shields);
        }
        else if (input == "3")
        {
            var amulets = Items.Where(i => i.Type == ItemType.Amulet).ToList();
            EquipMenu(myHero, amulets);
        }
        else if (input == "4")
        {
            var armors = Items.Where(i => i.Type == ItemType.Armor).ToList();
            EquipMenu(myHero, armors);
        }
        else if (input == "5")
        {
            var consumables = Items.Where(i => i.Type == ItemType.Consumable).ToList();
            UseConsumableMenu(myHero, consumables);
        }
        else if (input == "6")
        {
            isOpen = false;
        }
    }
}

// Вспомогательный метод для отображения и использования зелий из инвентаря
private void UseConsumableMenu(Hero myHero, List<Item> consumableList)
{
    if (consumableList.Count == 0)
    {
        Console.WriteLine("\nУ вас нет никаких расходников или зелий!");
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
        return;
    }

    Console.Clear();
    Console.WriteLine("=== ВАШИ РАСХОДНИКИ ===");
    for (int i = 0; i < consumableList.Count; i++)
    {
        consumableList[i].SetColor();
        Console.WriteLine($"{i + 1}. {consumableList[i].Name}");
        Console.ResetColor();
    }
    Console.WriteLine("0. Назад");

    Console.Write("\nВведите номер зелья, чтобы использовать его: ");
    if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= consumableList.Count)
    {
        Consumable selectedConsumable = (Consumable)consumableList[choice - 1];
        
        // Применяем эффект через метод героя
        myHero.UseConsumable(selectedConsumable);
        
        // Удаляем использованное зелье из инвентаря
        Items.Remove(selectedConsumable);

        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}

    public void EquipMenu(Hero myHero, List<Item> itemList)
    {
        if (itemList.Count == 0)
        {
            Console.WriteLine("В этой категории ничего нет!");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].SetColor();
            Console.WriteLine($"{i + 1}. {itemList[i].Name} ({itemList[i].Price} 🪙)");
            Console.ResetColor();
        }

        Console.Write("Введите номер предмета, чтобы надеть его: ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            int index = choice - 1;

            if (index >= 0 && index < itemList.Count) 
            {
                Item selectedItem = itemList[index];
                Item? oldItem = null;

                if (selectedItem is Shield shield)
                {
                    if (myHero.Class != HeroClass.Knight && myHero.Class != HeroClass.Paladin)
                    {
                        Console.WriteLine("\n❌ Ваш класс не может носить щиты!");
                        Console.ReadKey();
                        return;
                    }
                    oldItem = myHero.EquipShield(shield);
                }
                else if (selectedItem is Armor armor)
                {
                    oldItem = myHero.EquipArmor(armor);
                }
                else if (selectedItem is Amulet amulet)
                {
                    oldItem = myHero.EquipAmulet(amulet);
                }
                else if (selectedItem is Sword || selectedItem is Staff || selectedItem is Archer)
                {
                    oldItem = myHero.EquipWeapon(selectedItem);
                }

                Items.Remove(selectedItem); 

                if (oldItem != null)
                {
                    Items.Add(oldItem);
                    Console.WriteLine($"🎒 {oldItem.Name} возвращён в инвентарь.");
                }

                Console.WriteLine($"\nТекущие статы: Урон: {myHero.Damage} | Крит: {myHero.CreatDamage} | Защита: {myHero.Protection}");
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Данного предмета не найдено.");
            }
        }
    }
}
public enum MonsterElement
{
    None,
    Fire,   // Огонь
    Ice,    // Лёд
    Dark,
    Venom,
    Blad    // Тьма
}
public class Monster
{
    public string Name { get; set; }
    public double MaxHP { get; set; } 
    public double CurrentHP { get; set; }
    public int RewardGold { get; set; }
    public int RewardXP { get; set; }
    public double Damage { get; set; }
    public double CreatDamage { get; set; }
    public double SpeedDamage { get; set; }
    public int Protection { get; set; }
    public int Regent { get; set; }
    public Sword? EquippedSword { get; set; } 
    public Armor? EquippedArmor { get; set; }
    public Amulet? EquippedAmulet { get; set; }
    public Staff? EquippedMagicStaff { get; set; }     
    public Shield? EquippedShield { get; set; }
public MonsterElement Element { get; set; } // Стихия монстра

    public Monster(string name, double maxHP, int rewardGold, int rewardXP, double damage, double creatDamage, double speedDamage, int protection, int regent, MonsterElement element = MonsterElement.None)
    {
        Name = name;
        MaxHP = maxHP;
        CurrentHP = maxHP; 
        RewardGold = rewardGold;
        RewardXP = rewardXP;
        Damage = damage;
        CreatDamage = creatDamage;
        SpeedDamage = speedDamage;
        Protection = protection;
        Regent = regent;
        Element = element;
    }

    public void TakeDamage(double rawDamage, bool ignoreArmor = false)
    {
        double actualDamage = ignoreArmor ? rawDamage : rawDamage - Protection;
        if (actualDamage < 1) actualDamage = 1; 
        CurrentHP -= actualDamage;
        if (CurrentHP < 0) CurrentHP = 0;
        
        string armorNote = ignoreArmor ? " (Игнор брони!)" : "";
        Console.WriteLine($"⚔️ {Name} получил {actualDamage:F1} урона!{armorNote} Осталось HP: {CurrentHP:F1}/{MaxHP}");
    }
}
class Arena
{
    private static Random random = new Random();

    public static void StartBattle(Hero hero, Inventory inventory)
    {
        hero.CurrentHP = hero.MaxHP; 
        hero.SkillCooldown = 0; // Сброс кулдауна перед боем

        Console.Clear();
        Console.WriteLine("⚔️ === ВЫБОР ЛОКАЦИИ И СЛОЖНОСТИ ===");
        Console.WriteLine("1. Лес | 2. Пещера | 3. Замок Тьмы | 4. Бездна");
        Console.Write("Выбор: ");

        if (!int.TryParse(Console.ReadLine(), out int diffChoice) || diffChoice < 1 || diffChoice > 4) return;

        List<Monster> possibleMonsters = ItemDatabase.GetMonstersByDifficulty(diffChoice);
        bool keepFighting = true; 

        while (keepFighting && hero.CurrentHP > 0)
        {
            Monster monster = possibleMonsters[random.Next(possibleMonsters.Count)];
            monster.CurrentHP = monster.MaxHP; 

            Console.Clear();
            Console.WriteLine($"👹 Навстречу вышел: {monster.Name} [Стихия: {monster.Element} | HP: {monster.MaxHP} | Защита: {monster.Protection}]");

            while (hero.CurrentHP > 0 && monster.CurrentHP > 0)
            {
                Console.WriteLine($"\nHP {hero.Name}: {hero.CurrentHP:F1}/{hero.MaxHP} | HP {monster.Name}: {monster.CurrentHP:F1}/{monster.MaxHP}");
                
                string cdInfo = hero.SkillCooldown > 0 ? $" (КД: {hero.SkillCooldown} х.)" : " (ГОТОВО!)";
                Console.WriteLine("1. Обычная атака");
                Console.WriteLine($"2. ✨ Способность: {hero.SkillName}{cdInfo}");
                Console.WriteLine("3. Использовать зелье");
                Console.WriteLine("4. ⚒️ Кузница (Ремонт предметов)");
                Console.WriteLine("5. Сбежать");

                string choice = Console.ReadLine()!;

                if (choice == "1")
                {
                    ExecuteHeroAttack(hero, monster, isSkill: false);
                    if (monster.CurrentHP > 0) ExecuteMonsterAttack(monster, hero);
                    hero.DecrementCooldown();
                }
                else if (choice == "2")
                {
                    if (hero.SkillCooldown > 0)
                    {
                        Console.WriteLine($"❌ Способность еще перезаряжается! Осталось ходов: {hero.SkillCooldown}");
                        continue;
                    }

                    ExecuteHeroSkill(hero, monster);
                    if (monster.CurrentHP > 0) ExecuteMonsterAttack(monster, hero);
                    hero.SkillCooldown = hero.MaxSkillCooldown; // Установка КД после использования
                }
                else if (choice == "3")
                {
                    if (inventory.UseConsumableInBattle(hero) && monster.CurrentHP > 0)
                    {
                        ExecuteMonsterAttack(monster, hero);
                        hero.DecrementCooldown();
                    }
                }
                else if (choice == "4")
{
    Blacksmith.OpenForge(hero, inventory);
}
                else if (choice == "5")
                {
                    Console.WriteLine("🏃 Вы сбежали и потеряли экипировку!");
                    hero.LoseAllEquipment();
                    keepFighting = false;
                    break;
                }
            }

            if (monster.CurrentHP <= 0)
            {
                Console.WriteLine($"\n🎉 {monster.Name} побеждён! Награда: {monster.RewardGold} 🪙");
                inventory.Gold += monster.RewardGold;
                hero.AddXP(monster.RewardXP);
                hero.ReduceDurabilityAfterBattle();
                Console.ReadKey();
            }
            else if (hero.CurrentHP <= 0)
            {
                Console.WriteLine($"\n☠️ Вы пали в бою.");
                hero.LoseAllEquipment();
            }
        }
    }

    private static void ExecuteHeroSkill(Hero hero, Monster monster)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n🌟 {hero.Name} использует навык [{hero.SkillName}]!");
        Console.ResetColor();

        switch (hero.Class)
        {
            case HeroClass.Knight: // Мощный удар (2x урон)
                double knightDmg = hero.TotalDamage * 2.0 * GetElementalMultiplier(hero, monster);
                monster.TakeDamage(knightDmg);
                break;

            case HeroClass.Paladin: // Святое исцеление (+50% от MaxHP)
                double heal = hero.MaxHP * 0.5;
                hero.CurrentHP = Math.Min(hero.MaxHP, hero.CurrentHP + heal);
                Console.WriteLine($"✨ {hero.Name} восстановил {heal:F0} HP! Текущее здоровье: {hero.CurrentHP:F1}/{hero.MaxHP}");
                break;

            case HeroClass.Mage: // Огненный шар (Игнор брони + стихийный урон)
                double mageDmg = (hero.TotalDamage + 40) * GetElementalMultiplier(hero, monster, forcedElement: Staff.Element.Fire);
                monster.TakeDamage(mageDmg, ignoreArmor: true);
                break;

            case HeroClass.Archer: // Меткий залп (Гарантированный крит)
                double archerDmg = (hero.TotalDamage + hero.TotalCreatDamage) * GetElementalMultiplier(hero, monster);
                monster.TakeDamage(archerDmg);
                break;
        }
    }

    private static void ExecuteHeroAttack(Hero hero, Monster monster, bool isSkill)
    {
        if (random.Next(100) < 30)
        {
            Console.WriteLine($"💨 {monster.Name} увернулся и контратакует!");
            ExecuteMonsterAttack(monster, hero);
            return;
        }

        double elementalMultiplier = GetElementalMultiplier(hero, monster);
        double damage = hero.TotalDamage * elementalMultiplier;

        if (elementalMultiplier > 1.0)
            Console.WriteLine($"🔥 Преимущество стихии! Урон x{elementalMultiplier:F1}");
        else if (elementalMultiplier < 1.0)
            Console.WriteLine($"🛡️ Враг устойчив к стихии! Урон x{elementalMultiplier:F1}");

        bool isCrit = random.Next(100) < Math.Clamp(hero.SpeedDamage, 5, 90);
        if (isCrit)
        {
            damage += hero.TotalCreatDamage;
            Console.WriteLine($"⚡ КРИТИЧЕСКИЙ УДАР!");
        }

        monster.TakeDamage(damage);
    }

    private static double GetElementalMultiplier(Hero hero, Monster monster, Staff.Element? forcedElement = null)
    {
        if (monster.Element == MonsterElement.None) return 1.0;

        Staff.Element weaponElem = Staff.Element.Water; // Базовое значение по умолчанию
        
        if (forcedElement.HasValue)
        {
            weaponElem = forcedElement.Value;
        }
        else if (hero.EquippedWeapon is Staff staff)
        {
            // Берем преобладающую стихию посоха
            int maxIdx = 0;
            for (int i = 1; i < staff.Elements.Length; i++)
                if (staff.Elements[i] > staff.Elements[maxIdx]) maxIdx = i;
            weaponElem = (Staff.Element)maxIdx;
        }
        else return 1.0;

        // Расчет слабостей
        if (weaponElem == Staff.Element.Fire && monster.Element == MonsterElement.Ice) return 2.0;
        if (weaponElem == Staff.Element.Ice && monster.Element == MonsterElement.Fire) return 0.5;
        if ((weaponElem == Staff.Element.Light || weaponElem == Staff.Element.BlackVoid) && monster.Element == MonsterElement.Dark) return 2.0;

        return 1.0;
    }

    private static void ExecuteMonsterAttack(Monster monster, Hero hero)
    {
        if (random.Next(100) < 10) return;
        hero.TakeDamage(monster.Damage);
    }
}
public static class ItemDatabase
{
    public static List<Monster> GetMonstersByDifficulty(int difficulty)
    {
        return difficulty switch
        {
            1 => new List<Monster> 
            {
                new Monster("маленький слизень", 70, 50, 20, 720, 0, 0, 0, 0, MonsterElement.None),
            new Monster("Ледяной гоблин", 180, 100, 20, 25, 0, 0, 2, 0, MonsterElement.Ice),
                new Monster("Дикий Волк", 110, 150, 30, 20, 60, 0, 1, 0, MonsterElement.None),
                new Monster("Лесной Разбойник", 200, 200, 30, 65, 0, 0, 2, 0, MonsterElement.None)
            },
            2 => new List<Monster> 
            {
                new Monster("Незрячий скелет", 330, 200, 80, 120, 30, 0, 5, 0, MonsterElement.Dark),
                new Monster("Огненный Орк", 860, 300, 400, 80, 170, 0, 5, 0, MonsterElement.Fire),
                new Monster("Каменный Голем", 1200, 500, 490, 490, 40, 0, 12, 0, MonsterElement.None),
                new Monster("Ядовитый Паук", 490, 950, 890, 560, 560, 0, 3, 0, MonsterElement.Venom)
            },
            3 => new List<Monster> 
            {
                new Monster("полтергейст", 5500, 10000, 4200, 1300, 60, 0, 20, 0, MonsterElement.Dark),
                new Monster("Смертный грех", 5500, 10000, 4200, 1300, 60, 0, 20, 0, MonsterElement.Fire),
                new Monster("Истинный вампир", 9800, 25000, 9500, 1800, 90, 0, 35, 0, MonsterElement.Blad)
            },
            4 => new List<Monster> 
            {
                new Monster("Рыцарь Смерти", 7500, 10000, 6200, 1300, 60, 0, 20, 0, MonsterElement.Venom),
                new Monster("Древний Дракон", 9800, 25000, 9500, 1800, 90, 0, 35, 0, MonsterElement.Fire),
                new Monster("Бог тьмы", 307500, 1110000, 69200, 111300, 60, 0, 20, 0, MonsterElement.Dark)
            },
            _ => new List<Monster>()
        };
    }

    public static List<Item>? GetItemsByCase(int caseIndex)
    {
        return caseIndex switch
        {
            0 => GetSwords(),
            1 => GetArcher(),
            2 => GetStaff(),
            3 => GetShield(),
            4 => GetArmor(),
            5 => GetAmulet(),
            _ => null
        };
    }

    public static List<Item> GetSwords()
    {
        return new List<Item>
        {
            new Sword(100, "Безнадёжный меч", ItemType.Sword, Rarity.Common, 62.7, 15, 0, 0, 0, 0),
            new Sword(500, "Закалённый меч", ItemType.Sword, Rarity.Rare, 27, 36.12, 0, 0, 0, 0),
            new Sword(1000, "Упоротый меч", ItemType.Sword, Rarity.Mythical, 7, 129, 150.4, 0, 0, 0),
            new Sword(1500, "Меч пламени дракона", ItemType.Sword, Rarity.Magical, 3, 180, 250.1, 28, 0, 0),
            new Sword(10000, "Святой Экскалибур", ItemType.Sword, Rarity.Legendary, 1, 270, 390.8, 0, 40, 0),
            new Sword(20000, "???????", ItemType.Sword, Rarity.Unknown, 0.3, 440, 150.4, 130, 57, -12)
        };
    }

    public static List<Item> GetArcher()
    {
        return new List<Item>
        {
            new Archer(100, "Рогатка", ItemType.Archer, Rarity.Common, 62.7, 5, 0, 0),
            new Archer(500, "Длинный лук", ItemType.Archer, Rarity.Rare, 27, 20, 0, 0),
            new Archer(1000, "Кровавый жнец", ItemType.Archer, Rarity.Mythical, 7, 70, 0, 0),
            new Archer(1500, "Ночной охотник", ItemType.Archer, Rarity.Magical, 3, 140, 0, 0),
            new Archer(10000, "Лук архангела", ItemType.Archer, Rarity.Legendary, 1, 250, 0, 0),
            new Archer(20000, "???????", ItemType.Archer, Rarity.Unknown, 0.3, 300, 0, 0)
        };
    }

    public static List<Item> GetAmulet()
    {
        return new List<Item>
        {
            new Amulet(100, "Cломанные часы", ItemType.Amulet, Rarity.Common, 62.7, 0, 0, 15, 0),
            new Amulet(500, "змеиный титул", ItemType.Amulet, Rarity.Rare, 27, 10, 10, 10, 0),
            new Amulet(1000, "Бутылек яда", ItemType.Amulet, Rarity.Mythical, 7, 0, 0, 65, 0),
            new Amulet(10000, "Знамя мага", ItemType.Amulet, Rarity.Magical, 3, 0, 0, 40, 100),
            new Amulet(15000, "Кулон жизни", ItemType.Amulet, Rarity.Legendary, 1, 0, 100, 100, 0),
            new Amulet(20000, "???????", ItemType.Amulet, Rarity.Unknown, 0.3, 50, 50, 200, 200)
        };
    }

    public static List<Item> GetShield()
    {
        return new List<Item>
        {
            new Shield(100, "Старый щит", ItemType.Shield, Rarity.Common, 62.7, 0, 0, 5, 0, 0, 0),
            new Shield(500, "Стальной щит", ItemType.Shield, Rarity.Rare, 27, 0, 0, 20, 0, 0, 0),
            new Shield(1000, "Плывучий щит", ItemType.Shield, Rarity.Mythical, 7, 0, 0, 65, 0, 0, 0),
            new Shield(1500, "ледяной шит", ItemType.Shield, Rarity.Magical, 3, 0, 0, 100, 28, 0, 0),
            new Shield(10000, "щит бездны", ItemType.Shield, Rarity.Legendary, 1, 0, 0, 160, 0, 60, 0),
            new Shield(20000, "???????", ItemType.Shield, Rarity.Unknown, 0.3, 0, 0, 440, 0, 190, -12)
        };
    }

    public static List<Item> GetStaff()
    {
        return new List<Item>
        {
            new Staff(100, "коряга", ItemType.Staff, Rarity.Common, 62.7, 5, 0, 0, new int[] {20, 0, 0, 0, 0, 0}, 0),
            new Staff(500, "медный посох", ItemType.Staff, Rarity.Rare, 27, 20, 0, 0, new int[] {0, 40, 0, 0, 0, 0}, 0),
            new Staff(1000, "лазуритный посох", ItemType.Staff, Rarity.Mythical, 7, 70, 0, 0, new int[] {0, 0, 120, 0, 0, 0}, 0),
            new Staff(1500, "пламенный посох", ItemType.Staff, Rarity.Magical, 3, 140, 0, 0, new int[] {0, 0, 0, 200, 0, 0}, 0),
            new Staff(10000, "посох луны", ItemType.Staff, Rarity.Legendary, 1, 250, 0, 0, new int[] {0, 0, 0, 0, 270, 0}, 0),
            new Staff(20000, "???????", ItemType.Staff, Rarity.Unknown, 0.3, 300, 0, 0, new int[] {50, 50, 50, 50, 50, 300}, -100)
        };
    }

    public static List<Item> GetArmor()
    {
        return new List<Item>
        {
            new Armor(15000, "Малый череп дракона : шлем(мечник)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: Мечник", ArmorSlot.Helmet),
            new Armor(15000, "Пламенный кирасир : нагрудник(мечник)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: Мечник", ArmorSlot.Chestplate),
            new Armor(15000, "Раскаленные поножи : штаны(мечник)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: Мечник", ArmorSlot.Leggings),
            new Armor(15000, "Когтистые наручи : руки(мечник)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: Мечник", ArmorSlot.Gloves),
            new Armor(15000, "позолоченный топфхельм : шлем(паладин)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин", ArmorSlot.Helmet),
            new Armor(15000, "стальной латник : нагрудник(паладин)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин", ArmorSlot.Chestplate),
            new Armor(15000, "стальные наголенники : штаны(паладин)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин", ArmorSlot.Leggings),
            new Armor(15000, "латные руковицы : руки(паладин)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин", ArmorSlot.Gloves),
            new Armor(15000, "шипованный баргут : шлем(маг)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг", ArmorSlot.Helmet),
            new Armor(15000, "теневая грудная клетка : нагрудник(маг)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг", ArmorSlot.Chestplate),
            new Armor(15000, "губящие тьму поножи : штаны(маг)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг", ArmorSlot.Leggings),
            new Armor(15000, "костяные клещи : руки(маг)", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг", ArmorSlot.Gloves),
            new Armor(15000, "тёмная маска : шлем(лучник)", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник", ArmorSlot.Helmet),
            new Armor(15000, "ночной плащ : нагрудник(лучник)", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник", ArmorSlot.Chestplate),
            new Armor(15000, "лёгкие сапоги : штаны(лучник)", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник", ArmorSlot.Leggings),
            new Armor(15000, "грубые наплечники : руки(лучник)", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник", ArmorSlot.Gloves),
            new Armor(100, "потертая бармица : шлем", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "", ArmorSlot.Helmet),
            new Armor(100, "порванная бригантина : нагрудник", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "", ArmorSlot.Chestplate),
            new Armor(100, "грязные сабатоны : штаны", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "", ArmorSlot.Leggings),
            new Armor(100, "ржавые наплечники : руки", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "", ArmorSlot.Gloves),
            new Armor(500, "потертая бармица : шлем", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "", ArmorSlot.Helmet),
            new Armor(500, "порванная бригантина : нагрудник", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "", ArmorSlot.Chestplate),
            new Armor(500, "грязные сабатоны : штаны", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "", ArmorSlot.Leggings),
            new Armor(500, "ржавые наплечники : руки", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "", ArmorSlot.Gloves),
            new Armor(1000, "потертая бармица : шлем", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "", ArmorSlot.Helmet),
            new Armor(1000, "порванная бригантина : нагрудник", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "", ArmorSlot.Chestplate),
            new Armor(1000, "грязные сабатоны : штаны", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "", ArmorSlot.Leggings),
            new Armor(1000, "ржавые наплечники : руки", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "", ArmorSlot.Gloves),
            new Armor(1500, "потертая бармица : шлем", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "", ArmorSlot.Helmet),
            new Armor(1500, "порванная бригантина : нагрудник", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "", ArmorSlot.Chestplate),
            new Armor(1500, "грязные сабатоны : штаны", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "", ArmorSlot.Leggings),
            new Armor(1500, "ржавые наплечники : руки", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "", ArmorSlot.Gloves),
            new Armor(20000, "???????(кровавый шлем)", ItemType.Armor, Rarity.Unknown, 0.2, 20, 20, 150, "", ArmorSlot.Helmet),
            new Armor(20000, "???????(мертвый латник)", ItemType.Armor, Rarity.Unknown, 0.3, 20, 20, 150, "", ArmorSlot.Chestplate),
            new Armor(20000, "???????(поножи смерти)", ItemType.Armor, Rarity.Unknown, 0.2, 20, 20, 150, "", ArmorSlot.Leggings),
            new Armor(20000, "???????(ломающие жизнь наручи)", ItemType.Armor, Rarity.Unknown, 0.2, 20, 20, 150, "", ArmorSlot.Gloves),
            new Armor(100, "рыцарский шлем : шлем", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "", ArmorSlot.Helmet),
            new Armor(100, "сверкающая кираса : нагрудник", ItemType.Armor,Rarity.Common, 6.5, 0, 0, 10, "", ArmorSlot.Chestplate),
            new Armor(100, "серебрянные поножи : штаны", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "", ArmorSlot.Leggings),
            new Armor(100, "стальные наручи : руки", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "", ArmorSlot.Gloves),
            new Armor(500, "рыцарский шлем : шлем", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "", ArmorSlot.Helmet),
            new Armor(500, "сверкающая кираса : нагрудник", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "", ArmorSlot.Chestplate),
            new Armor(500, "серебрянные поножи : штаны", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "", ArmorSlot.Leggings),
            new Armor(500, "стальные наручи : руки", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "", ArmorSlot.Gloves),
            new Armor(1000, "рыцарский шлем : шлем", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "", ArmorSlot.Helmet),
            new Armor(1000, "сверкающая кираса : нагрудник", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "", ArmorSlot.Chestplate),
            new Armor(1000, "серебрянные поножи : штаны", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "", ArmorSlot.Leggings),
            new Armor(1000, "стальные наручи : руки", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "", ArmorSlot.Gloves),
            new Armor(1500, "рыцарский шлем : шлем", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "", ArmorSlot.Helmet),
            new Armor(1500, "сверкающая кираса : нагрудник", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "", ArmorSlot.Chestplate),
            new Armor(1500, "серебрянные поножи : штаны", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "", ArmorSlot.Leggings),
            new Armor(1500, "стальные наручи : руки", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "", ArmorSlot.Gloves)
        };
    }
}
public class Blacksmith
{
    public static void OpenForge(Hero hero, Inventory inventory)
    {
        bool inForge = true;
        while (inForge)
        {
            Console.Clear();
            Console.WriteLine("=== ⚒️ МАСТЕРСКАЯ КУЗНЕЦА ===");
            Console.WriteLine($"Ваше золото: {inventory.Gold} 🪙\n");
            Console.WriteLine("1. 🛠️ Отремонтировать ВСЁ экипированное снаряжение");
            Console.WriteLine("2. 🎒 Ремонт конкретного предмета (Экипировка/Инвентарь)");
            Console.WriteLine("3. ❌ Покинуть кузницу");
            Console.Write("\nВыбор: ");

            string choice = Console.ReadLine()!;
            if (choice == "1") RepairAllEquipped(hero, inventory);
            else if (choice == "2") RepairSingleItemMenu(hero, inventory);
            else if (choice == "3") inForge = false;
        }
    }

    private static void RepairAllEquipped(Hero hero, Inventory inventory)
    {
        Console.Clear();
        List<Item> damagedEquipped = hero.GetEquippedItems().Where(i => i.CurrentDurability < i.MaxDurability).ToList();

        if (damagedEquipped.Count == 0)
        {
            Console.WriteLine("Всё ваше экипированное снаряжение в идеальном состоянии!");
            Console.ReadKey();
            return;
        }

        int totalCost = damagedEquipped.Sum(i => i.RepairCost);
        Console.WriteLine($"Общая стоимость ремонта: {totalCost} 🪙 (Золото: {inventory.Gold} 🪙)");
        Console.Write("Подтвердить ремонт? (1 - Да, 0 - Отмена): ");

        if (Console.ReadLine() == "1")
        {
            if (inventory.Gold >= totalCost)
            {
                inventory.Gold -= totalCost;
                foreach (var item in damagedEquipped) item.CurrentDurability = item.MaxDurability;
                Console.WriteLine("\n✅ Всё снаряжение отремонтировано!");
            }
            else Console.WriteLine("\n❌ Недостаточно золота!");
        }
        Console.ReadKey();
    }

    private static void RepairSingleItemMenu(Hero hero, Inventory inventory)
    {
        Console.Clear();
        List<Item> allDamaged = hero.GetEquippedItems()
            .Concat(inventory.GetAllItems())
            .Where(i => i.CurrentDurability < i.MaxDurability)
            .Distinct()
            .ToList();

        if (allDamaged.Count == 0)
        {
            Console.WriteLine("У вас нет повреждённых предметов!");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < allDamaged.Count; i++)
        {
            Item item = allDamaged[i];
            Console.WriteLine($"{i + 1}. {item.Name} - {item.CurrentDurability}/{item.MaxDurability} (Ремонт: {item.RepairCost} 🪙)");
        }

        Console.Write("\nВведите номер предмета: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= allDamaged.Count)
        {
            Item selected = allDamaged[choice - 1];
            if (inventory.Gold >= selected.RepairCost)
            {
                inventory.Gold -= selected.RepairCost;
                selected.CurrentDurability = selected.MaxDurability;
                Console.WriteLine($"\n✅ '{selected.Name}' отремонтирован!");
            }
            else Console.WriteLine("\n❌ Недостаточно золота!");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("\n❌ Неверный номер предмета!");
            Console.ReadKey();
        }
    }
}




public int AddGold
{
int amount = 0;
amout + Gold;
}