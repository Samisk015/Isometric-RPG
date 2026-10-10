using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Tags describe the rule; this engine-owned service applies it on a capped budget.
public sealed class BlockRandomTickSystem : MonoBehaviour
{
    [SerializeField] private float ticksPerLoadedChunkPerSecond = 2f;
    private float credit;
    private long gameTick;
    private readonly System.Random random = new();
    

    private void Update()
    {
        if (ChunkManager.Instance == null || World.Instance == null) return;
        gameTick++;
        List<Chunk> chunks = new(ChunkManager.Instance.loadedChunks.Values);
        credit += chunks.Count * ticksPerLoadedChunkPerSecond * Time.deltaTime;
        int budget = Mathf.FloorToInt(credit);
        credit -= budget;

        for (int i = 0; i < budget; i++)
        {
            TickOneRandomBlock(chunks[random.Next(chunks.Count)]);
        }
    }

    private void TickOneRandomBlock(Chunk chunk)
    {
        int x = random.Next(Chunk.CHUNK_SIZE);
        int y = random.Next(Chunk.CHUNK_SIZE);
        int z = random.Next(Chunk.CHUNK_HEIGHT);
        Block block = chunk.blocks[x, y, z];
        if (block?.definition == null) return;

        Vector3Int position = new(chunk.coordinate.x * Chunk.CHUNK_SIZE + x, chunk.coordinate.y * Chunk.CHUNK_SIZE + y, z);
        if (block.definition.HasTag("base:grows_grass") && World.Instance.IsAir(position + Vector3Int.forward))
        {
            World.Instance.SetBlock(position, "base:grass");
        }
        else if (block.definition.HasTag("#plants") && World.Instance.IsAir(position + Vector3Int.forward))
        {
            EntityState state = World.Instance.GetOrCreate(position);
            double growth = state.GetNumber("growth_stage", 0);

            if (growth + 1 == 1)
            {
                string txtId = $"{block.definition.FullId}_growth_1";
                ChunkRenderer.Instance.RenderBlock(position, block, txtId);
            } else if (growth + 1 == 2)
            {
                string txtId = $"{block.definition.FullId}_growth_2";
                ChunkRenderer.Instance.RenderBlock(position, block, txtId);
            } else if (growth + 1 == 3)
            {
                string txtId = $"{block.definition.FullId}_grown";
                ChunkRenderer.Instance.RenderBlock(position, block, txtId);
            }

            state.SetNumber("growth_stage", growth + 1);
        }


        if (!string.IsNullOrEmpty(block.definition.behaviour) &&
        LuaBehaviourRegistry.Instance.TryGetBlockBehaviour(
            block.definition.behaviour,
            out LuaBehaviourModule behaviour))
        {
            LuaBlockContext context = new LuaBlockContext(
                new LuaGridPosition(position),
                new LuaBlockApi(block),
                new LuaBlockStateApi(World.Instance.GetOrCreate(position)),
                null,
                new LuaTimeApi(gameTick),
                new LuaRandomApi(position.GetHashCode() ^ gameTick.GetHashCode()),
                new LuaWorldApi(World.Instance));

            behaviour.Call("on_random_tick", context);
        }
        
    }
}
