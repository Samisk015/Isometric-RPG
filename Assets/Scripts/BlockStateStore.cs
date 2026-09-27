using System.Collections.Generic;
using UnityEngine;

// State is allocated only for blocks that Lua or an engine rule actually changes.
public sealed class BlockStateStore
{
    private readonly Dictionary<Vector3Int, EntityState> states = new();

    public EntityState GetOrCreate(Vector3Int position)
    {
        if (!states.TryGetValue(position, out EntityState state))
        {
            state = new EntityState();
            states[position] = state;
        }
        return state;
    }

    public void Remove(Vector3Int position)
    {
        states.Remove(position);
    }
}
