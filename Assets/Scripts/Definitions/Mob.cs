using UnityEngine;

public class Mob
{
    public int health;
    public string displayName;

    public Mob(MobDefinition mobDefinition)
    {
        
    }
}

[System.Serializable]
public class MobDefinition
{
    public string Namespace { get; }
    public string LocalId { get; }

    public string FullId { get; }
}

[System.Serializable]
public class MobDefinitionData
{
    
}

[System.Serializable]
public class MobDefinitionFile
{
    public MobDefinitionData[] file;
}
