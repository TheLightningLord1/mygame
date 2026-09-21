using System;
using System.Collections.Generic;
using System.Linq;
using Ruletka.Models;

namespace Ruletka.Systems;

public class Inventory
{
    public List<Item> Items { get; set; } = new();
    public int Gold { get; set; } = 1000;

    public void AddItem(Item item)
    {
        Items.Add(item);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n🎒 Получен предмет: {item.Name} [{item.Rarity}]");
        Console.ResetColor();
    }

    public void AddItems(List<Item> items)
    {
        Items.AddRange(items);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n🎒 Получено предметов: {items.Count}");
        foreach (var item in items)
        {
            Console.WriteLine($"  - {item.Name} [{item.Rarity}]");
        }
        Console.ResetColor();
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }

    public List<Item> GetAllItems() => Items;

    public void OpenMenu(Hero hero)
    {
        bool inInventory = true;
        while (inInventory)
        {
            Console.Clear();
            Console.WriteLine("=== 🎒 ИНВЕНТАРЬ ===");
            Console.WriteLine($"Золото: {Gold} 🪙\n");

            Console.WriteLine("1. Показать все предметы");
            Console.WriteLine("2. Экипировать предмет");
            Console.WriteLine("3. Снять экипировку");
            Console.WriteLine("4. Использовать расходник");
            Console.WriteLine("5. Продать предмет");
            Console.WriteLine("6. Выбросить предмет");
            Console.WriteLine("7. Назад");

            Console.Write("\nВыбор: ");
            string choice = Console.ReadLine()!;

            if (choice == "1")
                ShowAllItems();
            else if (choice == "2")
                EquipItem(hero);
            else if (choice == "3")
                UnequipItem(hero);
            else if (choice == "4")
                UseConsumableInMenu(hero);
            else if (choice == "5")
                SellItem();
            else if (choice == "6")
                DiscardItem();
            else if (choice == "7")
                inInventory = false;
        }
    }

    private void ShowAllItems()
    {
        Console.Clear();
        Console.WriteLine("=== 📦 ВСЕ ПРЕДМЕТЫ ===\n");

        if (Items.Count == 0)
        {
            Console.WriteLine("Инвентарь пуст!");
        }
        else
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Item item = Items[i];
                string durabilityInfo = item.Type != ItemType.Consumable 
                    ? $" | Прочность: {item.CurrentDurability}/{item.MaxDurability}" 
                    : "";
                Console.WriteLine($"{i + 1}. {item.Name} [{item.Rarity}] | Тип: {item.Type} | Урон: {item.Damage} | Крит: {item.CreatDamage} | Защита: {item.Protection}{durabilityInfo}");
            }
        }

        Console.WriteLine("\n0. Назад");
        Console.Write("Выбор: ");
        Console.ReadLine();
    }

    private void EquipItem(Hero hero)
    {
        Console.Clear();
        Console.WriteLine("=== ⚔️ ЭКИПИРОВКА ===\n");

        var equippableItems = Items.Where(i => i.Type != ItemType.Consumable && !i.IsBroken()).ToList();

        if (equippableItems.Count == 0)
        {
            Console.WriteLine("Нет предметов для экипировки!");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < equippableItems.Count; i++)
        {
            Item item = equippableItems[i];
            Console.WriteLine($"{i + 1}. {item.Name} [{item.Type}] | Урон: {item.Damage} | Крит: {item.CreatDamage} | Защита: {item.Protection}");
        }

        Console.Write("\nВведите номер предмета: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= equippableItems.Count)
        {
            Item selected = equippableItems[choice - 1];
            
            // Снимаем старую экипировку если есть
            if (selected.Type == ItemType.Sword || selected.Type == ItemType.Staff || selected.Type == ItemType.Archer)
            {
                if (hero.EquippedWeapon != null)
                {
                    hero.EquippedWeapon.RemoveBonus(hero);
                    Items.Add(hero.EquippedWeapon);
                }
                hero.EquippedWeapon = selected;
                selected.ApplyBonus(hero);
                
                if (selected is Sword sword) hero.EquippedSword = sword;
                else if (selected is Staff staff) hero.EquippedMagicStaff = staff;
                else if (selected is Archer bow) hero.EquippedBow = bow;
            }
            else if (selected.Type == ItemType.Shield)
            {
                if (hero.EquippedShield != null)
                {
                    hero.EquippedShield.RemoveBonus(hero);
                    Items.Add(hero.EquippedShield);
                }
                hero.EquippedShield = (Shield)selected;
                selected.ApplyBonus(hero);
            }
            else if (selected.Type == ItemType.Amulet)
            {
                if (hero.EquippedAmulet != null)
                {
                    hero.EquippedAmulet.RemoveBonus(hero);
                    Items.Add(hero.EquippedAmulet);
                }
                hero.EquippedAmulet = (Amulet)selected;
                selected.ApplyBonus(hero);
            }
            else if (selected.Type == ItemType.Armor)
            {
                Armor armor = (Armor)selected;
                if (hero.EquippedArmorSlots[armor.Slot] != null)
                {
                    var oldArmor = hero.EquippedArmorSlots[armor.Slot];
                    if (oldArmor != null)
                    {
                        oldArmor.RemoveBonus(hero);
                        Items.Add(oldArmor);
                    }
                }
                hero.EquippedArmorSlots[armor.Slot] = armor;
                armor.ApplyBonus(hero);
            }

            Items.Remove(selected);
            Console.WriteLine($"\n✅ {selected.Name} экипирован!");
        }

        Console.ReadKey();
    }

    private void UnequipItem(Hero hero)
    {
        Console.Clear();
        Console.WriteLine("=== 📤 СНЯТИЕ ЭКИПИРОВКИ ===\n");

        var equippedItems = hero.GetEquippedItems();

        if (equippedItems.Count == 0)
        {
            Console.WriteLine("Нет экипированных предметов!");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < equippedItems.Count; i++)
        {
            Item item = equippedItems[i];
            Console.WriteLine($"{i + 1}. {item.Name} [{item.Type}]");
        }

        Console.Write("\nВведите номер предмета: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= equippedItems.Count)
        {
            Item selected = equippedItems[choice - 1];
            selected.RemoveBonus(hero);
            Items.Add(selected);

            if (hero.EquippedWeapon == selected)
            {
                hero.EquippedWeapon = null;
                hero.EquippedSword = null;
                hero.EquippedMagicStaff = null;
                hero.EquippedBow = null;
            }
            else if (hero.EquippedShield == selected)
            {
                hero.EquippedShield = null;
            }
            else if (hero.EquippedAmulet == selected)
            {
                hero.EquippedAmulet = null;
            }
            else if (selected is Armor armor)
            {
                hero.EquippedArmorSlots[armor.Slot] = null;
            }

            Console.WriteLine($"\n✅ {selected.Name} снят и добавлен в инвентарь!");
        }

        Console.ReadKey();
    }

    private void UseConsumableInMenu(Hero hero)
    {
        Console.Clear();
        Console.WriteLine("=== 🧪 РАСХОДНИКИ ===\n");

        var consumables = Items.Where(i => i is Consumable).ToList();

        if (consumables.Count == 0)
        {
            Console.WriteLine("Нет расходников!");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < consumables.Count; i++)
        {
            Consumable c = (Consumable)consumables[i];
            Console.WriteLine($"{i + 1}. {c.Name} | Эффект: {c.Effect} | Сила: {c.Power}");
        }

        Console.Write("\nВведите номер предмета: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= consumables.Count)
        {
            Consumable selected = (Consumable)consumables[choice - 1];
            hero.UseConsumable(selected);
            Items.Remove(selected);
        }

        Console.ReadKey();
    }

    public bool UseConsumableInBattle(Hero hero)
    {
        var consumables = Items.Where(i => i is Consumable).ToList();

        if (consumables.Count == 0)
        {
            Console.WriteLine("❌ Нет доступных зелий!");
            return false;
        }

        Console.WriteLine("\n=== 🧪 ЗЕЛЬЯ ===");
        for (int i = 0; i < consumables.Count; i++)
        {
            Consumable c = (Consumable)consumables[i];
            Console.WriteLine($"{i + 1}. {c.Name} | Эффект: {c.Effect} | Сила: {c.Power}");
        }

        Console.Write("Выберите зелье: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= consumables.Count)
        {
            Consumable selected = (Consumable)consumables[choice - 1];
            hero.UseConsumable(selected);
            Items.Remove(selected);
            return true;
        }

        return false;
    }

    private void SellItem()
    {
        Console.Clear();
        Console.WriteLine("=== 💰 ПРОДАЖА ПРЕДМЕТОВ ===\n");

        if (Items.Count == 0)
        {
            Console.WriteLine("Инвентарь пуст!");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            Item item = Items[i];
            int sellPrice = (int)(item.Price * 0.5);
            Console.WriteLine($"{i + 1}. {item.Name} | Цена продажи: {sellPrice} 🪙");
        }

        Console.Write("\nВведите номер предмета для продажи: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= Items.Count)
        {
            Item selected = Items[choice - 1];
            int sellPrice = (int)(selected.Price * 0.5);
            Gold += sellPrice;
            Items.Remove(selected);
            Console.WriteLine($"\n✅ Продано за {sellPrice} 🪙!");
        }

        Console.ReadKey();
    }

    private void DiscardItem()
    {
        Console.Clear();
        Console.WriteLine("=== 🗑️ ВЫБРОСИТЬ ПРЕДМЕТ ===\n");

        if (Items.Count == 0)
        {
            Console.WriteLine("Инвентарь пуст!");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            Item item = Items[i];
            Console.WriteLine($"{i + 1}. {item.Name} [{item.Rarity}]");
        }

        Console.Write("\nВведите номер предмета: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= Items.Count)
        {
            Item selected = Items[choice - 1];
            Items.Remove(selected);
            Console.WriteLine($"\n❌ {selected.Name} выброшен!");
        }

        Console.ReadKey();
    }
}
