using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Block
{

    public Vector3Int position;
    public Direction direction;

    [System.NonSerialized]
    public BlockDefinition definition;

    public Vector3Int GetPosition()
    {
        return position;
    }

    public Block(BlockDefinition definition, Direction direction = Direction.Up)
    {
         this.definition = definition;
        if (definition.SupportsRotation)
        {
            this.direction = direction;
        }
    }
}

[System.Serializable]
public class ModMetadata
{
    public string modName;
    public string @namespace;
    public string gameVersion;
    public string version;
    public string author;
}

[System.Serializable]
public class BlockDefinitionFile
{
    public BlockDefinitionData[] blocks;
}

[System.Serializable]
public class BlockDefinitionData
{
    public string id;
    public bool isWalkable;
    public bool supportsRotation;
    // Keep both forms while the content format is being standardized.
    public string behaviour;
    public string[] behaviours;
    public string[] Tags;
    public string[] tags;
}

public class BlockDefinition
{
    public string Namespace { get; }
    public string LocalId { get; }

    public string FullId { get; }

    public bool IsWalkable { get; }
    public bool SupportsRotation { get; }

    public short pathfindCost { get; }
    public string[] behaviours { get; }

    public string[] tags { get; }

    public BlockDefinition(
        string blockNamespace,
        string localId,
        bool isWalkable,
        bool supportsRotation,
        string[] blockBehaviours,
        string[] blockTags,
        short blockPathfindCost)
    {
        Namespace = blockNamespace;
        LocalId = localId;
        IsWalkable = isWalkable;
        SupportsRotation = supportsRotation;
        pathfindCost = blockPathfindCost;
        behaviours = blockBehaviours ?? System.Array.Empty<string>();
        tags = blockTags ?? System.Array.Empty<string>();
        FullId = $"{Namespace}:{LocalId}";
    }

    public bool HasTag(string tagId)
    {
        return System.Array.Exists(tags, tag => tag == tagId);
    }
}

[System.Serializable]
public class ResourcePackMetadata
{
    public string id;
    public string name;
    public string version;
    public string author;
    public string description;
}

public enum Direction
{
    Up, Down, North, South, East, West
}
