using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance { get; private set; }

    public Block GetBlock(Vector3Int worldPosition)
    {
        Vector2Int chunkCoord = new Vector2Int(
            Mathf.FloorToInt((float)worldPosition.x / Chunk.CHUNK_SIZE),
            Mathf.FloorToInt((float)worldPosition.y / Chunk.CHUNK_SIZE)
        );

        if (ChunkManager.Instance.loadedChunks.TryGetValue(chunkCoord, out Chunk chunk))
        {
            int localX = worldPosition.x - chunkCoord.x * Chunk.CHUNK_SIZE;
            int localY = worldPosition.y - chunkCoord.y * Chunk.CHUNK_SIZE;
            int localZ = worldPosition.z;

            if (localX >= 0 && localX < Chunk.CHUNK_SIZE &&
                localY >= 0 && localY < Chunk.CHUNK_SIZE &&
                localZ >= 0 && localZ < Chunk.CHUNK_HEIGHT)
            {
                return chunk.blocks[localX, localY, localZ];
            }
        }

        return null; // Return null if the block is not found
    }

    public bool TryGetBlock(Vector3Int worldPosition)
    {
        Vector2Int chunkCoord = new Vector2Int(
            Mathf.FloorToInt((float)worldPosition.x / Chunk.CHUNK_SIZE),
            Mathf.FloorToInt((float)worldPosition.y / Chunk.CHUNK_SIZE)
        );

        if (ChunkManager.Instance.loadedChunks.TryGetValue(chunkCoord, out Chunk chunk))
        {
            int localX = worldPosition.x - chunkCoord.x * Chunk.CHUNK_SIZE;
            int localY = worldPosition.y - chunkCoord.y * Chunk.CHUNK_SIZE;
            int localZ = worldPosition.z;

            if (localX >= 0 && localX < Chunk.CHUNK_SIZE &&
                localY >= 0 && localY < Chunk.CHUNK_SIZE &&
                localZ >= 0 && localZ < Chunk.CHUNK_HEIGHT)
            {
                return true;
            }
        }

        return false; // Return null if the block is not found
    }

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
}
