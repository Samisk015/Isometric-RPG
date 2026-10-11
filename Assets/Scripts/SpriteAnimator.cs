using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteAnimation[] animations;

    private SpriteAnimation currentAnimation;
    private AnimationRuntime currentRuntimeAnimation;
    private int currentFrame;
    private float timer;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (currentRuntimeAnimation != null)
        {
            UpdateRuntimeAnimation();
            return;
        }

        if (currentAnimation == null || currentAnimation.frames == null || currentAnimation.frames.Length == 0)
            return;

        if (currentAnimation.fps <= 0f)
            return;

        float frameTime = 1f / currentAnimation.fps;
        timer += Time.deltaTime;
        while (timer >= frameTime)
        {
            timer -= frameTime;
            currentFrame++;

            if (currentFrame >= currentAnimation.frames.Length)
            {
                if (currentAnimation.loop)
                    currentFrame = 0;
                else
                    currentFrame = currentAnimation.frames.Length - 1;
            }

            spriteRenderer.sprite = currentAnimation.frames[currentFrame];
        }
    }

    private void UpdateRuntimeAnimation()
    {
        if (currentRuntimeAnimation.frames == null || currentRuntimeAnimation.frames.Length == 0)
            return;

        timer += Time.deltaTime;
        while (timer >= currentRuntimeAnimation.frames[currentFrame].duration)
        {
            timer -= currentRuntimeAnimation.frames[currentFrame].duration;
            currentFrame++;

            if (currentFrame >= currentRuntimeAnimation.frames.Length)
            {
                if (currentRuntimeAnimation.loop)
                    currentFrame = 0;
                else
                {
                    currentFrame = currentRuntimeAnimation.frames.Length - 1;
                    timer = 0f;
                    break;
                }
            }

            spriteRenderer.sprite = currentRuntimeAnimation.frames[currentFrame].sprite;
        }
    }

    public void Play(string animationName)
    {
        if (animations != null)
        {
            foreach (SpriteAnimation animation in animations)
            {
                if (animation != null && animation.name == animationName)
                {
                    if (animation.frames == null || animation.frames.Length == 0)
                    {
                        Debug.LogWarning($"Animation '{animationName}' has no frames.", this);
                        return;
                    }

                    if (!EnsureSpriteRenderer())
                        return;

                    if (currentAnimation == animation && currentRuntimeAnimation == null)
                        return;

                    currentRuntimeAnimation = null;
                    currentAnimation = animation;
                    currentFrame = 0;
                    timer = 0f;
                    spriteRenderer.sprite = animation.frames[0];
                    return;
                }
            }
        }

        Debug.LogWarning("Animation not found: " + animationName);
    }

    public void Play(AnimationRuntime animation)
    {
        if (animation == null || animation.frames == null || animation.frames.Length == 0)
        {
            Debug.LogWarning("Cannot play an animation with no runtime frames.", this);
            return;
        }

        foreach (AnimRuntimeFrame frame in animation.frames)
        {
            if (frame == null || frame.sprite == null || frame.duration <= 0f)
            {
                Debug.LogWarning($"Cannot play animation '{animation.id}' because it contains an invalid runtime frame.", this);
                return;
            }
        }

        if (!EnsureSpriteRenderer())
            return;

        currentAnimation = null;
        currentRuntimeAnimation = animation;
        currentFrame = 0;
        timer = 0f;
        spriteRenderer.sprite = animation.frames[0].sprite;
    }

    private bool EnsureSpriteRenderer()
    {
        if (spriteRenderer != null)
            return true;

        Debug.LogError("SpriteAnimator requires a SpriteRenderer on this GameObject or one of its children.", this);
        return false;
    }
}

[System.Serializable]
public class SpriteAnimation
{
    public Sprite[] frames;
    public float fps = 10f;
    public string name;
    public bool loop = true;
}
