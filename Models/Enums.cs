namespace Ruletka.Models;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythic
}

public enum ItemType
{
    Sword,
    Staff,
    Shield,
    Archer,
    Armor,
    Amulet,
    Consumable,
    Helmet,
    Chestplate,
    Leggings,
    Gloves
}

public enum HeroClass
{
    Knight,   
    Paladin,  
    Mage,     
    Archer    
}

public enum MonsterElement
{
    None,
    Fire,   // Огонь
    Ice,    // Лёд
    Dark,
    Venom,
    Blad    // Тьма
}

public enum ArmorSlot
{
    Helmet,
    Chestplate,
    Leggings,
    Gloves
}

public enum ConsumableEffect
{
    Food,
    Water,
    Regeneration,
    Strength,
    Defense,
    Magic,
    Speed
}
