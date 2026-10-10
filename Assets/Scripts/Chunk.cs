using UnityEngine;

[System.Serializable]
public class Chunk
{
    public const int CHUNK_SIZE = 8;
    public const int CHUNK_HEIGHT = 25;

    private int temperature = 30;

    public int GetTemp()
    {
        return temperature;
    }

    public void SetTemp(int newTemp)
    {
        temperature = newTemp;
    }

    public Vector2Int coordinate;
    
    [System.NonSerialized]
    public Block[,,] blocks;

    public Chunk(Vector2Int coordinate)
    {
        this.coordinate = coordinate;
        blocks = new Block[CHUNK_SIZE, CHUNK_SIZE, CHUNK_HEIGHT];
    }
}
