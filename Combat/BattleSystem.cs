using System;
using System.Collections.Generic;
using System.Linq;
using Ruletka.Models;

namespace Ruletka.Combat;

public class BattleSystem
{
    private static Random random = new Random();

    public static void StartBattle(Hero hero, Systems.Inventory inventory)
    {
        hero.CurrentHP = hero.MaxHP; 
        hero.SkillCooldown = 0;
        hero.ComboCount = 0;

        Console.Clear();
        Console.WriteLine("⚔️ === ВЫБОР ЛОКАЦИИ И СЛОЖНОСТИ ===");
        Console.WriteLine("1. Лес (Легко) | 2. Пещера (Средне) | 3. Замок Тьмы (Сложно) | 4. Бездна (Очень сложно)");
        Console.Write("Выбор: ");

        if (!int.TryParse(Console.ReadLine(), out int diffChoice) || diffChoice < 1 || diffChoice > 4) return;

        List<Monster> possibleMonsters = Data.ItemDatabase.GetMonstersByDifficulty(diffChoice);
        bool keepFighting = true; 

        while (keepFighting && hero.CurrentHP > 0)
        {
            Monster monster = possibleMonsters[random.Next(possibleMonsters.Count)];
            monster.CurrentHP = monster.MaxHP; 
            monster.ActiveEffects.Clear();

            Console.Clear();
            Console.WriteLine($"👹 Навстречу вышел: {monster.Name} [Стихия: {monster.Element} | HP: {monster.MaxHP} | Защита: {monster.Protection}]");

            while (hero.CurrentHP > 0 && monster.CurrentHP > 0)
            {
                Console.WriteLine($"\n❤️ HP {hero.Name}: {hero.CurrentHP:F1}/{hero.MaxHP} | 👹 HP {monster.Name}: {monster.CurrentHP:F1}/{monster.MaxHP}");
                
                string cdInfo = hero.SkillCooldown > 0 ? $" (КД: {hero.SkillCooldown} х.)" : " (ГОТОВО!)";
                string comboInfo = hero.ComboCount >= 2 ? $" 🔥 Комбо: x{hero.ComboCount}" : "";
                
                Console.WriteLine("\n=== БОЙ ===");
                Console.WriteLine($"1. ⚔️ Обычная атака{comboInfo}");
                Console.WriteLine($"2. ✨ Способность: {hero.SkillName}{cdInfo}");
                Console.WriteLine("3. 🧪 Использовать зелье");
                Console.WriteLine("4. ⚒️ Кузница (Ремонт предметов)");
                Console.WriteLine("5. 🛡️ Защитная стойка (+блок/уклонение)");
                Console.WriteLine("6. 🏃 Сбежать");

                string choice = Console.ReadLine()!;

                if (choice == "1")
                {
                    ExecuteHeroAttack(hero, monster, isSkill: false);
                    if (monster.CurrentHP > 0 && !monster.HasStatusEffect(StatusEffect.Stun) && !monster.HasStatusEffect(StatusEffect.Freeze))
                    {
                        ExecuteMonsterAttack(monster, hero);
                    }
                    hero.DecrementCooldown();
                    monster.ProcessStatusEffects();
                }
                else if (choice == "2")
                {
                    if (hero.SkillCooldown > 0)
                    {
                        Console.WriteLine($"❌ Способность еще перезаряжается! Осталось ходов: {hero.SkillCooldown}");
                        continue;
                    }

                    ExecuteHeroSkill(hero, monster);
                    if (monster.CurrentHP > 0 && !monster.HasStatusEffect(StatusEffect.Stun) && !monster.HasStatusEffect(StatusEffect.Freeze))
                    {
                        ExecuteMonsterAttack(monster, hero);
                    }
                    hero.SkillCooldown = hero.MaxSkillCooldown;
                    hero.DecrementCooldown();
                    monster.ProcessStatusEffects();
                }
                else if (choice == "3")
                {
                    if (inventory.UseConsumableInBattle(hero))
                    {
                        if (monster.CurrentHP > 0 && !monster.HasStatusEffect(StatusEffect.Stun) && !monster.HasStatusEffect(StatusEffect.Freeze))
                        {
                            ExecuteMonsterAttack(monster, hero);
                        }
                        hero.DecrementCooldown();
                        monster.ProcessStatusEffects();
                    }
                }
                else if (choice == "4")
                {
                    Systems.Blacksmith.OpenForge(hero, inventory);
                }
                else if (choice == "5")
                {
                    DefensiveStance(hero);
                    if (monster.CurrentHP > 0 && !monster.HasStatusEffect(StatusEffect.Stun) && !monster.HasStatusEffect(StatusEffect.Freeze))
                    {
                        ExecuteMonsterAttack(monster, hero);
                    }
                    hero.DecrementCooldown();
                    monster.ProcessStatusEffects();
                }
                else if (choice == "6")
                {
                    if (random.Next(100) < 50 + (hero.ComboCount * 5))
                    {
                        Console.WriteLine("🏃 Вы успешно сбежали!");
                        keepFighting = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("❌ Не удалось сбежать!");
                        ExecuteMonsterAttack(monster, hero);
                        hero.DecrementCooldown();
                        monster.ProcessStatusEffects();
                    }
                }
            }

            if (monster.CurrentHP <= 0)
            {
                Console.WriteLine($"\n🎉 {monster.Name} побеждён! Награда: {monster.RewardGold} 🪙 | Опыт: {monster.RewardXP} XP");
                inventory.Gold += monster.RewardGold;
                hero.AddXP(monster.RewardXP);
                hero.ReduceDurabilityAfterBattle();
                hero.ComboCount = 0; // Сброс комбо после боя
                Console.ReadKey();
            }
            else if (hero.CurrentHP <= 0)
            {
                Console.WriteLine($"\n☠️ Вы пали в бою...");
                hero.LoseAllEquipment();
                keepFighting = false;
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
            case HeroClass.Knight: // Мощный удар (2x урон + оглушение)
                double knightDmg = hero.TotalDamage * 2.0 * GetElementalMultiplier(hero, monster);
                monster.TakeDamage(knightDmg);
                if (random.Next(100) < 30)
                {
                    monster.ApplyStatusEffect(StatusEffect.Stun, 2);
                    Console.WriteLine("💫 Враг оглушен на 2 хода!");
                }
                break;

            case HeroClass.Paladin: // Святое исцеление + урон нежити
                double heal = hero.MaxHP * 0.5;
                hero.CurrentHP = Math.Min(hero.MaxHP, hero.CurrentHP + heal);
                Console.WriteLine($"✨ {hero.Name} восстановил {heal:F0} HP! Текущее здоровье: {hero.CurrentHP:F1}/{hero.MaxHP}");
                
                if (monster.Element == MonsterElement.Dark)
                {
                    double holyDmg = hero.TotalDamage * 1.5;
                    monster.TakeDamage(holyDmg);
                    Console.WriteLine("⚡ Дополнительный урон по нежити!");
                }
                break;

            case HeroClass.Mage: // Огненный шар + горение
                double mageDmg = (hero.TotalDamage + 40) * GetElementalMultiplier(hero, monster, forcedElement: Staff.Element.Fire);
                monster.TakeDamage(mageDmg, ignoreArmor: true);
                monster.ApplyStatusEffect(StatusEffect.Burn, 3);
                Console.WriteLine("🔥 Враг подожжен на 3 хода!");
                break;

            case HeroClass.Archer: // Меткий залп + кровотечение
                double archerDmg = (hero.TotalDamage + hero.TotalCreatDamage) * GetElementalMultiplier(hero, monster);
                monster.TakeDamage(archerDmg);
                monster.ApplyStatusEffect(StatusEffect.Bleed, 3);
                Console.WriteLine("🩸 Враг истекает кровью на 3 хода!");
                break;
        }
    }

    private static void ExecuteHeroAttack(Hero hero, Monster monster, bool isSkill)
    {
        // Система комбо
        hero.ComboCount++;
        double comboMultiplier = 1.0 + (hero.ComboCount * 0.1); // +10% за каждый удар комбо
        
        // Проверка на уклонение монстра
        if (random.Next(100) < 15)
        {
            Console.WriteLine($"💨 {monster.Name} увернулся от атаки!");
            hero.ComboCount = 0; // Сброс комбо при промахе
            return;
        }

        double elementalMultiplier = GetElementalMultiplier(hero, monster);
        double damage = hero.TotalDamage * elementalMultiplier * comboMultiplier;

        if (comboMultiplier > 1.0)
            Console.WriteLine($"🔥 Комбо множитель: x{comboMultiplier:F1}!");
            
        if (elementalMultiplier > 1.0)
            Console.WriteLine($"🔥 Преимущество стихии! Урон x{elementalMultiplier:F1}");
        else if (elementalMultiplier < 1.0)
            Console.WriteLine($"🛡️ Враг устойчив к стихии! Урон x{elementalMultiplier:F1}");

        // Критический удар
        bool isCrit = random.Next(100) < Math.Clamp(hero.SpeedDamage, 5, 90);
        if (isCrit)
        {
            damage += hero.TotalCreatDamage;
            damage *= 1.5; // Крит умножает урон
            Console.WriteLine($"⚡ КРИТИЧЕСКИЙ УДАР! x1.5");
        }

        monster.TakeDamage(damage);
    }

    private static void ExecuteMonsterAttack(Monster monster, Hero hero)
    {
        // Проверка на промах монстра
        if (random.Next(100) < 10)
        {
            Console.WriteLine($"💨 {monster.Name} промахнулся!");
            return;
        }

        double damage = monster.Damage;
        
        // Критический удар монстра
        if (random.Next(100) < monster.SpeedDamage)
        {
            damage += monster.CreatDamage;
            Console.WriteLine($"⚡ {monster.Name} нанес критический удар!");
        }

        hero.TakeDamage(damage);
    }

    private static void DefensiveStance(Hero hero)
    {
        double oldDodge = hero.DodgeChance;
        double oldBlock = hero.BlockChance;
        
        hero.DodgeChance += 15;
        hero.BlockChance += 20;
        
        Console.WriteLine($"🛡️ {hero.Name} встал в защитную стойку! Уклонение +15%, Блок +20%");
        
        // Восстанавливаем значения после одного хода
        hero.DodgeChance = oldDodge;
        hero.BlockChance = oldBlock;
    }

    private static double GetElementalMultiplier(Hero hero, Monster monster, Staff.Element? forcedElement = null)
    {
        if (monster.Element == MonsterElement.None) return 1.0;

        Staff.Element weaponElem = Staff.Element.Water;
        
        if (forcedElement.HasValue)
        {
            weaponElem = forcedElement.Value;
        }
        else if (hero.EquippedWeapon is Staff staff)
        {
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
}
