using System.IO;
using UnityEngine;

public static class AnimationsLoader
{
    public static void LoadAnimations(string modFolder, string modNamespace)
    {
        string configPath = Path.Combine(modFolder, "animations.json");
        if (!File.Exists(configPath))
            return;

        AnimDefinitionFile definitionFile =
            JsonUtility.FromJson<AnimDefinitionFile>(File.ReadAllText(configPath));
        if (definitionFile == null || definitionFile.animations == null)
        {
            Debug.LogError($"Invalid animation definitions in '{configPath}'. Expected an 'animations' array.");
            return;
        }

        foreach (AnimGroupData groupData in definitionFile.animations)
        {
            if (groupData == null || string.IsNullOrWhiteSpace(groupData.groupId))
            {
                Debug.LogError($"An animation group in '{configPath}' has no groupId.");
                continue;
            }

            if (groupData.animations == null)
            {
                Debug.LogError($"Animation group '{groupData.groupId}' in '{configPath}' has no animations array.");
                continue;
            }

            string groupId = QualifyId(groupData.groupId, modNamespace);
            RegisterAnimations(groupId, groupData.animations, configPath);
        }
    }

    public static void RegisterAnimations(string groupId, AnimDefinitionData[] animations, string source)
    {
        if (animations == null)
            return;

        foreach (AnimDefinitionData animationData in animations)
        {
            if (animationData == null || string.IsNullOrWhiteSpace(animationData.id))
            {
                Debug.LogError($"An animation in group '{groupId}' in '{source}' has no id.");
                continue;
            }

            if (animationData.frames == null || animationData.frames.Length == 0)
            {
                Debug.LogError($"Animation '{animationData.id}' in group '{groupId}' in '{source}' has no frames.");
                continue;
            }

            bool hasInvalidFrame = false;
            foreach (AnimationFrame frame in animationData.frames)
            {
                if (frame == null || frame.ticks <= 0)
                {
                    Debug.LogError(
                        $"Animation '{animationData.id}' in group '{groupId}' in '{source}' contains a frame with a non-positive tick duration.");
                    hasInvalidFrame = true;
                    break;
                }

                if (string.IsNullOrWhiteSpace(frame.id))
                    frame.id = frame.path;

                if (string.IsNullOrWhiteSpace(frame.id))
                {
                    Debug.LogError(
                        $"Animation '{animationData.id}' in group '{groupId}' in '{source}' contains a frame without an id or path.");
                    hasInvalidFrame = true;
                    break;
                }
            }

            if (hasInvalidFrame)
                continue;

            AnimationDefinition definition =
                new AnimationDefinition(animationData.id, animationData.loop, animationData.frames);
            AnimationRegistry.RegisterAnimation(groupId, new Animation(definition));
        }
    }

    private static string QualifyId(string id, string modNamespace)
    {
        return id.Contains(":") || string.IsNullOrWhiteSpace(modNamespace)
            ? id
            : $"{modNamespace}:{id}";
    }
}
