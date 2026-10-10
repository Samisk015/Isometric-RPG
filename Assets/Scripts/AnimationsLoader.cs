using UnityEngine;
using System;

public static class AnimationsLoader
{
    public static void LoadAnimations()
    {
        
    }

    private void LoadAnimation(string dir)
    {
        foreach (string file in Directory.GetFiles(dir, "*.png"))
        {
            string id = Path.GetFileNameWithoutExtension(file);
            Debug.Log("Loading texture: " + file + " " + "with ID: " + id);
            Texture2D texture = ConvertToTexture2D(file, id);
            TextureRegistry.Register(id, texture);
        }
    }
}