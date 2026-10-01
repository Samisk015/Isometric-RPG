using System;
using System.Collections.Generic;
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

public class CustomItemState
{
    private readonly Dictionary<string, object> values = new();

    public void Set<T>(string key, T value)
    {
        values[key] = value;
    }

    public T Get<T>(string key, T defaultValue = default)
    {
        return values.TryGetValue(key, out object value)
            ? (T)value
            : defaultValue;
    }
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

    public CustomItemState state;

    public Enchantment[] enchantments;

    public byte amount;

    public Item(ItemDefinition definition, CustomItemState itemState, byte itemAmount = 1, Enchantment[] itemEnchantments = null)
    {
        this.definition = definition;
        this.amount = itemAmount;
        this.enchantments = itemEnchantments;
        this.state = itemState;
    }
}

[System.Serializable]
public class ItemDefinition
{

    public string Namespace { get; }
    public string LocalId { get; }

    public string FullId { get; }
    public ItemPurpose[] purposes;

    public string[] Tags;

    public short stackSize;

    public ItemDefinition(string itemNamespace, string itemLocalId, string itemFullId, ItemPurpose[] itemPurposes, short itemStackSize, string[] ItemTags)
    {
        Namespace = itemNamespace;
        LocalId = itemLocalId;
        FullId = itemFullId;
        purposes = itemPurposes;
        stackSize = itemStackSize;
        Tags = ItemTags;
    }

}


[System.Serializable]
public class ItemDefinitionData
{
    public string id;
    public ItemPurpose[] itemPurposes;
    public short stackSize;

    public string[] itemTags;
}

[System.Serializable]
public class ItemDefinitionFile
{
    public ItemDefinitionData[] file;
}