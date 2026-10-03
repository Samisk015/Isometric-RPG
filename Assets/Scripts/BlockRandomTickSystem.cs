using System.Collections.Generic;
using UnityEngine;

// Tags describe the rule; this engine-owned service applies it on a capped budget.
public sealed class BlockRandomTickSystem : MonoBehaviour
{
    [SerializeField] private float ticksPerLoadedChunkPerSecond = 2f;
    private float credit;
    private long gameTick;
    private readonly System.Random random = new();
    private readonly BlockStateStore states = new();

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
            EntityState newState = new EntityState();

            // newState.SetNumber("growth_stage", )
            // BlockStateStore.SetOrCreate(position, )
        }

        foreach (string behaviourId in block.definition.behaviours)
        {
            if (!LuaBehaviourRegistry.Instance.TryGetBlockBehaviour(behaviourId, out LuaBehaviourModule behaviour)) continue;

            LuaBlockContext context = new LuaBlockContext(
                new LuaGridPosition(position),
                new LuaBlockApi(block),
                new LuaBlockStateApi(states.GetOrCreate(position)),
                null,
                new LuaTimeApi(gameTick),
                new LuaRandomApi(position.GetHashCode() ^ gameTick.GetHashCode()),
                new LuaWorldApi(World.Instance));
            behaviour.Call("on_random_tick", context);
        }
    }
}
