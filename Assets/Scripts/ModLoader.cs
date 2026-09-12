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

    public static void LoadLuaScript()
    {
       
    }

    public static void LoadALlLua(string modPath)
    {
        string luaFolderPath = Path.Combine(modPath, "Lua");
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

        if (!File.Exists(metadataPath) || !File.Exists(blocksPath) || !File.Exists(regionsPath))
        {
            Debug.LogError($"Mod files missing in: {modFolder}");
            return;
        }

        ModMetadata metadata =
            JsonUtility.FromJson<ModMetadata>(
                File.ReadAllText(metadataPath));

        BlockDefinitionFile blockFile =
            JsonUtility.FromJson<BlockDefinitionFile>(
                File.ReadAllText(blocksPath));

        RegionDefinitionFile regionFile =
            JsonUtility.FromJson<RegionDefinitionFile>(
                File.ReadAllText(regionsPath));

        foreach (BlockDefinitionData data in blockFile.blocks)
        {
            BlockDefinition definition = new BlockDefinition(
                metadata.@namespace,
                data.id,
                data.isWalkable,
                data.supportsRotation
            );
            BlockRegistry.Register(definition);
        }

        foreach (RegionDefinitionData data in regionFile.regions)
        {
            RegionDefinition definition = new RegionDefinition(
                metadata.@namespace,
                data.id,
                data.minTemp,
                data.maxTemp,
                data.minHumidity,
                data.maxHumidity,
                data.terrainShape
            );
            RegionRegistry.Register(definition);
        }
    }
}
