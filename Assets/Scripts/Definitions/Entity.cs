using UnityEngine;

public class Entity
{
    public int health;
    public string displayName;

    public Entity(EntityDefinition)
    {
        
    }
}

[System.Serializable]
public class EntityDefinition
{
    public string Namespace { get; }
    public string LocalId { get; }

    public string FullId { get; }
}

[System.Serializable]
public class EntityDefinitionData
{
    
}

[System.Serializable]
public class EntityDefinitionFile
{
    public EntityDefinitionData[] file;
}
