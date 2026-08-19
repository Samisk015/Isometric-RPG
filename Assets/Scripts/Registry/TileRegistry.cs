using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TileRegistry
{
    private static readonly Dictionary<string, TileBase> tiles = new();

    public static void Register(string id, TileBase tile)
    {
        tiles[id] = tile;
    }

    public static TileBase Get(string id)
    {
        return tiles[id];
    }

    public static bool TryGet(string id, out TileBase tile)
    {
        return tiles.TryGetValue(id, out tile);
    }
}
