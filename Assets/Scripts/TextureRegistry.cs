using System.Collections.Generic;
using UnityEngine;

public static class TextureRegistry
{
    private static readonly Dictionary<string, Texture2D> textures = new();

    public static void Register(string id, Texture2D texture)
    {
        textures[id] = texture;
    }

    public static Texture2D Get(string id)
    {
        return textures[id];
    }

    public static bool TryGet(string id, out Texture2D texture)
    {
        return textures.TryGetValue(id, out texture);
    }
}
