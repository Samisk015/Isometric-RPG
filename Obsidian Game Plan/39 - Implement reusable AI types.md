---
tags: [plan, mobs, ai, registries]
---
# Implement reusable AI types

Create a small set of compiled AI algorithms selected from mob JSON, similar to Terraria-style shared AI.

Initial types can include `base:idle`, `base:passive`, `base:hostile`, `base:flying`, and later `base:worm`.

Register factories rather than shared AI objects. Each mob receives a fresh `IMobAI` instance because targets, timers, and home positions differ per mob.

## Done when

- Two mob definitions can share the same AI type with different parameters.
- Each spawned mob has independent AI state.
- Unknown AI IDs fail during content validation.

Previous: [[38 - Improve LivingEntity]]  
Next: [[40 - Store per-mob state]]
