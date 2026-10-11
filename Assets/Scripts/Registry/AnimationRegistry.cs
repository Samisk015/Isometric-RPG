using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationGroup
{
    public List<Animation> animations = new List<Animation>();

    public Animation FirstAnimation
    {
        get { return animations.Count > 0 ? animations[0] : null; }
    }

    public bool TryGetAnimation(string id, out Animation animation)
    {
        animation = animations.Find(candidate => candidate.id == id);
        return animation != null;
    }

    internal void Register(Animation animation)
    {
        int existingIndex = animations.FindIndex(candidate => candidate.id == animation.id);
        if (existingIndex >= 0)
        {
            Debug.LogWarning($"Animation '{animation.id}' is already registered in this group. Replacing it.");
            animations[existingIndex] = animation;
            return;
        }

        animations.Add(animation);
    }
}

[System.Serializable]
public class AnimationRuntime
{
    public string id;
    public bool loop;
    public AnimRuntimeFrame[] frames;

    private const float TicksPerSecond = 20f;
    private static readonly Dictionary<string, Sprite> spritesByTextureId = new Dictionary<string, Sprite>();

    internal static bool TryCreate(Animation animation, string groupId, out AnimationRuntime runtime)
    {
        runtime = null;
        if (animation == null || animation.definition == null || animation.definition.frames == null)
            return false;

        List<AnimRuntimeFrame> runtimeFrames = new List<AnimRuntimeFrame>(animation.definition.frames.Length);
        foreach (AnimationFrame frame in animation.definition.frames)
        {
            if (!TryGetTexture(frame.id, groupId, out string textureId, out Texture2D texture))
            {
                Debug.LogError(
                    $"Cannot create animation '{animation.id}' in group '{groupId}': " +
                    $"texture for frame '{frame.id}' is not registered.");
                return false;
            }

            if (!spritesByTextureId.TryGetValue(textureId, out Sprite sprite) || sprite == null)
            {
                sprite = Sprite.Create(
                    texture,
                    new Rect(0f, 0f, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f),
                    100f);
                spritesByTextureId[textureId] = sprite;
            }

            runtimeFrames.Add(new AnimRuntimeFrame
            {
                sprite = sprite,
                duration = frame.ticks / TicksPerSecond
            });
        }

        runtime = new AnimationRuntime
        {
            id = animation.id,
            loop = animation.definition.loop,
            frames = runtimeFrames.ToArray()
        };
        return true;
    }

    private static bool TryGetTexture(
        string frameId,
        string groupId,
        out string textureId,
        out Texture2D texture)
    {
        string modNamespace = groupId.Contains(":")
            ? groupId.Substring(0, groupId.IndexOf(':'))
            : string.Empty;

        if (frameId.Contains(":"))
        {
            if (TextureRegistry.TryGet(frameId, out texture) && texture != null)
            {
                textureId = frameId;
                return true;
            }

            string localFrameId = frameId.Substring(frameId.IndexOf(':') + 1);
            if (TextureRegistry.TryGet(localFrameId, out texture) && texture != null)
            {
                textureId = localFrameId;
                return true;
            }
        }
        else if (!string.IsNullOrEmpty(modNamespace))
        {
            string namespacedFrameId = $"{modNamespace}:{frameId}";
            if (TextureRegistry.TryGet(namespacedFrameId, out texture) && texture != null)
            {
                textureId = namespacedFrameId;
                return true;
            }
        }

        textureId = frameId;
        return TextureRegistry.TryGet(frameId, out texture) && texture != null;
    }
}

[System.Serializable]
public class AnimRuntimeFrame
{
    public Sprite sprite;
    public float duration;
}

public static class AnimationRegistry
{
    private static readonly Dictionary<string, AnimationGroup> groups =
        new Dictionary<string, AnimationGroup>();
    private static readonly Dictionary<string, Dictionary<string, AnimationRuntime>> runtimeCache =
        new Dictionary<string, Dictionary<string, AnimationRuntime>>();

    public static void RegisterAnimation(string groupId, Animation animation)
    {
        if (string.IsNullOrWhiteSpace(groupId))
        {
            Debug.LogError("Cannot register an animation without a group id.");
            return;
        }

        if (animation == null || string.IsNullOrWhiteSpace(animation.id))
        {
            Debug.LogError($"Cannot register an invalid animation in group '{groupId}'.");
            return;
        }

        if (!groups.TryGetValue(groupId, out AnimationGroup group))
        {
            group = new AnimationGroup();
            groups.Add(groupId, group);
        }

        group.Register(animation);
        if (runtimeCache.TryGetValue(groupId, out Dictionary<string, AnimationRuntime> cachedAnimations))
            cachedAnimations.Remove(animation.id);
    }

    public static bool TryGetGroup(string groupId, out AnimationGroup group)
    {
        return groups.TryGetValue(groupId, out group);
    }

    public static bool TryGetRuntime(string groupId, string animationId, out AnimationRuntime runtime)
    {
        runtime = null;
        if (!groups.TryGetValue(groupId, out AnimationGroup group)
            || !group.TryGetAnimation(animationId, out Animation animation))
        {
            return false;
        }

        if (runtimeCache.TryGetValue(groupId, out Dictionary<string, AnimationRuntime> cachedAnimations)
            && cachedAnimations.TryGetValue(animationId, out runtime))
        {
            return true;
        }

        if (!AnimationRuntime.TryCreate(animation, groupId, out runtime))
            return false;

        if (cachedAnimations == null)
        {
            cachedAnimations = new Dictionary<string, AnimationRuntime>();
            runtimeCache[groupId] = cachedAnimations;
        }

        cachedAnimations[animationId] = runtime;
        return true;
    }
}
