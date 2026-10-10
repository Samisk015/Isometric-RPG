using UnityEngine;
using System.IO;

public static class ModLoader
{
    public static void LoadBaseGame()
    {
        string GameDataPath = Path.Combine(Application.dataPath, "GameData");
        string baseGamePath = Path.Combine(GameDataPath, "Base");
        LoadContentPack(baseGamePath);
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

        ModMetadata metadata =
            JsonUtility.FromJson<ModMetadata>(
                File.ReadAllText(metadataPath));

        LuaBehaviourRegistry.Instance.LoadFromDirectory(modFolder, metadata.@namespace);

        

        if (File.Exists(blocksPath))
        {
            BlockDefinitionFile blockFile = JsonUtility.FromJson<BlockDefinitionFile>(File.ReadAllText(blocksPath));
            foreach (BlockDefinitionData data in blockFile.blocks)
            {
                string[] tags = data.Tags ?? System.Array.Empty<string>();
                CustomValue[] customValues = data.customValues ?? System.Array.Empty<CustomValue>();
                ItemDrop[] drops = data.drops ?? System.Array.Empty<ItemDrop>();
                BlockRegistry.Register(new BlockDefinition(metadata.@namespace, data.id, data.isWalkable, data.supportsRotation, data.behaviour, tags, data.pathfindCost, drops, customValues, data.blockBreaking, data.textures));
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
