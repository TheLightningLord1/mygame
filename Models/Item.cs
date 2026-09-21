using System;
using Ruletka.Models;

namespace Ruletka.Models;

public class Item
{
    public string Name { get; set; } = "";
    public ItemType Type { get; set; }
    public Rarity Rarity { get; set; }
    public double Price { get; set; }
    public double Damage { get; set; }
    public double CreatDamage { get; set; }
    public int Protection { get; set; }
    public int Regent { get; set; }
    public double MaxDurability { get; set; }
    public double CurrentDurability { get; set; }
    public int RepairCost => (int)(Price * 0.1);

    public Item() { }

    public Item(double price, string name, ItemType type, Rarity rarity, double maxDurability, double damage, double creatDamage, int protection, int regent, double currentDurability = 0)
    {
        Price = price;
        Name = name;
        Type = type;
        Rarity = rarity;
        MaxDurability = maxDurability;
        CurrentDurability = currentDurability > 0 ? currentDurability : maxDurability;
        Damage = damage;
        CreatDamage = creatDamage;
        Protection = protection;
        Regent = regent;
    }

    public virtual void ApplyBonus(Hero hero)
    {
        hero.Damage += Damage;
        hero.CreatDamage += CreatDamage;
    }

    public virtual void RemoveBonus(Hero hero)
    {
        hero.Damage -= Damage;
        hero.CreatDamage -= CreatDamage;
    }

    public bool IsBroken() => CurrentDurability <= 0;

    public void ReduceDurability(double amount = 1)
    {
        CurrentDurability -= amount;
        if (CurrentDurability < 0) CurrentDurability = 0;
    }
}
