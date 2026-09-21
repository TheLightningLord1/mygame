using System;
using System.Collections.Generic;
using System.Linq;
using Ruletka.Models;
using Ruletka.Systems;
using Ruletka.Combat;
using Ruletka.Data;

namespace Ruletka;

public class SaveManager
{
    private const string SaveFilePath = "save.json";

    private static readonly System.Text.Json.JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        IncludeFields = true,
        PropertyNameCaseInsensitive = true
    };

    public static void SaveGame(Hero hero, Inventory inventory)
    {
        try
        {
            var saveData = new SaveData
            {
                Hero = hero,
                Inventory = inventory
            };

            string json = System.Text.Json.JsonSerializer.Serialize(saveData, Options);
            File.WriteAllText(SaveFilePath, json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n💾 Прогресс успешно сохранён в save.json!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Ошибка при сохранении: {ex.Message}");
            Console.ResetColor();
        }
    }

    public static (Hero? hero, Inventory? inventory) LoadGame()
    {
        if (!File.Exists(SaveFilePath))
            return (null, null);

        try
        {
            string json = File.ReadAllText(SaveFilePath);
            var saveData = System.Text.Json.JsonSerializer.Deserialize<SaveData>(json, Options);
            return (saveData?.Hero, saveData?.Inventory);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Ошибка чтения сохранения: {ex.Message}");
            Console.ResetColor();
            return (null, null);
        }
    }

    public static bool SaveExists() => File.Exists(SaveFilePath);
}

[JsonDerivedType(typeof(Sword), "sword")]
[JsonDerivedType(typeof(Staff), "staff")]
[JsonDerivedType(typeof(Shield), "shield")]
[JsonDerivedType(typeof(Archer), "archer")]
[JsonDerivedType(typeof(Armor), "armor")]
[JsonDerivedType(typeof(Amulet), "amulet")]
[JsonDerivedType(typeof(Consumable), "consumable")]
public class SaveData
{
    public Hero Hero { get; set; } = null!;
    public Inventory Inventory { get; set; } = null!;
}
