using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteAnimation[] animations;

    private SpriteAnimation currentAnimation;
    private int currentFrame;
    private float timer;

    void Update()
    {
        if (currentAnimation == null || currentAnimation.frames.Length == 0)
            return;

        timer += Time.deltaTime;

        float frameTime = 1f / currentAnimation.fps;

        if (timer >= frameTime)
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

    public void Play(string animationName)
    {
        foreach (SpriteAnimation animation in animations)
        {
            if (animation.name == animationName)
            {
                if (currentAnimation == animation)
                    return;

                currentAnimation = animation;
                currentFrame = 0;
                timer = 0f;

                spriteRenderer.sprite = animation.frames[0];
                return;
            }
        }

        Debug.LogWarning("Animation not found: " + animationName);
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
