using System;
using System.Collections.Generic;
using Ruletka.Models;

namespace Ruletka.Data;

public static class ItemDatabase
{
    public static List<Item> GetSwords()
    {
        return new List<Item>
        {
            new Sword(1000, "Ржавый меч", ItemType.Sword, Rarity.Common, 100, 15, 5, 0, 0),
            new Sword(2500, "Стальной клинок", ItemType.Sword, Rarity.Uncommon, 150, 25, 10, 2, 0),
            new Sword(5000, "Закалённый меч", ItemType.Sword, Rarity.Rare, 200, 40, 20, 5, 0),
            new Sword(10000, "Мифриловый клинок", ItemType.Sword, Rarity.Epic, 300, 65, 35, 10, 0),
            new Sword(25000, "Легендарный меч героя", ItemType.Sword, Rarity.Legendary, 500, 100, 60, 20, 0)
        };
    }

    public static List<Item> GetArcher()
    {
        return new List<Item>
        {
            new Archer(1000, "Простой лук", ItemType.Archer, Rarity.Common, 100, 12, 8, 0, 0),
            new Archer(2500, "Охотничий лук", ItemType.Archer, Rarity.Uncommon, 150, 20, 15, 0, 0),
            new Archer(5000, "Эльфийский лук", ItemType.Archer, Rarity.Rare, 200, 35, 25, 0, 0),
            new Archer(10000, "Лук теней", ItemType.Archer, Rarity.Epic, 300, 55, 40, 5, 0),
            new Archer(25000, "Драконий лук", ItemType.Archer, Rarity.Legendary, 500, 85, 65, 10, 0)
        };
    }

    public static List<Item> GetStaff()
    {
        return new List<Item>
        {
            new Staff(1000, "Деревянный посох", ItemType.Staff, Rarity.Common, 80, 8, 3, 0, 0, new int[] { 5, 0, 0, 0, 0 }),
            new Staff(2500, "Посох ученика", ItemType.Staff, Rarity.Uncommon, 120, 15, 8, 0, 0, new int[] { 10, 5, 0, 0, 0 }),
            new Staff(5000, "Магический посох", ItemType.Staff, Rarity.Rare, 180, 25, 15, 0, 0, new int[] { 15, 10, 10, 0, 0 }),
            new Staff(10000, "Посох архимага", ItemType.Staff, Rarity.Epic, 250, 40, 25, 5, 0, new int[] { 25, 20, 15, 10, 0 }),
            new Staff(25000, "Посох вечности", ItemType.Staff, Rarity.Legendary, 400, 60, 40, 10, 0, new int[] { 40, 35, 30, 25, 20 })
        };
    }

    public static List<Item> GetShield()
    {
        return new List<Item>
        {
            new Shield(800, "Деревянный щит", ItemType.Shield, Rarity.Common, 120, 5, 0, 15, 0),
            new Shield(2000, "Кожаный щит", ItemType.Shield, Rarity.Uncommon, 180, 8, 2, 25, 0),
            new Shield(4500, "Железный щит", ItemType.Shield, Rarity.Rare, 250, 12, 5, 40, 0),
            new Shield(9000, "Стальной бастион", ItemType.Shield, Rarity.Epic, 350, 18, 10, 60, 0),
            new Shield(22000, "Щит дракона", ItemType.Shield, Rarity.Legendary, 500, 25, 15, 85, 0)
        };
    }

    public static List<Item> GetArmor()
    {
        var slots = new[] { ArmorSlot.Helmet, ArmorSlot.Chestplate, ArmorSlot.Leggings, ArmorSlot.Gloves };
        var names = new[] { "Шлем", "Нагрудник", "Поножи", "Перчатки" };
        var list = new List<Item>();

        for (int i = 0; i < slots.Length; i++)
        {
            list.Add(new Armor(600, $"{names[i]} новичка", ItemType.Armor, Rarity.Common, 100, 3, 0, 10, 0, slots[i]));
            list.Add(new Armor(1500, $"{names[i]} воина", ItemType.Armor, Rarity.Uncommon, 150, 6, 2, 20, 0, slots[i]));
            list.Add(new Armor(3500, $"{names[i]} рыцаря", ItemType.Armor, Rarity.Rare, 220, 10, 5, 35, 0, slots[i]));
            list.Add(new Armor(7000, $"{names[i]} чемпиона", ItemType.Armor, Rarity.Epic, 320, 16, 10, 55, 0, slots[i]));
            list.Add(new Armor(18000, $"{names[i]} легенды", ItemType.Armor, Rarity.Legendary, 450, 25, 18, 80, 0, slots[i]));
        }

        return list;
    }

    public static List<Item> GetAmulet()
    {
        return new List<Item>
        {
            new Amulet(500, "Амулет удачи", ItemType.Amulet, Rarity.Common, 200, 5, 3, 5, 0),
            new Amulet(1200, "Амулет силы", ItemType.Amulet, Rarity.Uncommon, 250, 10, 6, 8, 0),
            new Amulet(3000, "Амулет мощи", ItemType.Amulet, Rarity.Rare, 300, 18, 12, 12, 0),
            new Amulet(6500, "Амулет титана", ItemType.Amulet, Rarity.Epic, 400, 30, 20, 18, 0),
            new Amulet(15000, "Амулет бога", ItemType.Amulet, Rarity.Legendary, 550, 50, 35, 30, 0)
        };
    }

    public static List<Monster> GetMonstersByDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 1: // Лес - легко
                return new List<Monster>
                {
                    new Monster("Гоблин", 50, 50, 30, 8, 3, 10, 5, 0, MonsterElement.None),
                    new Monster("Волк", 70, 70, 40, 12, 5, 15, 8, 0, MonsterElement.None),
                    new Monster("Разбойник", 90, 90, 50, 15, 8, 20, 10, 0, MonsterElement.None),
                    new Monster("Ядовитый паук", 60, 60, 60, 10, 4, 12, 3, 0, MonsterElement.Venom)
                };

            case 2: // Пещера - средне
                return new List<Monster>
                {
                    new Monster("Скелет", 120, 120, 80, 20, 10, 15, 15, 0, MonsterElement.Dark),
                    new Monster("Орк", 180, 180, 100, 28, 12, 20, 20, 0, MonsterElement.None),
                    new Monster("Тролль", 250, 250, 120, 35, 15, 25, 30, 5, MonsterElement.None),
                    new Monster("Огненный элементаль", 150, 150, 110, 30, 18, 22, 10, 0, MonsterElement.Fire)
                };

            case 3: // Замок Тьмы - сложно
                return new List<Monster>
                {
                    new Monster("Рыцарь смерти", 300, 300, 180, 45, 25, 30, 40, 0, MonsterElement.Dark),
                    new Monster("Тёмный маг", 200, 200, 200, 55, 35, 25, 15, 0, MonsterElement.Dark),
                    new Monster("Драконид", 350, 350, 220, 50, 20, 35, 45, 0, MonsterElement.Fire),
                    new Monster("Ледяной голем", 400, 400, 190, 40, 15, 20, 55, 0, MonsterElement.Ice)
                };

            case 4: // Бездна - очень сложно
                return new List<Monster>
                {
                    new Monster("Демон-повелитель", 500, 500, 350, 70, 40, 45, 50, 10, MonsterElement.Dark),
                    new Monster("Древний дракон", 700, 700, 400, 85, 50, 50, 70, 15, MonsterElement.Fire),
                    new Monster("Король личей", 450, 450, 380, 75, 45, 40, 45, 10, MonsterElement.Dark),
                    new Monster("Пустошный ужас", 600, 600, 420, 80, 48, 48, 60, 10, MonsterElement.Blad)
                };

            default:
                return new List<Monster>();
        }
    }
}
