using System.Diagnostics.Tracing;
using UnityEngine;


public class Enchantment
{
    public short level;

    [System.NonSerialized]
    public EnchantmentDefinition definition;

    // public bool applied;
    // wanted to add stored / applied like in minecraft enchanted books

    public Enchantment(EnchantmentDefinition definition, short level)
    {
        this.definition = definition;
        this.level = level;
    }
}

[System.Serializable]

public class EnchantmentDefinition
{
    public string Namespace { get; }
    public string LocalId { get; }

    public string FullId { get; }

    public short maxLevel { get; }

    public EnchantmentDefinition(string enchantNamespace, string enchantLocalId, short enchantMaxLevel)
    {
        Namespace = enchantNamespace;
        LocalId = enchantLocalId;
        FullId = $"{Namespace}:{LocalId}";
        maxLevel = enchantMaxLevel;
    }
}

[System.Serializable]

public class EnchantmentDefinitionData
{
    
}

[System.Serializable]

public class EnchantmentDefinitionFile
{
    public EnchantmentDefinitionData[] file;
}