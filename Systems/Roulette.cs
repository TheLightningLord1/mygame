using System;
using System.Collections.Generic;
using Ruletka.Models;

namespace Ruletka.Systems;

public class Roulette
{
    private Random random = new Random();

    public Item Spin(List<Item> weapons)
    {
        if (weapons.Count == 0) return null!;
        
        double totalWeight = 0;
        foreach(Item weapon in weapons)
        {
            totalWeight += GetWeightByRarity(weapon.Rarity);
        }
        
        double rolledNumber = totalWeight * random.NextDouble();
        
        foreach(Item item in weapons)
        {
            rolledNumber -= GetWeightByRarity(item.Rarity);
            if (rolledNumber <= 0)
            {
                return item;
            }
        }
        
        return weapons[0];
    }

    public Item SingleSpin(List<Item> weapons)
    {
        Item droppedItem = Spin(weapons);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n🎁 Выпал предмет: {droppedItem.Name} [{droppedItem.Rarity}]");
        Console.ResetColor();
        return droppedItem;
    }

    public List<Item> TestSpin(List<Item> weapons)
    {
        List<Item> droppedList = new List<Item>();

        for (int i = 0; i < 10; i++)
        {
            Item droppedItem = Spin(weapons);
            droppedList.Add(droppedItem);
        }

        return droppedList;
    }

    private double GetWeightByRarity(Rarity rarity)
    {
        return rarity switch
        {
            Rarity.Common => 50,
            Rarity.Uncommon => 30,
            Rarity.Rare => 15,
            Rarity.Epic => 4,
            Rarity.Legendary => 1,
            Rarity.Mythic => 0.1,
            _ => 10
        };
    }
}
