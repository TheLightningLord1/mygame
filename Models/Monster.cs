using System;
using System.Collections.Generic;
using Ruletka.Models;

namespace Ruletka.Models;

public class Monster
{
    public string Name { get; set; } = "";
    public double MaxHP { get; set; } 
    public double CurrentHP { get; set; }
    public int RewardGold { get; set; }
    public int RewardXP { get; set; }
    public double Damage { get; set; }
    public double CreatDamage { get; set; }
    public double SpeedDamage { get; set; }
    public int Protection { get; set; }
    public int Regent { get; set; }
    public MonsterElement Element { get; set; }

    // Статусные эффекты
    public Dictionary<StatusEffect, int> ActiveEffects { get; set; } = new();

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
            
            // Применяем эффект
            switch (effect)
            {
                case StatusEffect.Burn:
                    CurrentHP -= MaxHP * 0.05; // 5% от макс HP
                    Console.WriteLine($"🔥 {Name} получает урон от горения! -{MaxHP * 0.05:F1} HP");
                    break;
                case StatusEffect.Poison:
                    CurrentHP -= MaxHP * 0.03; // 3% от макс HP
                    Console.WriteLine($"☠️ {Name} получает урон от яда! -{MaxHP * 0.03:F1} HP");
                    break;
                case StatusEffect.Freeze:
                    Console.WriteLine($"❄️ {Name} заморожен и пропускает ход!");
                    break;
                case StatusEffect.Stun:
                    Console.WriteLine($"💫 {Name} оглушен и пропускает ход!");
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
}

public enum StatusEffect
{
    None,
    Burn,       // Горение - урон каждый ход
    Poison,     // Яд - урон каждый ход
    Freeze,     // Заморозка - пропуск хода
    Stun,       // Оглушение - пропуск хода
    Bleed,      // Кровотечение - урон каждый ход
    Shield      // Щит - поглощение урона
}
