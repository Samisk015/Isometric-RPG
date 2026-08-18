using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public static WorldGenerator Instance { get; private set; }

    public static List<Vector2Int> biomePoints = new List<Vector2Int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    [Header("Terrain")]

    public float scale = 0.1f;
    public int heightOffset = 5;
    public int heightMultiplier = 8;

    public Chunk GenerateChunk(Vector2Int chunkCoord)
    {
        Chunk chunk = new Chunk(chunkCoord);

        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                int worldX = chunkCoord.x * Chunk.CHUNK_SIZE + x;
                int worldY = chunkCoord.y * Chunk.CHUNK_SIZE + y;

                float noise =
                    Mathf.PerlinNoise(
                        worldX * scale,
                        worldY * scale);

                int terrainHeight =
                    Mathf.RoundToInt(noise * heightMultiplier)
                    + heightOffset;

                terrainHeight =
                    Mathf.Clamp(
                        terrainHeight,
                        0,
                        Chunk.CHUNK_HEIGHT - 1);

                GenerateColumn(
                    chunk,
                    x,
                    y,
                    terrainHeight);
            }
        }

        return chunk;
    }

    private void GenerateBiome(Chunk chunk)
    {
        Vector2Int chunkCoordinate = chunk.coordinate;
        Vector2Int nearestBiome = GetNearestBiome(chunkCoordinate);
        
    }

    private Vector2Int GetNearestBiome(Vector2Int coordinate)
    {
        Vector2Int nearestPoint = new Vector2Int(0, 0);
        Vector2Int currentPoint = coordinate;
        foreach (Vector2Int point in biomePoints)
        {
            if ((currentPoint - point).magnitude < (currentPoint - nearestPoint).magnitude)
            {
                nearestPoint = point;
            }
        }
        return nearestPoint;
    }

    private void GenerateColumn(Chunk chunk, int x, int y, int terrainHeight)
    {
        for (int z = 0; z < Chunk.CHUNK_HEIGHT; z++)
        {
            if (z > terrainHeight)
            {
                BlockDefinition blockDef = BlockRegistry.Get("base:air");
                chunk.blocks[x,y,z] =
                    new Block(blockDef);

                continue;
            }

            if (z == terrainHeight)
            {
                BlockDefinition blockDef = BlockRegistry.Get("base:grass");
                chunk.blocks[x,y,z] =
                    new Block(blockDef);
            }
            else if (z >= terrainHeight - 2)
            {BlockDefinition blockDef = BlockRegistry.Get("base:dirt");
                chunk.blocks[x,y,z] =
                    new Block(blockDef);
            }
            else
            {
                BlockDefinition blockDef = BlockRegistry.Get("base:stone");
                chunk.blocks[x,y,z] =
                    new Block(blockDef);
            }
        }
}
}

