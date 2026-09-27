using System;
using System.IO;
using UnityEngine;

public static class ModScriptManager
{
    public static LuaBehaviourRegistry Behaviours { get; } = new();

    public static void LoadLuaScriptsFromDirectory(string modPath)
    {
        string metadataPath = Path.Combine(modPath, "mod.json");
        ModMetadata metadata = JsonUtility.FromJson<ModMetadata>(File.ReadAllText(metadataPath));
        LoadDirectory(Path.Combine(modPath, "Lua", "MobBehaviours"), metadata.@namespace, Behaviours.RegisterMobBehaviour);
        LoadDirectory(Path.Combine(modPath, "Lua", "BlockBehaviours"), metadata.@namespace, Behaviours.RegisterBlockBehaviour);
    }

    private static void LoadDirectory(string directory, string modNamespace, Action<string, string> register)
    {
        if (!Directory.Exists(directory)) return;

        foreach (string path in Directory.GetFiles(directory, "*.lua", SearchOption.AllDirectories))
        {
            string id = $"{modNamespace}:{Path.GetFileNameWithoutExtension(path)}";
            try
            {
                register(id, File.ReadAllText(path));
                Debug.Log($"Loaded Lua behaviour: {id}");
            }
            catch (Exception exception)
            {
                Debug.LogError($"Lua behaviour '{id}' could not load: {exception.Message}");
            }
        }
    }
}
