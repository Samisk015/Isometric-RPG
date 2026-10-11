using System;
using UnityEngine;

[RequireComponent(typeof(LivingEntity))]
[RequireComponent(typeof(GridNavigationAgent))]
public sealed class MobController : MonoBehaviour
{
    public MobDefinition Definition { get; private set; }
    public LivingEntity LivingEntity { get; private set; }
    public GridNavigationAgent Navigation { get; private set; }
    public EntityState PersistentState { get; } = new();
    public string PersistentId { get; private set; }
    public Vector3Int GridPosition { get; private set; }

    private SpriteAnimator spriteAnimator;

    private void Awake()
    {
        LivingEntity = GetComponent<LivingEntity>();
        Navigation = GetComponent<GridNavigationAgent>();
        spriteAnimator = GetComponentInChildren<SpriteAnimator>(true);
        Navigation.MoveRequested += SetGridPosition;
    }

    private void OnEnable()
    {
        MobRandomTickSystem.Register(this);
    }

    private void OnDisable()
    {
        MobRandomTickSystem.Unregister(this);
    }

    public void Initialize(MobDefinition definition, string persistentId, Vector3Int position)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        PersistentId = string.IsNullOrWhiteSpace(persistentId) ? Guid.NewGuid().ToString("N") : persistentId;
        LivingEntity.ConfigureMaxHealth(definition.maxHealth);
        SetGridPosition(position);
        PlayDefaultAnimation();
    }

    public bool PlayAnimation(string animationId)
    {
        if (Definition == null)
        {
            Debug.LogWarning("Cannot play an animation before the mob is initialized.", this);
            return false;
        }

        if (!AnimationRegistry.TryGetRuntime(Definition.FullId, animationId, out AnimationRuntime animation))
        {
            Debug.LogWarning(
                $"Animation '{animationId}' is not registered for mob '{Definition.FullId}'.",
                this);
            return false;
        }

        if (!TryEnsureSpriteAnimator())
        {
            Debug.LogWarning($"Mob '{Definition.FullId}' has animations but no SpriteRenderer.", this);
            return false;
        }

        spriteAnimator.Play(animation);
        return true;
    }

    private bool TryEnsureSpriteAnimator()
    {
        if (spriteAnimator != null)
            return true;

        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>(true);
        if (renderer == null)
            return false;

        spriteAnimator = renderer.GetComponent<SpriteAnimator>();
        if (spriteAnimator == null)
            spriteAnimator = renderer.gameObject.AddComponent<SpriteAnimator>();

        return true;
    }

    private void PlayDefaultAnimation()
    {
        if (AnimationRegistry.TryGetGroup(Definition.FullId, out AnimationGroup group)
            && group.FirstAnimation != null)
        {
            PlayAnimation(group.FirstAnimation.id);
        }
    }

    public void SetGridPosition(Vector3Int position)
    {
        GridPosition = position;
    }
}
