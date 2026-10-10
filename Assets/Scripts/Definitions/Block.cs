using MoonSharp.VsCodeDebugger.SDK;
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
public class ItemDrop
{
    public string id;
    public int min;
    public int max;
    public float chance;
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
public class CustomValue
{
    public string id;
    public string type;
    
    public string stringValue;
    public bool boolValue;
    public double numberValue;
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
    public short pathfindCost;
    public bool supportsRotation;
    public string behaviour;
    public string[] Tags;
    public ItemDrop[] drops;
    public CustomValue[] customValues;
    public string blockBreaking;
    public string[] textures;
}

public class BlockDefinition
{
    public string Namespace { get; }
    public string LocalId { get; }

    public string FullId { get; }

    public bool IsWalkable { get; }
    public bool SupportsRotation { get; }
    public short pathfindCost { get; }
    public string behaviour { get; }
    public string[] tags { get; }
    public ItemDrop[] drops { get; }
    public CustomValue[] customValues { get; }
    public string blockBreaking { get; }
    public string[] textures { get; }

    public BlockDefinition(
        string blockNamespace,
        string localId,
        bool isWalkable,
        bool supportsRotation,
        string blockBehaviour,
        string[] blockTags,
        short blockPathfindCost,
        ItemDrop[] blockItemDrops,
        CustomValue[] blockCustomValues,
        string blockBreakingType,
        string[] blockTextures)
    {
        Namespace = blockNamespace;
        LocalId = localId;
        IsWalkable = isWalkable;
        SupportsRotation = supportsRotation;
        pathfindCost = blockPathfindCost;
        behaviour = blockBehaviour;
        tags = blockTags ?? System.Array.Empty<string>();
        drops = blockItemDrops ?? System.Array.Empty<ItemDrop>();
        customValues = blockCustomValues ?? System.Array.Empty<CustomValue>();
        blockBreaking = blockBreakingType;
        textures = blockTextures ?? System.Array.Empty<string>();
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
