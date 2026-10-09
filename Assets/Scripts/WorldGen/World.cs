using System;
using Unity.VisualScripting;
using UnityEngine;

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}

public class World : MonoBehaviour
{
    public static World Instance { get; private set; }

    private float timer;

    private float intervalSeconds = 1.0f;

    private int DAY_LENGTH = 6000;

    private int MAX_ANGLE = 180;

    private int YEAR_LENGTH = 120;

    private float HOTTEST_DAY = 0.25f;

    private float COLDEST_DAY = 0.75f;

    private float HOTTEST_MULTI = 2.0f;

    private float COLDEST_MULTI = 0.25f;

    private const float SIGMOID_STEEPNESS = 8.0f;

    private const float WEATHER_VARIATION = 0.04f;

    private float Temperature_mutlti = 1.0f;

    private readonly BlockStateStore states = new();

    public EntityState GetOrCreate(Vector3Int position)
    {
        return states.GetOrCreate(position);
    }

    public void SetOrCreate(Vector3Int position, EntityState state)
    {
        states.SetOrCreate(position, state);
    }

    public void RemoveState(Vector3Int position)
    {
        states.Remove(position);
    }
    
    private Season currentSeason = Season.Spring;

    public Season GetSeason()
    {
        return currentSeason;
    }

    public float GetTempMutli()
    {
        return Temperature_mutlti;
    }
    

    private int NIGHT_LENGTH = 3000;

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

    public int day = 1;

    public int daytime = 0;

    public bool TryGetBlock(Vector3Int worldPosition)
    {
        return TryGetBlock(worldPosition, out _);
    }

    public bool TryGetBlock(Vector3Int worldPosition, out Block block)
    {
        block = null;
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
                block = chunk.blocks[localX, localY, localZ];
                return block != null;
            }
        }

        return false;
    }

    public bool IsAir(Vector3Int worldPosition)
    {
        return TryGetBlock(worldPosition, out Block block) && block.definition.FullId == "base:air";
    }

    public bool SetBlock(Vector3Int worldPosition, string blockId)
    {
        if (!BlockRegistry.TryGet(blockId, out BlockDefinition definition)) return false;

        Vector2Int chunkCoord = new Vector2Int(
            Mathf.FloorToInt((float)worldPosition.x / Chunk.CHUNK_SIZE),
            Mathf.FloorToInt((float)worldPosition.y / Chunk.CHUNK_SIZE));
        if (!ChunkManager.Instance.loadedChunks.TryGetValue(chunkCoord, out Chunk chunk)) return false;

        int localX = worldPosition.x - chunkCoord.x * Chunk.CHUNK_SIZE;
        int localY = worldPosition.y - chunkCoord.y * Chunk.CHUNK_SIZE;
        int localZ = worldPosition.z;
        if (localX < 0 || localX >= Chunk.CHUNK_SIZE || localY < 0 || localY >= Chunk.CHUNK_SIZE || localZ < 0 || localZ >= Chunk.CHUNK_HEIGHT) return false;

        Block block = new Block(definition);
        chunk.blocks[localX, localY, localZ] = block;
        ChunkRenderer.Instance?.RenderBlock(worldPosition, block);
        return true;
    }

    public bool SetOrSwapBlock(Vector3Int worldPosition, string blockId)
    {
        if (!BlockRegistry.TryGet(blockId, out BlockDefinition definition)) return false;
        
        Vector2Int chunkCoord = new Vector2Int(
            Mathf.FloorToInt((float)worldPosition.x / Chunk.CHUNK_SIZE),
            Mathf.FloorToInt((float)worldPosition.y / Chunk.CHUNK_SIZE));
        if (!ChunkManager.Instance.loadedChunks.TryGetValue(chunkCoord, out Chunk chunk)) return false;

        int localX = worldPosition.x - chunkCoord.x * Chunk.CHUNK_SIZE;
        int localY = worldPosition.y - chunkCoord.y * Chunk.CHUNK_SIZE;
        int localZ = worldPosition.z;
        if (localX < 0 || localX >= Chunk.CHUNK_SIZE || localY < 0 || localY >= Chunk.CHUNK_SIZE || localZ < 0 || localZ >= Chunk.CHUNK_HEIGHT) return false;

        Block block = new Block(definition);
        chunk.blocks[localX, localY, localZ] = block;
        ChunkRenderer.Instance?.RenderBlock(worldPosition, block);
        return true;
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
            UpdateTemperature();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intervalSeconds)
        {
            timer -= intervalSeconds;
            daytime++;

            if (daytime >= 6000)
            {
                day++;
                if (day >= YEAR_LENGTH)
                {
                    day -= YEAR_LENGTH;
                }

                UpdateTemperature();
                daytime -= 6000;
            }
        }
    }

    private void UpdateTemperature()
    {
        float hottestDay = HOTTEST_DAY * YEAR_LENGTH;
        float coldestDay = COLDEST_DAY * YEAR_LENGTH;
        float progress = Mathf.Repeat(day - hottestDay, YEAR_LENGTH) / YEAR_LENGTH;
        float easedProgress;
        float startTemperature;
        float endTemperature;

        if (progress < 0.25f)
            currentSeason = Season.Spring;
        else if (progress < 0.5f)
            currentSeason = Season.Summer;
        else if (progress < 0.75f)
            currentSeason = Season.Autumn;
        else
            currentSeason = Season.Winter;

        if (progress < 0.5f)
        {
            easedProgress = EvaluateSigmoid(progress * 2.0f);
            startTemperature = HOTTEST_MULTI;
            endTemperature = COLDEST_MULTI;
        }
        else
        {
            easedProgress = EvaluateSigmoid((progress - 0.5f) * 2.0f);
            startTemperature = COLDEST_MULTI;
            endTemperature = HOTTEST_MULTI;
        }

        float temperature = Mathf.Lerp(startTemperature, endTemperature, easedProgress);
        bool isSeasonalExtreme = day == Mathf.RoundToInt(hottestDay) || day == Mathf.RoundToInt(coldestDay);
        if (!isSeasonalExtreme)
        {
            float variation = (HOTTEST_MULTI - COLDEST_MULTI) * WEATHER_VARIATION;
            temperature += UnityEngine.Random.Range(-variation, variation);
        }

        Temperature_mutlti = Mathf.Clamp(temperature, COLDEST_MULTI, HOTTEST_MULTI);
    }

    private static float EvaluateSigmoid(float progress)
    {
        float lowerBound = 1.0f / (1.0f + Mathf.Exp(SIGMOID_STEEPNESS * 0.5f));
        float upperBound = 1.0f / (1.0f + Mathf.Exp(-SIGMOID_STEEPNESS * 0.5f));
        float value = 1.0f / (1.0f + Mathf.Exp(-SIGMOID_STEEPNESS * (progress - 0.5f)));
        return (value - lowerBound) / (upperBound - lowerBound);
    }
}
