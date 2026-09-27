using System.Collections.Generic;
using MoonSharp.Interpreter.CoreLib;
using UnityEngine;

public class Mob
{
    public int health;
    public string displayName;

    public CustomMobState state;

    public bool AI;

    public Mob(MobDefinition mobDefinition) : this(mobDefinition, mobDefinition.maxHealth)
    {
        
    }

    public Mob(MobDefinition mobDefinition, int maxHeatlh)
    {
        
    }
}

public struct MobItemDrop
{
    public string id;
}

public class CustomMobState
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

[System.Serializable]
public class MobDefinition
{
    public string Namespace;
    public string LocalId;

    public string FullId;

    public int maxHealth;    

    public string[] Tags;

    public string AIType;
    public string BehaviourId;

    public MobDefinition(string mobNamespace, string mobLocalId, string mobFullId, int mobMaxHealth, string[] mobTags, string mobAIType, string behaviourId)
    {
        Namespace = mobNamespace;
        LocalId = mobLocalId;
        FullId = mobFullId;
        maxHealth = mobMaxHealth;
        Tags = mobTags ?? System.Array.Empty<string>();
        AIType = mobAIType;
        BehaviourId = behaviourId;
    }
}

[System.Serializable]
public class MobDefinitionData
{
    public string id;
    public string behaviour;
    public int maxHealth;
    public string[] Tags;
    public string[] tags;
    public string aiType;
}

[System.Serializable]
public class MobDefinitionFile
{
    public MobDefinitionData[] mobs;
}
