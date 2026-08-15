using System.Formats.Asn1;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        Hero myHero = null!;
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
    myHero = new Hero("Неудержимый мечник", 100, 100, 0, 0, 0, 12, 40, 10, 5, 0);
}
else if (choice == "2")
{
    myHero = new Hero("Святой Паладин", 160, 160, 0, 0, 0, 23, 5, 3, 10, 0); // Создали Воина
}
else if (choice == "3")
{
    myHero = new Hero("Мэрлин", 70, 70, 0, 0, 0, 5, 0, 5, 13, 0); // Создали Мага
}
else if (choice == "4")
{
    myHero = new Hero("Меткий Эльф", 70, 70, 0, 0, 0, 7, 38, 18, 0, 0);
}
else
            {
                Console.WriteLine("Неверный выбор! Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
        Inventory playerInventory = new Inventory();
        while (true)
{
            Console.WriteLine($"\nЗолото: {playerInventory.Gold} 🪙");
            Console.WriteLine("Выберите тип прокрутки:");
            Console.WriteLine("1. 1 прокрутка (1 000 золота)");
            Console.WriteLine("2. 10 прокруток (10 000 золота)");
            Console.WriteLine("3. Инвентарь и экипировка");
            Console.WriteLine("4. Выйти в бой");
            Console.WriteLine("5. Выход");
        
if (int.TryParse(Console.ReadLine(), out int spinChoice))
{     
    if (spinChoice == 1 && playerInventory.Gold >= 1000)
    {
       Console.Write("Выберите кейс для прокрутки\n 0. Мечи.\n 1. Луки.\n 2. Посохи.\n 3. Щиты.\n 4. Броня.\n 5. Амулеты.\n 6. Выход.\n");

                if (int.TryParse(Console.ReadLine(), out int singleChoice))
    {
            if (singleChoice == 0 && playerInventory.Gold >= 1000)
            {
                playerInventory.Gold -= 1000;
                // 1. Создаем мечи
        Item Sword1 = new Sword(100, "Безнадёжный меч", ItemType.Sword, Rarity.Common, 62.7, 15, 0, 0, 0, 0);//имя. редкость. выпадение. урон. крит. защита. огонь. рана
        Item Sword2 = new Sword(500, "Закалённый меч", ItemType.Sword, Rarity.Rare, 27, 36.12, 0, 0, 0, 0);
        Item Sword3 = new Sword(1000, "Упоротый меч", ItemType.Sword, Rarity.Mythical, 7, 129, 150.4, 0, 0, 0);
        Item Sword4 = new Sword(1500, "Меч пламени дракона", ItemType.Sword, Rarity.Magical, 3, 180, 250.1, 28, 0, 0);
        Item Sword5 = new Sword(10000, "Святой Экскалибур", ItemType.Sword, Rarity.Legendary, 1, 270, 390.8, 0, 40, 0);
        Item Sword6 = new Sword(20000, "???????", ItemType.Sword, Rarity.Unknown, 0.3, 440, 150.4, 130, 57, -12);

        // 2. Собираем их в список
        List<Item> ItemsList = new List<Item> { Sword1, Sword2, Sword3, Sword4, Sword5, Sword6 };

        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();;
    Item droppedItem = roulette.SingleSpin(ItemsList);
    playerInventory.AddItem(droppedItem);
            }
            else if (singleChoice == 1 && playerInventory.Gold >= 1000)
            {
                playerInventory.Gold -= 1000;
        Item Archer1 = new Archer(100, "Рогатка", ItemType.Archer, Rarity.Common, 62.7, 5, 0, 0);//имя. редкость. выпадение. урон. крит. защита.
        Item Archer2 = new Archer(500, "Длинный лук", ItemType.Archer, Rarity.Rare, 27, 20, 0, 0);
        Item Archer3 = new Archer(1000, "Кровавый жнец", ItemType.Archer, Rarity.Mythical, 7, 70, 0, 0);
        Item Archer4 = new Archer(1500, "Ночной охотник", ItemType.Archer, Rarity.Magical, 3, 140, 0, 0);
        Item Archer5 = new Archer(10000, "Лук архангела", ItemType.Archer, Rarity.Legendary, 1, 250, 0, 0);
        Item Archer6 = new Archer(20000, "???????", ItemType.Archer, Rarity.Unknown, 0.3, 300, 0, 0);

        // 2. Собираем их в список
        List<Item> ItemsList = new List<Item> { Archer1, Archer2, Archer3, Archer4, Archer5, Archer6 };

        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();
        Item droppedItem = roulette.SingleSpin(ItemsList);
    playerInventory.AddItem(droppedItem); 
            }
             else if (singleChoice == 2 && playerInventory.Gold >= 1000)
            {
                playerInventory.Gold -= 1000;
                // 1. Создаем посохи
        Item Staff1 = new Staff(100, "коряга", ItemType.Staff, Rarity.Common, 62.7, 5, 0, 0, new int[] {20, 0, 0, 0, 0, 0}, 0);//имя. редкость. выпадение. урон. крит. защита. вода. огонь. лёд. молния. яд. чёрная пустота. рана
        Item Staff2 = new Staff(500, "медный посох", ItemType.Staff, Rarity.Rare, 27, 20, 0, 0, new int[] {0, 40, 0, 0, 0, 0}, 0);
        Item Staff3 = new Staff(1000, "лазуритный посох", ItemType.Staff, Rarity.Mythical, 7, 70, 0, 0, new int[] {0, 0, 120, 0, 0, 0}, 0);
        Item Staff4 = new Staff(1500, "пламенный посох", ItemType.Staff, Rarity.Magical, 3, 140, 0, 0, new int[] {0, 0, 0, 200, 0, 0}, 0);
        Item Staff5 = new Staff(10000, "посох луны", ItemType.Staff, Rarity.Legendary, 1, 250, 0, 0, new int[] {0, 0, 0, 0, 270, 0}, 0);
        Item Staff6 = new Staff(20000, "???????", ItemType.Staff, Rarity.Unknown, 0.3, 300, 0, 0, new int[] {50, 50, 50, 50, 50, 300}, -100);
        // 2. Собираем их в список
        List<Item> ItemsList = new List<Item> { Staff1, Staff2, Staff3, Staff4, Staff5, Staff6 };

        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();
        Item droppedItem = roulette.SingleSpin(ItemsList);
    playerInventory.AddItem(droppedItem); 
            }
            else if (singleChoice == 3 && playerInventory.Gold >= 1000)
            {
                playerInventory.Gold -= 1000;
                // 1. Создаем щиты
        Item Shield1 = new Shield(100, "Старый щит", ItemType.Shield, Rarity.Common, 62.7, 0, 0, 5, 0, 0, 0);//имя. редкость. выпадение. урон. крит. защита. лёд. отражение урона. рана
        Item Shield2 = new Shield(500, "Стальной щит", ItemType.Shield, Rarity.Rare, 27, 0, 0, 20, 0, 0, 0);
        Item Shield3 = new Shield(1000, "Плывучий щит", ItemType.Shield, Rarity.Mythical, 7, 0, 0, 65, 0, 0, 0);
        Item Shield4 = new Shield(1500, "ледяной шит", ItemType.Shield, Rarity.Magical, 3, 0, 0, 100, 28, 0, 0);
        Item Shield5 = new Shield(10000, "щит бездны", ItemType.Shield, Rarity.Legendary, 1, 0, 0, 160, 0, 60, 0);
        Item Shield6 = new Shield(20000, "???????", ItemType.Shield, Rarity.Unknown, 0.3, 0, 0, 440, 0, 190, -12);
        // 2. Собираем их в список
        List<Item> ItemsList = new List<Item> { Shield1, Shield2, Shield3, Shield4, Shield5, Shield6 };

        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();
        Item droppedItem = roulette.SingleSpin(ItemsList);
    playerInventory.AddItem(droppedItem); 
            }
            else if(singleChoice == 4 && playerInventory.Gold >= 1500)
            {
                playerInventory.Gold -= 1500;
        //Броня индивидуальная для каждого класса 
        Item DragonArmor1 = new Armor(15000, "малый череп дракона", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: мечник"); //прирост к силе меча
        Item DragonArmor2 = new Armor(15000, "пламенный кирасир", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: мечник");
        Item DragonArmor3 = new Armor(15000, "раскаленные поножи", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: мечник");
        Item DragonArmor4 = new Armor(15000, "когтистые наручи", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: мечник");
        Item DruidArmor1 = new Armor(15000, "позолоченный топфхельм", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин"); //прирост к броне + 100 к каждой части
        Item DruidArmor2 = new Armor(15000, "стальной латник", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин");
        Item DruidArmor3 = new Armor(15000, "стальные наголенники", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин");
        Item DruidArmor4 = new Armor(15000, "латные руковицы", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин");
        Item MagArmor1 = new Armor(15000, "шипованный баргут", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг");// прирост к магии 
        Item MagArmor2 = new Armor(15000, "теневая грудная клетка", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг");
        Item MagArmor3 = new Armor(15000, "губящие тьму поножи", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг");
        Item MagArmor4 = new Armor(15000, "костяные клещи", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг");
        Item ArcherArmor1 = new Armor(15000, "ржавые доспехи", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник");// прирост к скорости атаки и увороту 
        Item ArcherArmor2 = new Armor(15000, "ржавые доспехи", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник");
        Item ArcherArmor3 = new Armor(15000, "ржавые доспехи", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник");
        Item ArcherArmor4 = new Armor(15000, "ржавые доспехи", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник");
        // обычная броня 1
        Item Armor1 = new Armor(100, "потертая бармица", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");//имя. редкость. выпадение. урон. крит. защита.
        Item Armor2 = new Armor(100, "порванная бригантина", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor3 = new Armor(100, "грязные сабатоны", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor4 = new Armor(100, "ржавые наплечники", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor5 = new Armor(500, "потертая бармица", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor6 = new Armor(500, "порванная бригантина", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor7 = new Armor(500, "грязные сабатоны", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor8 = new Armor(500, "ржавые наплечники", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor9 = new Armor(1000, "потертая бармица", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor10 = new Armor(1000, "порванная бригантина", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor11 = new Armor(1000, "грязные сабатоны", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor12 = new Armor(1000, "ржавые наплечники", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor13 = new Armor(1500, "потертая бармица", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor14 = new Armor(1500, "порванная бригантина", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor15 = new Armor(1500, "грязные сабатоны", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor16 = new Armor(1500, "ржавые наплечники", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        //неизвестная броня
        Item Armor17 = new Armor(20000, "???????(кровавый шлем)", ItemType.Armor, Rarity.Unknown, 0.2, 20, 20, 150, "");
        Item Armor18 = new Armor(20000, "???????(мертвый латник)", ItemType.Armor, Rarity.Unknown, 0.3, 20, 20, 150, "");
        Item Armor19 = new Armor(20000, "???????(поножи смерти)", ItemType.Armor, Rarity.Unknown, 0.2, 20, 20, 150, "");
        Item Armor20 = new Armor(20000, "???????(ломающие жизнь наручи)", ItemType.Armor, Rarity.Unknown, 0.2, 20, 20, 150, "");
        // обычная броня 2
        Item Armor21 = new Armor(100, "рыцарский шлем", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");//имя. редкость. выпадение. урон. крит. защита.
        Item Armor22 = new Armor(100, "сверкающая кираса", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor23 = new Armor(100, "серебрянные поножи", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor24 = new Armor(100, "стальные наручи", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor25 = new Armor(500, "рыцарский шлем", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor26 = new Armor(500, "сверкающая кираса", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor27 = new Armor(500, "серебрянные поножи", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor28 = new Armor(500, "стальные наручи", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor29 = new Armor(1000, "рыцарский шлем", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor30 = new Armor(1000, "сверкающая кираса", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor31 = new Armor(1000, "серебрянные поножи", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor32 = new Armor(1000, "стальные наручи", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor33 = new Armor(1500, "рыцарский шлем", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor34 = new Armor(1500, "сверкающая кираса", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor35 = new Armor(1500, "серебрянные поножи", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor36 = new Armor(1500, "стальные наручи", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        // 2. Собираем их в список
        List<Item> ItemsList = new List<Item> { DragonArmor1, DragonArmor2, DragonArmor3, DragonArmor4, DruidArmor1, DruidArmor2, DruidArmor3, DruidArmor4, MagArmor1, MagArmor2, MagArmor3, MagArmor4, ArcherArmor1, ArcherArmor2, ArcherArmor3, ArcherArmor4, Armor1, Armor2, Armor3, Armor4, Armor5, Armor6, Armor7, Armor8, Armor9, Armor10, Armor11, Armor12, Armor13, Armor14, Armor15, Armor16, Armor17, Armor18, Armor19, Armor20, Armor21, Armor22, Armor23, Armor24, Armor25, Armor26, Armor27, Armor28, Armor29, Armor30, Armor31, Armor32, Armor33, Armor34, Armor35, Armor36 };
        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();
        Item droppedItem = roulette.SingleSpin(ItemsList);
    playerInventory.AddItem(droppedItem);
            }
             else if(singleChoice == 5 && playerInventory.Gold >= 1000)
            {
                playerInventory.Gold -= 1000;
        Item Amulet1 = new Amulet(100, "Cломанные часы", ItemType.Amulet, Rarity.Common, 62.7, 0, 0, 15, 0);//имя. редкость. выпадение. урон. крит. защита.
        Item Amulet2 = new Amulet(500, "змеиный титул", ItemType.Amulet, Rarity.Rare, 27, 10, 10, 10, 0);
        Item Amulet3 = new Amulet(1000, "Бутылек яда", ItemType.Amulet, Rarity.Mythical, 7, 0, 0, 65, 0);
        Item Amulet4 = new Amulet(10000, "Знамя мага", ItemType.Amulet, Rarity.Magical, 3, 0, 0, 40, 100);
        Item Amulet5 = new Amulet(15000, "Кулон жизни", ItemType.Amulet, Rarity.Legendary, 1, 0, 100, 100, 0);
        Item Amulet6 = new Amulet(20000, "???????", ItemType.Amulet, Rarity.Unknown, 0.3, 50, 50, 200, 200);
               List<Item> ItemsList = new List<Item> { Amulet1, Amulet2, Amulet3, Amulet4, Amulet5, Amulet6};
        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();
        Item droppedItem = roulette.SingleSpin(ItemsList);
    playerInventory.AddItem(droppedItem); 
            }
            else if (singleChoice == 6)
        {
            continue; // выходим из бесконечного цикла — программа завершится
        }
                        else
                        {
                            Console.Write("Недостаточно золото");
                        }
    }
    }
    else if (spinChoice == 2 && playerInventory.Gold >= 10000)
{
          Console.Write("Выберите кейс для прокрутки\n 0. Мечи.\n 1. Луки.\n 2. Посохи.\n 3. Щиты.\n 4. Броня.\n 5. Амулеты.\n 6. Выход.\n");

                if (int.TryParse(Console.ReadLine(), out int userChoice))
    {
            if (userChoice == 0 && playerInventory.Gold >= 10000)
            {
                playerInventory.Gold -= 10000;
                // 1. Создаем мечи
        Item Sword1 = new Sword(100, "Безнадёжный меч", ItemType.Sword, Rarity.Common, 62.7, 15, 0, 0, 0, 0);//имя. редкость. выпадение. урон. крит. защита. огонь. рана
        Item Sword2 = new Sword(500, "Закалённый меч", ItemType.Sword, Rarity.Rare, 27, 36.12, 0, 0, 0, 0);
        Item Sword3 = new Sword(1000, "Упоротый меч", ItemType.Sword, Rarity.Mythical, 7, 129, 150.4, 0, 0, 0);
        Item Sword4 = new Sword(1500, "Меч пламени дракона", ItemType.Sword, Rarity.Magical, 3, 180, 250.1, 28, 0, 0);
        Item Sword5 = new Sword(10000, "Святой Экскалибур", ItemType.Sword, Rarity.Legendary, 1, 270, 390.8, 0, 40, 0);
        Item Sword6 = new Sword(20000, "???????", ItemType.Sword, Rarity.Unknown, 0.3, 440, 150.4, 130, 57, -12);

        // 2. Собираем их в список
        List<Item> ItemsList = new List<Item> { Sword1, Sword2, Sword3, Sword4, Sword5, Sword6 };
        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();
        List<Item> tenItems = roulette.TestSpin(ItemsList);
playerInventory.AddItems(tenItems); // Добавляет сразу все 10 предметов!
            }
            else if (userChoice == 1 && playerInventory.Gold >= 10000)
            {
                playerInventory.Gold -= 10000;
        Item Archer1 = new Archer(100, "Рогатка", ItemType.Archer, Rarity.Common, 62.7, 5, 0, 0);//имя. редкость. выпадение. урон. крит. защита.
        Item Archer2 = new Archer(500, "Длинный лук", ItemType.Archer, Rarity.Rare, 27, 20, 0, 0);
        Item Archer3 = new Archer(1000, "Кровавый жнец", ItemType.Archer, Rarity.Mythical, 7, 70, 0, 0);
        Item Archer4 = new Archer(1500, "Ночной охотник", ItemType.Archer, Rarity.Magical, 3, 140, 0, 0);
        Item Archer5 = new Archer(10000, "Лук архангела", ItemType.Archer, Rarity.Legendary, 1, 250, 0, 0);
        Item Archer6 = new Archer(20000, "???????", ItemType.Archer, Rarity.Unknown, 0.3, 300, 0, 0);
        // 2. Собираем их в список
        List<Item> ItemsList = new List<Item> { Archer1, Archer2, Archer3, Archer4, Archer5, Archer6 };
Roulette roulette = new Roulette();
        List<Item> tenItems = roulette.TestSpin(ItemsList);
playerInventory.AddItems(tenItems); // Добавляет сразу все 10 предметов!
            }
             else if (userChoice == 2 && playerInventory.Gold >= 10000)
            {
                playerInventory.Gold -= 10000;
                // 1. Создаем посохи
        Item Staff1 = new Staff(100, "коряга", ItemType.Staff, Rarity.Common, 62.7, 5, 0, 0, new int[] {20, 0, 0, 0, 0, 0}, 0);//имя. редкость. выпадение. урон. крит. защита. вода. огонь. лёд. молния. яд. чёрная пустота. рана
        Item Staff2 = new Staff(500, "медный посох", ItemType.Staff, Rarity.Rare, 27, 20, 0, 0, new int[] {0, 40, 0, 0, 0, 0}, 0);
        Item Staff3 = new Staff(1000, "лазуритный посох", ItemType.Staff, Rarity.Mythical, 7, 70, 0, 0, new int[] {0, 0, 120, 0, 0, 0}, 0);
        Item Staff4 = new Staff(1500, "пламенный посох", ItemType.Staff, Rarity.Magical, 3, 140, 0, 0, new int[] {0, 0, 0, 200, 0, 0}, 0);
        Item Staff5 = new Staff(10000, "посох луны", ItemType.Staff, Rarity.Legendary, 1, 250, 0, 0, new int[] {0, 0, 0, 0, 270, 0}, 0);
        Item Staff6 = new Staff(20000, "???????", ItemType.Staff, Rarity.Unknown, 0.3, 300, 0, 0, new int[] {50, 50, 50, 50, 50, 300}, -100);
        // 2. Собираем их в список
        List<Item> ItemsList = new List<Item> { Staff1, Staff2, Staff3, Staff4, Staff5, Staff6 };
        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();
        List<Item> tenItems = roulette.TestSpin(ItemsList);
playerInventory.AddItems(tenItems); // Добавляет сразу все 10 предметов! 
            }
            else if (userChoice == 3 && playerInventory.Gold >= 10000)
            {
                playerInventory.Gold -= 10000;
                // 1. Создаем щиты
        Item Shield1 = new Shield(100, "Старый щит", ItemType.Shield, Rarity.Common, 62.7, 0, 0, 5, 0, 0, 0);//имя. редкость. выпадение. урон. крит. защита. лёд. отражение урона. рана
        Item Shield2 = new Shield(500, "Стальной щит", ItemType.Shield, Rarity.Rare, 27, 0, 0, 20, 0, 0, 0);
        Item Shield3 = new Shield(1000, "Плывучий щит", ItemType.Shield, Rarity.Mythical, 7, 0, 0, 65, 0, 0, 0);
        Item Shield4 = new Shield(1500, "ледяной шит", ItemType.Shield, Rarity.Magical, 3, 0, 0, 100, 28, 0, 0);
        Item Shield5 = new Shield(10000, "щит бездны", ItemType.Shield, Rarity.Legendary, 1, 0, 0, 160, 0, 60, 0);
        Item Shield6 = new Shield(20000, "???????", ItemType.Shield, Rarity.Unknown, 0.3, 0, 0, 440, 0, 190, -12);
        // 2. Собираем их в список
        List<Item> ItemsList = new List<Item> { Shield1, Shield2, Shield3, Shield4, Shield5, Shield6 };
        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();
        List<Item> tenItems = roulette.TestSpin(ItemsList);
playerInventory.AddItems(tenItems); // Добавляет сразу все 10 предметов! 
            }
            else if(userChoice == 4 && playerInventory.Gold >= 15000)
            {
                playerInventory.Gold -= 15000;
        //Броня индивидуальная для каждого класса 
         Item DragonArmor1 = new Armor(15000, "малый череп дракона", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: мечник"); //прирост к силе меча
        Item DragonArmor2 = new Armor(15000, "пламенный кирасир", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: мечник");
        Item DragonArmor3 = new Armor(15000, "раскаленные поножи", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: мечник");
        Item DragonArmor4 = new Armor(15000, "когтистые наручи", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: мечник");
        Item DruidArmor1 = new Armor(15000, "позолоченный топфхельм", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин"); //прирост к броне + 100 к каждой части
        Item DruidArmor2 = new Armor(15000, "стальной латник", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин");
        Item DruidArmor3 = new Armor(15000, "стальные наголенники", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин");
        Item DruidArmor4 = new Armor(15000, "латные руковицы", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: паладин");
        Item MagArmor1 = new Armor(15000, "шипованный баргут", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг");// прирост к магии 
        Item MagArmor2 = new Armor(15000, "теневая грудная клетка", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг");
        Item MagArmor3 = new Armor(15000, "губящие тьму поножи", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг");
        Item MagArmor4 = new Armor(15000, "костяные клещи", ItemType.Armor, Rarity.Legendary, 0.4, 0, 0, 100, "часть брони комплекта: маг");
        Item ArcherArmor1 = new Armor(15000, "ржавые доспехи", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник");// прирост к скорости атаки и увороту 
        Item ArcherArmor2 = new Armor(15000, "ржавые доспехи", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник");
        Item ArcherArmor3 = new Armor(15000, "ржавые доспехи", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник");
        Item ArcherArmor4 = new Armor(15000, "ржавые доспехи", ItemType.Armor, Rarity.Legendary, 0.4, 10, 50, 30, "часть брони комплекта: лучник");
        // обычная броня 1
        Item Armor1 = new Armor(100, "потертая бармица", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");//имя. редкость. выпадение. урон. крит. защита.
        Item Armor2 = new Armor(100, "порванная бригантина", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor3 = new Armor(100, "грязные сабатоны", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor4 = new Armor(100, "ржавые наплечники", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor5 = new Armor(500, "потертая бармица", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor6 = new Armor(500, "порванная бригантина", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor7 = new Armor(500, "грязные сабатоны", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor8 = new Armor(500, "ржавые наплечники", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor9 = new Armor(1000, "потертая бармица", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor10 = new Armor(1000, "порванная бригантина", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor11 = new Armor(1000, "грязные сабатоны", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor12 = new Armor(1000, "ржавые наплечники", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor13 = new Armor(1500, "потертая бармица", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor14 = new Armor(1500, "порванная бригантина", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor15 = new Armor(1500, "грязные сабатоны", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor16 = new Armor(1500, "ржавые наплечники", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        //неизвестная броня
        Item Armor17 = new Armor(20000, "???????(кровавый шлем)", ItemType.Armor, Rarity.Unknown, 0.2, 20, 20, 150, "");
        Item Armor18 = new Armor(20000, "???????(мертвый латник)", ItemType.Armor, Rarity.Unknown, 0.3, 20, 20, 150, "");
        Item Armor19 = new Armor(20000, "???????(поножи смерти)", ItemType.Armor, Rarity.Unknown, 0.2, 20, 20, 150, "");
        Item Armor20 = new Armor(20000, "???????(ломающие жизнь наручи)", ItemType.Armor, Rarity.Unknown, 0.2, 20, 20, 150, "");
        // обычная броня 2
        Item Armor21 = new Armor(100, "рыцарский шлем", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");//имя. редкость. выпадение. урон. крит. защита.
        Item Armor22 = new Armor(100, "сверкающая кираса", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor23 = new Armor(100, "серебрянные поножи", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor24 = new Armor(100, "стальные наручи", ItemType.Armor, Rarity.Common, 6.5, 0, 0, 10, "");
        Item Armor25 = new Armor(500, "рыцарский шлем", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor26 = new Armor(500, "сверкающая кираса", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor27 = new Armor(500, "серебрянные поножи", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor28 = new Armor(500, "стальные наручи", ItemType.Armor, Rarity.Rare, 3, 0, 0, 30, "");
        Item Armor29 = new Armor(1000, "рыцарский шлем", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor30 = new Armor(1000, "сверкающая кираса", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor31 = new Armor(1000, "серебрянные поножи", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor32 = new Armor(1000, "стальные наручи", ItemType.Armor, Rarity.Mythical, 0.9, 10, 0, 50, "");
        Item Armor33 = new Armor(1500, "рыцарский шлем", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor34 = new Armor(1500, "сверкающая кираса", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor35 = new Armor(1500, "серебрянные поножи", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        Item Armor36 = new Armor(1500, "стальные наручи", ItemType.Armor, Rarity.Magical, 0.7, 0, 30, 100, "");
        // 2. Собираем их в список
        List<Item> ItemsList = new List<Item> { DragonArmor1, DragonArmor2, DragonArmor3, DragonArmor4, DruidArmor1, DruidArmor2, DruidArmor3, DruidArmor4, MagArmor1, MagArmor2, MagArmor3, MagArmor4, ArcherArmor1, ArcherArmor2, ArcherArmor3, ArcherArmor4, Armor1, Armor2, Armor3, Armor4, Armor5, Armor6, Armor7, Armor8, Armor9, Armor10, Armor11, Armor12, Armor13, Armor14, Armor15, Armor16, Armor17, Armor18, Armor19, Armor20, Armor21, Armor22, Armor23, Armor24, Armor25, Armor26, Armor27, Armor28, Armor29, Armor30, Armor31, Armor32, Armor33, Armor34, Armor35, Armor36 };
        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();
        List<Item> tenItems = roulette.TestSpin(ItemsList);
playerInventory.AddItems(tenItems); // Добавляет сразу все 10 предметов!
            }
             else if(userChoice == 5 && playerInventory.Gold >= 10000)
            {
                playerInventory.Gold -= 10000;
        Item Amulet1 = new Amulet(100, "Cломанные часы", ItemType.Amulet, Rarity.Common, 62.7, 0, 0, 15, 0);//имя. редкость. выпадение. урон. крит. защита.
        Item Amulet2 = new Amulet(500, "змеиный титул", ItemType.Amulet, Rarity.Rare, 27, 10, 10, 10, 0);
        Item Amulet3 = new Amulet(1000, "Бутылек яда", ItemType.Amulet, Rarity.Mythical, 7, 0, 0, 65, 0);
        Item Amulet4 = new Amulet(10000, "Знамя мага", ItemType.Amulet, Rarity.Magical, 3, 0, 0, 40, 100);
        Item Amulet5 = new Amulet(15000, "Кулон жизни", ItemType.Amulet, Rarity.Legendary, 1, 0, 100, 100, 0);
        Item Amulet6 = new Amulet(20000, "???????", ItemType.Amulet, Rarity.Unknown, 0.3, 50, 50, 200, 200);
               List<Item> ItemsList = new List<Item> { Amulet1, Amulet2, Amulet3, Amulet4, Amulet5, Amulet6};
        // 3. Создаем рулетку и запускаем тест!
        Roulette roulette = new Roulette();
        List<Item> tenItems = roulette.TestSpin(ItemsList);
playerInventory.AddItems(tenItems); // Добавляет сразу все 10 предметов!
}
    else if (userChoice == 6)
{
    continue; // Возвращаемся в самое начало цикла, к главному меню!
}
    }
}
     else if (spinChoice == 3)
                {
                    playerInventory.OpenMenu(myHero);
                }
    else if (spinChoice == 4)
        {
            Arena.StartBattle(myHero, playerInventory);
        }
    else if (spinChoice == 5)
    {
        Console.WriteLine("Выход из программы...");
        break;
    }

    else
    {
        Console.WriteLine("Недостаточно золота или неверный выбор!");
    }
}
            }
    
}
}
public enum Rarity
{
   Common,      // Обычное
    Rare,        // Редкое
    Mythical,    // Мифическое
    Magical,     // Магическое
    Legendary,   // Легендарное
    Unknown      // Неизвестное
}
public enum ItemType
{
    Sword,
    Staff,
    Armor,
    Amulet,
    Shield,
    Archer
}
public class Item
{
    public int Price { get; set; }
    public string Name { get; set; }//продажа. имя. классы. редкость. выпадение. урон. крит. защита
    public ItemType Type { get; set; }   // Меч, Шлем и т.д.
public Rarity Rarity { get; set; }
public double Weight { get; set; }
public double Damage { get; set; }
    public double CreatDamage { get; set; }
    public int Protection { get; set; }
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
    virtual public void PrintInfo()
{
    // Назначаем цвет в зависимости от редкости
    Console.ForegroundColor = Rarity switch
    {
        Rarity.Common => ConsoleColor.Gray,
        Rarity.Rare => ConsoleColor.Blue,
        Rarity.Mythical => ConsoleColor.Magenta,
        Rarity.Magical => ConsoleColor.Red,
        Rarity.Legendary => ConsoleColor.DarkYellow,
        Rarity.Unknown => ConsoleColor.Black,// ... здесь будут остальные редкости
        _ => ConsoleColor.White // Запасной цвет, если ничего не подошло
    };
}
public void SetColor()
    {
        Rarity
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
    
    public override void PrintInfo()// override это запрос из item
    {
        // Вызываем базовый метод из Item, чтобы включить нужный цвет
        base.PrintInfo(); 
        
        // Добавляем уникальный текст для меча
        Console.WriteLine($"⚔️ Выпал меч: {Name} [{RarityRu}] | Урон: {Damage} | Крит: {CreatDamage}");

        Console.ResetColor();
    }
}
class Roulette
{
    // Создаем генератор случайных чисел внутри класса
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
    List<Item> droppedList = new List<Item>(); // 1. Создаем пустой список для результатов

    for (int i = 0; i < 10; i++)
    {
        Item droppedItems = Spin(weapons);
        droppedItems.PrintInfo();
        droppedList.Add(droppedItems); // 2. Добавляем каждый выпавший предмет в наш список
    }

    return droppedList; // 3. Возвращаем заполненный список из 10 предметов!
}
}

public class Staff : Item
{
    public int[] Elements { get; set; } // индекс = стихия
    public enum Element
{
    Water,   // индекс 0
    Fire,    // индекс 1
    Ice,     // индекс 2
    Light,   // индекс 3
    Venom,   // индекс 4
    BlackVoid // индекс 5
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
        if (Elements[i] > 0) // выводим только те стихии, где реально есть урон
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
    
  public override void PrintInfo()// override это запрос из item
    {
        // Вызываем базовый метод из Item, чтобы включить нужный цвет
        base.PrintInfo(); 
        
        // Добавляем уникальный текст для щита
        Console.WriteLine($"🛡️ Выпал щит: {Name} [{RarityRu}] | Урон: {Damage} | Защита: {Protection}");
        
        // Сбрасываем цвет после вывода текста
        Console.ResetColor();
    }
}
public class Archer : Item
{
    public Archer(int price, string name, ItemType type, Rarity rarity, double weight, double damage, double creatDamage, int protection) : base(price, name, type, rarity, weight, damage, creatDamage, protection)
    {}
    
  public override void PrintInfo()// override это запрос из item
    {
        // Вызываем базовый метод из Item, чтобы включить нужный цвет
        base.PrintInfo(); 
        
        // Добавляем уникальный текст для щита
        Console.WriteLine($" Выпал лук: {Name} [{RarityRu}] | Урон: {Damage} | Защита: {Protection}");
        
        // Сбрасываем цвет после вывода текста
        Console.ResetColor();
    }
}

public class Armor : Item
{
    string NameC { get; set; }
    public Armor(int price, string name, ItemType type, Rarity rarity, double weight, double damage, double creatDamage, int protection, string nameC) : base(price, name, type, rarity, weight, damage, creatDamage, protection)
    {
        NameC = nameC;
    }
    
  public override void PrintInfo()// override это запрос из item
    {
        // Вызываем базовый метод из Item, чтобы включить нужный цвет
        base.PrintInfo(); 
        
        // Добавляем уникальный текст для щита
        Console.WriteLine($"🛡️ Выпали доспехи: {Name} [{RarityRu}] | Защита: {Protection} | {NameC}");
        
        // Сбрасываем цвет после вывода текста
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
    
  public override void PrintInfo()// override это запрос из item
    {
        // Вызываем базовый метод из Item, чтобы включить нужный цвет
        base.PrintInfo(); 
        
        // Добавляем уникальный текст для щита
        Console.WriteLine($"📿 Выпали амулеты: {Name} [{RarityRu}] | Защита: {Protection} | урон: {Damage} | крит: {CreatDamage}");
        
        // Сбрасываем цвет после вывода текста
        Console.ResetColor();
    }
}
public class Hero
{
    public string Name { get; set; }
    public double MaxHP {  get; set; }
    public double CurrentHP {  get; set; }
    public int Level { get; set; }
    public int XP { get; set; }
    public int RequiredXP { get; set; }
    public double Damage { get; set; }
    public double CreatDamage { get; set; }
    public double SpeedDamage { get; set; }
    public int Protection { get; set; }
    public int Regent { get; set; }
    public Sword? EquippedSword { get; set; } 
    // Ячейка для брони
    public Armor? EquippedArmor { get; set; }
    public Amulet? EquippedAmulet { get; set; }
    public Staff? EquippedMagicStaff { get; set; }
     public Shield? EquippedShield { get; set; }
     public Archer? EquippedBow { get; set; }
    public Hero(string name, double maxHP, double currentHP, int level, int xp, int requiredXP, double damage, double creatDamage, double speedDamage, int protection, int regent)
    {
        Name = name;
        MaxHP = maxHP;
        CurrentHP = currentHP;
        Level = level;
        XP = xp;
        RequiredXP = requiredXP;
        Damage = damage;
        CreatDamage = creatDamage;
        SpeedDamage = speedDamage;
        Protection = protection;
        Regent = regent;
    }
    public void TakeDamage(double rawDamage)
    {
        // Защита снижает входящий урон (например, каждый 1 пункт защиты срезает 1 урона)
    double actualDamage = rawDamage - (Protection * 1);
    if (actualDamage < 1) actualDamage = 1; // Минимальный урон всегда хотя бы 1

    CurrentHP -= actualDamage;
    if (CurrentHP < 0) CurrentHP = 0;

    Console.WriteLine($"💥 {Name} получил {actualDamage:F1} урона! Осталось HP: {CurrentHP:F1}/{MaxHP}");
    }
    public Sword? EquipSword(Sword newSword)
{
    Sword? oldSword = EquippedSword; 

    if (oldSword != null)
    {
        // Снимаем характеристики старого меча
        Damage -= oldSword.Damage;
        CreatDamage -= oldSword.CreatDamage;
        Console.WriteLine($"🔻 {Name} снял {oldSword.Name}.");
    }

    // 2. Надеваем новый меч
    EquippedSword = newSword;
    Damage += newSword.Damage;
    CreatDamage += newSword.CreatDamage;
    Console.WriteLine($"⚔️ {Name} надел меч {newSword.Name}! Теперь урон: {Damage} | Крит: {CreatDamage}");

    // 3. Возвращаем снятый меч обратно!
    return oldSword;
}
// 🛡️ ЩИТ
    public Shield? EquipShield(Shield newShield)
    {
        Shield? oldShield = EquippedShield;
        if (oldShield != null)
        {
            Damage -= oldShield.Damage;
            CreatDamage -= oldShield.CreatDamage;
            Protection -= oldShield.Protection;
            Console.WriteLine($"🔻 {Name} снял {oldShield.Name}.");
        }

        EquippedShield = newShield;
        Damage += newShield.Damage;
        CreatDamage += newShield.CreatDamage;
        Protection += newShield.Protection;
        Console.WriteLine($"🛡️ {Name} экипировал щит {newShield.Name}!");
        return oldShield;
    }

    // 🎽 БРОНЯ
    public Armor? EquipArmor(Armor newArmor)
    {
        Armor? oldArmor = EquippedArmor;
        if (oldArmor != null)
        {
            Damage -= oldArmor.Damage;
            CreatDamage -= oldArmor.CreatDamage;
            Protection -= oldArmor.Protection;
            Console.WriteLine($"🔻 {Name} снял {oldArmor.Name}.");
        }

        EquippedArmor = newArmor;
        Damage += newArmor.Damage;
        CreatDamage += newArmor.CreatDamage;
        Protection += newArmor.Protection;
        Console.WriteLine($"🎽 {Name} надел броню {newArmor.Name}!");
        return oldArmor;
    }

    // 🔮 ПОСОХ
    public Staff? EquipStaff(Staff newStaff)
    {
        Staff? oldStaff = EquippedMagicStaff;
        if (oldStaff != null)
        {
            Damage -= oldStaff.Damage;
            CreatDamage -= oldStaff.CreatDamage;
            Protection -= oldStaff.Protection;
            Console.WriteLine($"🔻 {Name} убрал {oldStaff.Name}.");
        }

        EquippedMagicStaff = newStaff;
        Damage += newStaff.Damage;
        CreatDamage += newStaff.CreatDamage;
        Protection += newStaff.Protection;
        Console.WriteLine($"🔮 {Name} взял посох {newStaff.Name}!");
        return oldStaff;
    }

    // 🏹 ЛУК
    public Archer? EquipBow(Archer newBow)
    {
        Archer? oldBow = EquippedBow;
        if (oldBow != null)
        {
            Damage -= oldBow.Damage;
            CreatDamage -= oldBow.CreatDamage;
            Protection -= oldBow.Protection;
            Console.WriteLine($"🔻 {Name} снял {oldBow.Name}.");
        }

        EquippedBow = newBow;
        Damage += newBow.Damage;
        CreatDamage += newBow.CreatDamage;
        Protection += newBow.Protection;
        Console.WriteLine($"🏹 {Name} взял лук {newBow.Name}!");
        return oldBow;
    }

    // 📿 АМУЛЕТ
    public Amulet? EquipAmulet(Amulet newAmulet)
    {
        Amulet? oldAmulet = EquippedAmulet;
        if (oldAmulet != null)
        {
            Damage -= oldAmulet.Damage;
            CreatDamage -= oldAmulet.CreatDamage;
            Protection -= oldAmulet.Protection;
            Console.WriteLine($"🔻 {Name} снял {oldAmulet.Name}.");
        }

        EquippedAmulet = newAmulet;
        Damage += newAmulet.Damage;
        CreatDamage += newAmulet.CreatDamage;
        Protection += newAmulet.Protection;
        Console.WriteLine($"📿 {Name} надел амулет {newAmulet.Name}!");
        return oldAmulet;
    }
}

public class Inventory
{
    public int Gold { get; set; } = 100000;
    private List<Item> items = new List<Item>(); // Наш скрытый список предметов

    // Метод для добавления предмета в инвентарь
    public void AddItem(Item item)
    {
        items.Add(item);
        Console.WriteLine($"📦 Предмет {item.Name} добавлен в инвентарь!");
    }
    // Метод показа всех предметов
    public void ShowInventory()
    {
        Console.WriteLine("\n--- 🎒 ВАШ ИНВЕНТАРЬ ---");
        
        if (items.Count == 0)
        {
            Console.WriteLine("Инвентарь пуст!");
            return;
        }

        foreach (Item item in items)
        {
            item.PrintInfo(); // Вызываем родной метод предмета для красивого вывода!
        }
        
        Console.WriteLine("------------------------\n");
    }
    public void AddItems(List<Item> newItems)
    {
        items.AddRange(newItems); // Добавляет все элементы из newItems в наш список items!
        Console.WriteLine($"📦 Добавлено предметов: {newItems.Count} шт.");
    }
    public void SellItem(Item item)
{
    
    Gold += item.Price;// 1. Увеличиваем золото игрока на цену предмета
    
    items.Remove(item);// 2. Удаляем предмет из списка items
    
    Console.WriteLine($"💰 Вы продали {item.Name}!");
}
public void SellMenu()
    {
// 1. СРАЗУ проверяем пустоту
    if (items.Count == 0)
    {
        Console.WriteLine("Инвентарь пуст, продавать нечего!");
        return; // Команда return завершает работу метода прямо здесь!
    }
    // 2. Если мы дошли сюда, значит предметы есть!
   for (int i = 0; i < items.Count; i++)
{
    items[i].SetColor(); // Вызываем твой готовый метод!
    Console.WriteLine($"{i + 1}. {items[i].Name} ({items[i].Price} 🪙)");
    Console.ResetColor();
}
    // 3. Спрашиваем номер предмета
    Console.Write("Введите номер предмета для продажи: ");
    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        int index = choice - 1;

        // 4. Твоя правильная проверка на индекс
        if (index >= 0 && index < items.Count)
        {
            SellItem(items[index]);
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
        Console.WriteLine($"Герой: {myHero.Name} | Урон: {myHero.Damage} | Крит: {myHero.CreatDamage}");
        Console.WriteLine($"Золото: {Gold} 🪙");
        Console.WriteLine("------------------------------");
        Console.WriteLine("1. Посмотреть инвентарь / надеть предмет");
        Console.WriteLine("2. Продать предметы");
        Console.WriteLine("3. Выйти в главное меню");
        Console.Write("Выберите пункт: ");

        string input = Console.ReadLine()!;

        if (input == "1")
        {
            EquipMenu(myHero);
            Console.WriteLine("1. Оружие");
        Console.WriteLine("2. Щиты");
        Console.WriteLine("3. Амулет");
        Console.WriteLine("4. Броня");
        Console.WriteLine("5. Продать предмет");
        Console.WriteLine("6. Выйти в главное меню");
        Console.Write("Выберите пункт: ");
        string userinput = Console.ReadLine()!;
        if (userinput == "1")
        {
           var swords = items.Where(i => i.Type == ItemType.Sword).ToList();
           var staffs = items.Where(i => i.Type == ItemType.Staff).ToList();
           var archers = items.Where(i => i.Type == ItemType.Archer).ToList();
        }
        else if (userinput == "2")
        {
           var shield = items.Where(i => i.Type == ItemType.Shield).ToList();
        }
        else if (userinput == "3")
        {
           var amulet = items.Where(i => i.Type == ItemType.Amulet).ToList();
        }
        else if (userinput == "4")
        {
           var armors = items.Where(i => i.Type == ItemType.Armor).ToList();
        }
        else if (userinput == "5")
        {
            SellMenu();
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        else if (userinput == "6")
        {
            isOpen = false;
        }
        }
        else if (input == "2")
        {
            isOpen = false;
        }
    }
    }
public void EquipMenu(Hero myHero) // Передаем героя в метод
{
    // 1. ПРОВЕРКА НА ПУСТОТУ: 
    if (items.Count == 0)
    {
        Console.WriteLine("Инвентарь пуст!");
        return;
    }

    for (int i = 0; i < items.Count; i++)
{
    items[i].SetColor(); // Вызываем твой готовый метод!
    Console.WriteLine($"{i + 1}. {items[i].Name} ({items[i].Price} 🪙)");
    Console.ResetColor();
}

    Console.Write("Введите номер предмета, чтобы надеть его: ");
    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        int index = choice - 1;

        if (index >= 0 && index < items.Count) 
        {
            Item selectedItem = items[index];
            Item? oldItem = null;
            bool isEquipped = true;

            // Определяем тип и вызываем соответствующий метод экипировки
            if (selectedItem is Sword sword)
            {
                oldItem = myHero.EquipSword(sword);
            }
            else if (selectedItem is Shield shield)
            {
                oldItem = myHero.EquipShield(shield);
            }
            else if (selectedItem is Armor armor)
            {
                oldItem = myHero.EquipArmor(armor);
            }
            else if (selectedItem is Staff staff)
            {
                oldItem = myHero.EquipStaff(staff);
            }
            else if (selectedItem is Archer bow)
            {
                oldItem = myHero.EquipBow(bow);
            }
            else if (selectedItem is Amulet amulet)
            {
                oldItem = myHero.EquipAmulet(amulet);
            }
            else
            {
                isEquipped = false;
                Console.WriteLine("Этот предмет нельзя надеть!");
            }

            // Если предмет успешно надет — переносим вещи в инвентаре
            if (isEquipped)
            {
                items.Remove(selectedItem);

                if (oldItem != null)
                {
                    items.Add(oldItem);
                    Console.WriteLine($"🎒 {oldItem.Name} возвращён в инвентарь.");
                }

                Console.WriteLine($"\nТекущие статы: Урон: {myHero.Damage} | Крит: {myHero.CreatDamage} | Защита: {myHero.Protection}");
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Данного предмета не найдено.");
        }
    }
}
}

public class Monster
{
    public string Name { get; set; }
    public double MaxHP { get; set; } 
    public double CurrentHP { get; set; }
    public int RewardGold { get; set; }// сколько золота выпадает с монстра.
    public int RewardXP { get; set; }// сколько опыта даёт монстр.
    public double Damage { get; set; }
    public double CreatDamage { get; set; }
    public double SpeedDamage { get; set; }
    public int Protection { get; set; }
    public int Regent { get; set; }
    // 2. СЛОТЫ ДЛЯ ЭКИПИРОВКИ (новое!):
    // Эта ячейка хранит надетый меч. 
    // Если ничего не надето, тут будет пустота (в C# это называется null)
    public Sword? EquippedSword { get; set; } 
    // Ячейка для брони
    public Armor? EquippedArmor { get; set; }
    public Amulet? EquippedAmulet { get; set; }
    public Staff? EquippedMagicStaff { get; set; }     
    public Shield? EquippedShield { get; set; }// ? этот знак нужен для того чтобы успокоить комп
    public Monster(string name, double maxHP, int rewardGold, int rewardXP, double damage, double creatDamage, double speedDamage, int protection, int regent)
    {
        Name = name;
        MaxHP = maxHP;
        CurrentHP = maxHP; // При создании монстр всегда со 100% HP
        RewardGold = rewardGold;
        RewardXP = rewardXP;
        Damage = damage;
        CreatDamage = creatDamage;
        SpeedDamage = speedDamage;
        Protection = protection;
        Regent = regent;
    }
    public void TakeDamage(double rawDamage)
    {
        double actualDamage = rawDamage - (Protection * 1);
        if (actualDamage < 1) actualDamage = 1; // Минимальный урон всегда хотя бы 1
        CurrentHP -= actualDamage;
        if (CurrentHP < 0) CurrentHP = 0;
        Console.WriteLine($"⚔️ {Name} получил {actualDamage:F1} урона! Осталось HP: {CurrentHP:F1}/{MaxHP}");
    }
}
class Arena
{
    public static void StartBattle(Hero hero, Inventory inventory)
    {
        Monster goblin = new Monster("Лесной Гоблин", 150, 200, 10, 20, 40, 0, 0, 0);

        Console.WriteLine($"⚔️ На вас напал {goblin.Name}!");

        // 1. ЦИКЛ БОЯ (крутится, пока ОБА живы)
        while (hero.CurrentHP > 0 && goblin.CurrentHP > 0)
        {
            Console.WriteLine($"\nHP {hero.Name}: {hero.CurrentHP} | HP {goblin.Name}: {goblin.CurrentHP}");
            Console.WriteLine("Выберите действие:\n1. Атаковать\n2. Сбежать");
            
            string choice = Console.ReadLine()!;

            if (choice == "1")
            {
                goblin.TakeDamage(hero.Damage);

                // Если гоблин ВЫЖИЛ после нашего удара — он бьёт в ответ
                if (goblin.CurrentHP > 0)
                {
                    hero.TakeDamage(goblin.Damage);
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine("🏃 Вы успешно сбежали!");
                break; // Выходим из цикла боя
            }
        }

        // 2. ИТОГИ БОЯ (срабатывают ПОСЛЕ окончания цикла)
        if (goblin.CurrentHP <= 0)
        {
            Console.WriteLine($"🎉 Поздравляю с победой! Вы получили: {goblin.RewardGold} 🪙");
            inventory.Gold += goblin.RewardGold;
        }
        else if (hero.CurrentHP <= 0)
        {
            Console.WriteLine("☠️ Вы проиграли в бою...");
        }

        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}