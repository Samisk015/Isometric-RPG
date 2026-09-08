using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Block
{
    public Direction direction;

    [System.NonSerialized]
    public BlockDefinition definition;

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
    }

public class BlockDefinition
{
    public string Namespace { get; }
    public string LocalId { get; }

    public string FullId { get; }

    public bool IsWalkable { get; }
    public bool SupportsRotation { get; }

    public BlockDefinition(
        string blockNamespace,
        string localId,
        bool isWalkable,
        bool supportsRotation)
    {
        Namespace = blockNamespace;
        LocalId = localId;
        IsWalkable = isWalkable;
        SupportsRotation = supportsRotation;
        FullId = $"{Namespace}:{LocalId}";
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

public interface IBlockInterface
{
    
}

public enum Direction
{
    Up, Down, North, South, East, West
}
