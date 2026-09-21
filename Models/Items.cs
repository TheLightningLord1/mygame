using System;
using Ruletka.Models;

namespace Ruletka.Models;

public class Sword : Item
{
    public Sword() { Type = ItemType.Sword; }

    public Sword(double price, string name, ItemType type, Rarity rarity, double maxDurability, double damage, double creatDamage, int protection, int regent, double currentDurability = 0)
        : base(price, name, type, rarity, maxDurability, damage, creatDamage, protection, regent, currentDurability)
    {
        Type = ItemType.Sword;
    }
}

public class Staff : Item
{
    public enum Element
    {
        Water,
        Fire,
        Ice,
        Light,
        BlackVoid
    }

    public int[] Elements { get; set; } = new int[5]; // [Water, Fire, Ice, Light, BlackVoid]

    public Staff() { Type = ItemType.Staff; }

    public Staff(double price, string name, ItemType type, Rarity rarity, double maxDurability, double damage, double creatDamage, int protection, int regent, int[]? elements = null, double currentDurability = 0)
        : base(price, name, type, rarity, maxDurability, damage, creatDamage, protection, regent, currentDurability)
    {
        Type = ItemType.Staff;
        if (elements != null)
            Elements = elements;
    }

    public override void ApplyBonus(Hero hero)
    {
        base.ApplyBonus(hero);
        if (hero.Class == HeroClass.Mage)
        {
            int elementalBonus = Elements.Sum();
            hero.Damage += elementalBonus;
        }
    }

    public override void RemoveBonus(Hero hero)
    {
        base.RemoveBonus(hero);
        if (hero.Class == HeroClass.Mage)
        {
            int elementalBonus = Elements.Sum();
            hero.Damage -= elementalBonus;
        }
    }
}

public class Shield : Item
{
    public Shield() { Type = ItemType.Shield; }

    public Shield(double price, string name, ItemType type, Rarity rarity, double maxDurability, double damage, double creatDamage, int protection, int regent, double currentDurability = 0)
        : base(price, name, type, rarity, maxDurability, damage, creatDamage, protection, regent, currentDurability)
    {
        Type = ItemType.Shield;
    }
}

public class Archer : Item
{
    public Archer() { Type = ItemType.Archer; }

    public Archer(double price, string name, ItemType type, Rarity rarity, double maxDurability, double damage, double creatDamage, int protection, int regent, double currentDurability = 0)
        : base(price, name, type, rarity, maxDurability, damage, creatDamage, protection, regent, currentDurability)
    {
        Type = ItemType.Archer;
    }
}

public class Armor : Item
{
    public ArmorSlot Slot { get; set; }

    public Armor() { Type = ItemType.Armor; }

    public Armor(double price, string name, ItemType type, Rarity rarity, double maxDurability, double damage, double creatDamage, int protection, int regent, ArmorSlot slot, double currentDurability = 0)
        : base(price, name, type, rarity, maxDurability, damage, creatDamage, protection, regent, currentDurability)
    {
        Type = ItemType.Armor;
        Slot = slot;
    }

    public override void ApplyBonus(Hero hero)
    {
        base.ApplyBonus(hero);
        // Бонусы от сетов можно добавить здесь
    }
}

public class Amulet : Item
{
    public Amulet() { Type = ItemType.Amulet; }

    public Amulet(double price, string name, ItemType type, Rarity rarity, double maxDurability, double damage, double creatDamage, int protection, int regent, double currentDurability = 0)
        : base(price, name, type, rarity, maxDurability, damage, creatDamage, protection, regent, currentDurability)
    {
        Type = ItemType.Amulet;
    }
}

public class Consumable : Item
{
    public ConsumableEffect Effect { get; set; }
    public double Power { get; set; }

    public Consumable() { Type = ItemType.Consumable; }

    public Consumable(double price, string name, ItemType type, Rarity rarity, double maxDurability, ConsumableEffect effect, double power, double currentDurability = 0)
        : base(price, name, type, rarity, maxDurability, 0, 0, 0, 0, currentDurability)
    {
        Type = ItemType.Consumable;
        Effect = effect;
        Power = power;
    }
}
