using System;
using System.Collections.Generic;
using Ruletka.Models;

namespace Ruletka.Systems;

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
        List<Item> damagedEquipped = hero.GetEquippedItems().FindAll(i => i.CurrentDurability < i.MaxDurability);

        if (damagedEquipped.Count == 0)
        {
            Console.WriteLine("Всё ваше экипированное снаряжение в идеальном состоянии!");
            Console.ReadKey();
            return;
        }

        int totalCost = 0;
        foreach (var item in damagedEquipped)
        {
            totalCost += item.RepairCost;
        }
        
        Console.WriteLine($"Общая стоимость ремонта: {totalCost} 🪙 (Золото: {inventory.Gold} 🪙)");
        Console.Write("Подтвердить ремонт? (1 - Да, 0 - Отмена): ");

        if (Console.ReadLine() == "1")
        {
            if (inventory.Gold >= totalCost)
            {
                inventory.Gold -= totalCost;
                foreach (var item in damagedEquipped)
                    item.CurrentDurability = item.MaxDurability;
                Console.WriteLine("\n✅ Всё снаряжение отремонтировано!");
            }
            else Console.WriteLine("\n❌ Недостаточно золота!");
        }
        Console.ReadKey();
    }

    private static void RepairSingleItemMenu(Hero hero, Inventory inventory)
    {
        Console.Clear();
        List<Item> allDamaged = new List<Item>();
        
        foreach (var item in hero.GetEquippedItems())
        {
            if (item.CurrentDurability < item.MaxDurability && !allDamaged.Contains(item))
                allDamaged.Add(item);
        }
        
        foreach (var item in inventory.GetAllItems())
        {
            if (item.CurrentDurability < item.MaxDurability && !allDamaged.Contains(item))
                allDamaged.Add(item);
        }

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
