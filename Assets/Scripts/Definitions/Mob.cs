using System;

[System.Serializable]
public sealed class MobDefinition
{
    public string Namespace { get; }
    public string LocalId { get; }
    public string FullId { get; }
    public int MaxHealth { get; }
    public string[] Tags { get; }
    public string AggressionType { get; }
    public float KnockbackResistance { get; }
    public string BehaviourId { get; }
    public ItemDrop[] Drops { get; }
    public CustomValue[] CustomValues { get; }
    public string[] Animations { get; }

    public MobDefinition(
        string mobNamespace,
        string mobLocalId,
        string mobFullId,
        int mobMaxHealth,
        string[] mobTags,
        string aggressionType,
        float knockbackResistance,
        string behaviourId,
        ItemDrop[] drops,
        CustomValue[] customValues,
        string[] animations)
    {
        Namespace = mobNamespace;
        LocalId = mobLocalId;
        FullId = mobFullId;
        MaxHealth = mobMaxHealth;
        Tags = mobTags ?? Array.Empty<string>();
        AggressionType = aggressionType;
        KnockbackResistance = knockbackResistance;
        BehaviourId = behaviourId;
        Drops = drops ?? Array.Empty<ItemDrop>();
        CustomValues = customValues ?? Array.Empty<CustomValue>();
        Animations = animations ?? Array.Empty<string>();
    }

    public bool HasTag(string tagId)
    {
        return Array.Exists(Tags, tag => tag == tagId);
    }
}

[System.Serializable]
public sealed class MobDefinitionData
{
    public string id;
    public string behaviour;
    public int maxHealth;
    public string[] tags;
    public string aggressionType;
    public float knockbackResistance;
    public ItemDrop[] drops;
    public CustomValue[] customValues;
    public string[] animations;

    // Retained for existing content packs that used the original casing.
    public string[] Tags;
}

[System.Serializable]
public sealed class MobDefinitionFile
{
    public MobDefinitionData[] mobs;
}
