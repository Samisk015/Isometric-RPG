---
tags: [plan, entities, unity]
---
# Use one GameObject per active entity

Represent each currently loaded player or mob with one GameObject composed from focused components:

- `SpriteRenderer`
- `LivingEntity`
- `GridMovement`
- `MobController` or `Player`
- `MobAIController` for mobs
- `SpriteAnimator`

Definitions remain plain data. Only loaded, active entities need GameObjects. Pooling or data-only inactive entities can be added later if profiling requires it.

## Done when

- A generic mob prefab can initialize from any valid mob definition.
- Components do not duplicate health or position state.

Previous: [[36 - Add inventory]]  
Next: [[38 - Improve LivingEntity]]
