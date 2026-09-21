using System;
using System.Collections.Generic;
using System.Linq;
using Ruletka.Models;

namespace Ruletka.Models;

public class Hero
{
    public Item? EquippedWeapon { get; set; }
    public string Name { get; set; } = "";
    public HeroClass Class { get; set; }
    public double MaxHP { get; set; }
    public double CurrentHP { get; set; }
    public int Level { get; set; }
    public int XP { get; set; }
    public int RequiredXP { get; set; }
    public double Damage { get; set; }
    public double CreatDamage { get; set; }
    public double SpeedDamage { get; set; }
    public double BaseProtection { get; set; } 
    public int Regent { get; set; }

    public Dictionary<ArmorSlot, Armor?> EquippedArmorSlots { get; set; }
    public Sword? EquippedSword { get; set; } 
    public Amulet? EquippedAmulet { get; set; }
    public Staff? EquippedMagicStaff { get; set; }
    public Shield? EquippedShield { get; set; }
    public Archer? EquippedBow { get; set; }

    // Боевая система
    public int SkillCooldown { get; set; } = 0;
    public int ComboCount { get; set; } = 0;
    public double DodgeChance { get; set; } = 5.0;
    public double BlockChance { get; set; } = 10.0;
    
    // Статусные эффекты
    public Dictionary<StatusEffect, int> ActiveEffects { get; set; } = new();

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
        Regent = regent;
        EquippedArmorSlots = new Dictionary<ArmorSlot, Armor?>()
        {
            { ArmorSlot.Helmet, null },
            { ArmorSlot.Chestplate, null },
            { ArmorSlot.Leggings, null },
            { ArmorSlot.Gloves, null }
        };
    }

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

    public double TotalDamage
    {
        get
        {
            double total = Damage;
            if (EquippedWeapon != null && !EquippedWeapon.IsBroken())
                total += EquippedWeapon.Damage;
            if (EquippedShield != null && !EquippedShield.IsBroken())
                total += EquippedShield.Damage;
            if (EquippedAmulet != null && !EquippedAmulet.IsBroken())
                total += EquippedAmulet.Damage;
            if (EquippedMagicStaff != null && Class == HeroClass.Mage && !EquippedMagicStaff.IsBroken())
                total += EquippedMagicStaff.Elements.Sum();
            return total;
        }
    }

    public double TotalCreatDamage
    {
        get
        {
            double total = CreatDamage;
            if (EquippedWeapon != null && !EquippedWeapon.IsBroken())
                total += EquippedWeapon.CreatDamage;
            if (EquippedShield != null && !EquippedShield.IsBroken())
                total += EquippedShield.CreatDamage;
            if (EquippedAmulet != null && !EquippedAmulet.IsBroken())
                total += EquippedAmulet.CreatDamage;
            return total;
        }
    }

    public string ActiveSetBonusName => "Нет активного сета"; // Можно добавить логику сетов

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

    public void DecrementCooldown()
    {
        if (SkillCooldown > 0)
        {
            SkillCooldown--;
        }
        
        // Обработка статусных эффектов
        ProcessStatusEffects();
    }

    public void ApplyStatusEffect(StatusEffect effect, int duration)
    {
        if (ActiveEffects.ContainsKey(effect))
            ActiveEffects[effect] = duration;
        else
            ActiveEffects.Add(effect, duration);
    }

    public void ProcessStatusEffects()
    {
        var toRemove = new List<StatusEffect>();
        
        foreach (var kvp in ActiveEffects)
        {
            var effect = kvp.Key;
            var remainingDuration = kvp.Value - 1;
            
            switch (effect)
            {
                case StatusEffect.Burn:
                    CurrentHP -= MaxHP * 0.05;
                    Console.WriteLine($"🔥 {Name} получает урон от горения! -{MaxHP * 0.05:F1} HP");
                    break;
                case StatusEffect.Poison:
                    CurrentHP -= MaxHP * 0.03;
                    Console.WriteLine($"☠️ {Name} получает урон от яда! -{MaxHP * 0.03:F1} HP");
                    break;
                case StatusEffect.Regeneration:
                    CurrentHP = Math.Min(MaxHP, CurrentHP + MaxHP * 0.02);
                    Console.WriteLine($"💚 {Name} восстанавливает здоровье! +{MaxHP * 0.02:F1} HP");
                    break;
            }
            
            if (remainingDuration <= 0)
                toRemove.Add(effect);
            else
                ActiveEffects[effect] = remainingDuration;
        }
        
        foreach (var effect in toRemove)
            ActiveEffects.Remove(effect);
            
        if (CurrentHP < 0) CurrentHP = 0;
    }

    public bool HasStatusEffect(StatusEffect effect) => ActiveEffects.ContainsKey(effect);

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
            EquippedWeapon = null;
        }

        if (EquippedShield != null)
        {
            EquippedShield = null;
        }

        if (EquippedAmulet != null)
        {
            EquippedAmulet = null;
        }

        EquippedArmorSlots[ArmorSlot.Helmet] = null;
        EquippedArmorSlots[ArmorSlot.Chestplate] = null;
        EquippedArmorSlots[ArmorSlot.Leggings] = null;
        EquippedArmorSlots[ArmorSlot.Gloves] = null;
    }

    public void TakeDamage(double rawDamage)
    {
        // Проверка на уклонение
        Random random = new Random();
        if (random.Next(100) < DodgeChance)
        {
            Console.WriteLine($"💨 {Name} увернулся от атаки!");
            return;
        }
        
        // Проверка на блок
        if (random.Next(100) < BlockChance && EquippedShield != null && !EquippedShield.IsBroken())
        {
            double blockedDamage = rawDamage * 0.5;
            rawDamage -= blockedDamage;
            Console.WriteLine($"🛡️ {Name} заблокировал часть урона щитом! -{blockedDamage:F1}");
        }
        
        double actualDamage = rawDamage - Protection;
        if (actualDamage < 1) actualDamage = 1;
        
        CurrentHP -= actualDamage;
        if (CurrentHP < 0) CurrentHP = 0;
        
        Console.WriteLine($"💥 {Name} получил {actualDamage:F1} урона! Осталось HP: {CurrentHP:F1}/{MaxHP}");
    }

    public void AddXP(int xp)
    {
        XP += xp;
        if (XP >= RequiredXP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Level++;
        XP -= RequiredXP;
        RequiredXP = (int)(RequiredXP * 1.5);
        MaxHP += 20;
        CurrentHP = MaxHP;
        Damage += 5;
        CreatDamage += 2;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n🎉 НОВЫЙ УРОВЕНЬ! Теперь вы {Level} уровня!");
        Console.ResetColor();
    }

    public void ReduceDurabilityAfterBattle()
    {
        if (EquippedWeapon != null) EquippedWeapon.ReduceDurability(1);
        if (EquippedShield != null) EquippedShield.ReduceDurability(1);
        if (EquippedAmulet != null) EquippedAmulet.ReduceDurability(1);
        foreach (var armor in EquippedArmorSlots.Values)
        {
            if (armor != null) armor.ReduceDurability(1);
        }
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
                Console.WriteLine($"🛡️ {Name} выпил зелье защиты! Базовая защита увеличена на +{consumable.Power}");
                break;

            case ConsumableEffect.Magic:
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
}
