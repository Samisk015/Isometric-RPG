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

    private void Awake()
    {
        LivingEntity = GetComponent<LivingEntity>();
        Navigation = GetComponent<GridNavigationAgent>();
        Navigation.MoveRequested += SetGridPosition;
    }

    public void Initialize(MobDefinition definition, string persistentId, Vector3Int position)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        PersistentId = string.IsNullOrWhiteSpace(persistentId) ? Guid.NewGuid().ToString("N") : persistentId;
        LivingEntity.ConfigureMaxHealth(definition.maxHealth);
        SetGridPosition(position);
    }

    public void SetGridPosition(Vector3Int position)
    {
        GridPosition = position;
    }
}
