using System.Diagnostics.Tracing;
using UnityEngine;


public class Enchantment
{
    public short level;

    // public bool applied;
    // wanted to add stored / applied like in minecraft enchanted books

    public Enchantment(EnchantmentDefinition definition, short level)
    {
        
    }
}

[System.Serializable]

public class EnchantmentDefinition
{
    public string Namespace;
    public string LocalId;

    public string FullId;

    public byte maxLevel;

    public bool positive;

    public EnchantmentDefinition(string enchantNamespace, string enchantLocalId, string enchantFullId, byte enchantMaxLevel, bool enchantPositive)
    {
        Namespace = enchantNamespace;
        
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