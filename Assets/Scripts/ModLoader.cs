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

        AnimationsLoader.LoadAnimations(modFolder, metadata.@namespace);

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
            if (mobFile == null || mobFile.mobs == null)
            {
                Debug.LogError($"Invalid mob definitions in '{mobsPath}'. Expected a 'mobs' array.");
                return;
            }

            foreach (MobDefinitionData data in mobFile.mobs)
            {
                if (data == null || string.IsNullOrWhiteSpace(data.id))
                {
                    Debug.LogError($"A mob definition in '{mobsPath}' has no id.");
                    continue;
                }

                if (data.maxHealth <= 0)
                {
                    Debug.LogError($"Mob '{data.id}' in '{mobsPath}' must have maxHealth greater than zero.");
                    continue;
                }

                if (data.knockbackResistance < 0f || data.knockbackResistance > 1f)
                {
                    Debug.LogError(
                        $"Mob '{data.id}' in '{mobsPath}' must have knockbackResistance between 0 and 1.");
                    continue;
                }

                string fullId = QualifyId(data.id, metadata.@namespace);
                int namespaceSeparator = fullId.IndexOf(':');
                if (namespaceSeparator <= 0 ||
                    namespaceSeparator == fullId.Length - 1 ||
                    fullId.IndexOf(':', namespaceSeparator + 1) >= 0)
                {
                    Debug.LogError($"Mob id '{data.id}' in '{mobsPath}' must be a local id or a single namespace:id.");
                    continue;
                }

                string mobNamespace = fullId.Substring(0, namespaceSeparator);
                string localId = fullId.Substring(namespaceSeparator + 1);
                string behaviourId = string.IsNullOrWhiteSpace(data.behaviour)
                    ? null
                    : QualifyId(data.behaviour, metadata.@namespace);

                bool hasInvalidDrop = false;
                if (data.drops != null)
                {
                    foreach (ItemDrop drop in data.drops)
                    {
                        if (drop != null &&
                            !string.IsNullOrWhiteSpace(drop.id) &&
                            drop.min >= 0 &&
                            drop.max >= drop.min &&
                            drop.chance >= 0f &&
                            drop.chance <= 1f)
                        {
                            continue;
                        }

                        Debug.LogError($"Mob '{fullId}' in '{mobsPath}' has an invalid drop definition.");
                        hasInvalidDrop = true;
                    }
                }

                if (hasInvalidDrop)
                {
                    continue;
                }

                MobRegistry.Register(new MobDefinition(
                    mobNamespace,
                    localId,
                    fullId,
                    data.maxHealth,
                    data.tags ?? data.Tags,
                    string.IsNullOrWhiteSpace(data.aggressionType) ? "neutral" : data.aggressionType,
                    data.knockbackResistance,
                    behaviourId,
                    data.drops,
                    data.customValues,
                    data.animations));
            }
        }
    }

    private static string QualifyId(string id, string modNamespace)
    {
        return id.Contains(":") || string.IsNullOrWhiteSpace(modNamespace)
            ? id
            : $"{modNamespace}:{id}";
    }
}
