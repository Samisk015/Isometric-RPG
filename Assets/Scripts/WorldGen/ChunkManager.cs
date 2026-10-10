using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager Instance { get; private set; }

    public Grid grid;

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

    public int GetTemperature(Vector2Int coordinate)
    {
        return loadedChunks[coordinate].GetTemp();
    }

    public void SetTemperature(Vector2Int coordinate, int newTemp)
    {
        loadedChunks[coordinate].SetTemp(newTemp);
    }

    public void Test()
    {
        for (int x = -ChunkRenderer.Instance.RenderDistance; x <= ChunkRenderer.Instance.RenderDistance; x++)
        {
            for (int y = -ChunkRenderer.Instance.RenderDistance; y <= ChunkRenderer.Instance.RenderDistance; y++)
            {
                Vector2Int coord = new Vector2Int(x, y);

                Chunk chunk = WorldGenerator.Instance.GenerateChunk(coord);

                loadedChunks.Add(coord, chunk);
                ChunkRenderer.Instance.RenderChunk(chunk);
            }
        }
    }
    [System.NonSerialized]
    public Dictionary<Vector2Int, Chunk> loadedChunks = new Dictionary<Vector2Int, Chunk>();
}
        
