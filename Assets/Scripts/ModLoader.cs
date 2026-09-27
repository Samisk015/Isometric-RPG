using UnityEngine;
using System.IO;
using MoonSharp;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
public static class ModLoader
{
    public static void LoadBaseGame()
    {
        string GameDataPath = Path.Combine(Application.dataPath, "GameData");
        string baseGamePath = Path.Combine(GameDataPath, "Base");
        LoadContentPack(baseGamePath);
    }

    public static void LoadALlLua(string modPath)
    {
        ModScriptManager.LoadLuaScriptsFromDirectory(modPath);
    }
    
    public static void LoadAllMods()
    {
        string GameDataPath = Path.Combine(Application.dataPath, "GameData");
        string modsDirectory = Path.Combine(GameDataPath, "Mods");
        if (!Directory.Exists(modsDirectory))
        {
            Debug.LogError($"Mods directory does not exist: {modsDirectory}");
            return;
        }

        foreach (string modFolder in Directory.GetDirectories(modsDirectory))
        {
            LoadContentPack(modFolder);
        }
    }
    public static void LoadContentPack(string modFolder)
    {
        string metadataPath = Path.Combine(modFolder, "mod.json");
        string blocksPath = Path.Combine(modFolder, "blocks.json");
        string regionsPath = Path.Combine(modFolder, "regions.json");
        string mobsPath = Path.Combine(modFolder, "mobs.json");

        if (!File.Exists(metadataPath))
        {
            Debug.LogError($"Mod manifest missing in: {modFolder}");
            return;
        }

        LoadALlLua(modFolder);

        ModMetadata metadata =
            JsonUtility.FromJson<ModMetadata>(
                File.ReadAllText(metadataPath));

        if (File.Exists(blocksPath))
        {
            BlockDefinitionFile blockFile = JsonUtility.FromJson<BlockDefinitionFile>(File.ReadAllText(blocksPath));
            foreach (BlockDefinitionData data in blockFile.blocks)
            {
                string[] behaviours = data.behaviours ?? (string.IsNullOrEmpty(data.behaviour) ? System.Array.Empty<string>() : new[] { data.behaviour });
                string[] tags = data.tags ?? data.Tags ?? System.Array.Empty<string>();
                BlockRegistry.Register(new BlockDefinition(metadata.@namespace, data.id, data.isWalkable, data.supportsRotation, behaviours, tags));
            }
        }

        if (File.Exists(regionsPath))
        {
            RegionDefinitionFile regionFile = JsonUtility.FromJson<RegionDefinitionFile>(File.ReadAllText(regionsPath));
            foreach (RegionDefinitionData data in regionFile.regions)
            {
                RegionRegistry.Register(new RegionDefinition(metadata.@namespace, data.id, data.minTemp, data.maxTemp, data.minHumidity, data.maxHumidity, data.terrainShape));
            }
        }

        if (File.Exists(mobsPath))
        {
            MobDefinitionFile mobFile = JsonUtility.FromJson<MobDefinitionFile>(File.ReadAllText(mobsPath));
            foreach (MobDefinitionData data in mobFile.mobs)
            {
                string localId = data.id.Contains(":") ? data.id.Substring(data.id.IndexOf(':') + 1) : data.id;
                string fullId = data.id.Contains(":") ? data.id : $"{metadata.@namespace}:{data.id}";
                MobRegistry.Register(new MobDefinition(metadata.@namespace, localId, fullId, data.maxHealth, data.tags ?? data.Tags, data.aiType, data.behaviour));
            }
        }
    }
}
