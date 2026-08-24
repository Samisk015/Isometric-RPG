using System;
using UnityEngine;

public enum ItemPurpose
{
    Armor,
    Tool,
    Weapon,
    Consumable,
    Potion,
    Material,
    Placeable,
    Block,
    Misc,
}

public class ItemDisplay
{
    public string name;
    public string[] lore;
    public Color color;

    public ItemDisplay(string displayName, string[] displayLore, Color displayColor)
    {
        name = displayName;
        lore = displayLore;
        color = displayColor;
    }
}

public class Item
{
    public ItemDefinition definition;
    public ItemDisplay display = new ItemDisplay(string.Empty, Array.Empty<string>(), Color.white);

    public Enchantment[] enchantments;

    public byte amount;

    public Item(ItemDefinition definition, byte itemAmount)
    {
        this.definition = definition;
    }
}

[System.Serializable]
public class ItemDefinition
{

    public string Namespace { get; }
    public string LocalId { get; }

    public string FullId { get; }
    public ItemPurpose[] itemPurposes;

    public short stackSize;

    public ItemDefinition(string itemNamespace, string itemLocalId, string itemFullId, ItemPurpose[] itemPurposes)
    {
        
    }

}


[System.Serializable]
public class ItemDefinitionData
{
    
}

[System.Serializable]
public class ItemDefinitionFile
{
    public ItemDefinitionData[] file;
}