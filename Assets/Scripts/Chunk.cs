using UnityEngine;

[System.Serializable]
public class Chunk
{
    public const int CHUNK_SIZE = 8;
    public const int CHUNK_HEIGHT = 25;

    public Vector2Int coordinate;
    
    [System.NonSerialized]
    public Block[,,] blocks;

    public Chunk(Vector2Int coordinate)
    {
        this.coordinate = coordinate;
        blocks = new Block[CHUNK_SIZE, CHUNK_SIZE, CHUNK_HEIGHT];
    }
}
