using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkRenderer : MonoBehaviour
{
    public static ChunkRenderer Instance { get; private set; }

    public int RenderDistance = 1;

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

    public Tilemap[] tilemaps;
    public void RenderChunk(Chunk chunk)
    {
        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                for (int z = 0; z < Chunk.CHUNK_HEIGHT; z++)
                {
                    Block block = chunk.blocks[x, y, z];

                    BlockDefinition airDef = BlockRegistry.Get("base:air");

                    int worldX = chunk.coordinate.x * Chunk.CHUNK_SIZE + x;
                    int worldY = chunk.coordinate.y * Chunk.CHUNK_SIZE + y;

                    if (block.definition != airDef)
                    {
                        tilemaps[z].SetTile(Vector3Int.FloorToInt(new Vector3(worldX, worldY, 0)), TileRegistry.Get(block.definition.FullId));
                    }
                }
            }
        }
    }

    public void UnRenderChunk(Chunk chunk)
    {
        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                for (int z = 0; z < Chunk.CHUNK_HEIGHT; z++)
                {
                    Block block = chunk.blocks[x, y, z];

                    int worldX = chunk.coordinate.x * Chunk.CHUNK_SIZE + x;
                    int worldY = chunk.coordinate.y * Chunk.CHUNK_SIZE + y;

                    if (block.definition != BlockRegistry.Get("base:air"))
                    {
                        tilemaps[z].SetTile(Vector3Int.FloorToInt(new Vector3(worldX, worldY, 0)), null);
                    }
                }
            }
        } Vector2Int chunkCoord = chunk.coordinate;

    }

    public void RenderBlock(Vector3Int worldPosition, Block block, string textureId)
    {
        BlockDefinition definition = block.definition;

        // to add: check if its a texture inside the definition's textures array

        if (worldPosition.z < 0 || worldPosition.z >= tilemaps.Length) return;
        Vector3Int tilePosition = new Vector3Int(worldPosition.x, worldPosition.y, 0);
        tilemaps[worldPosition.z].SetTile(tilePosition,
            definition.FullId == "base:air" ? null : TileRegistry.Get(textureId));
    }

    public void RenderBlock(Vector3Int worldPosition, Block block)
    {
        BlockDefinition definition = block.definition;
        if (worldPosition.z < 0 || worldPosition.z >= tilemaps.Length) return;
        Vector3Int tilePosition = new Vector3Int(worldPosition.x, worldPosition.y, 0);
        tilemaps[worldPosition.z].SetTile(tilePosition,
            definition.FullId == "base:air" ? null : TileRegistry.Get(definition.FullId));
    } 
}
