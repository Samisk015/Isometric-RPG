---
tags: [plan, registries, resources]
---
# Remove local ID asset collisions

Use full IDs when registering and retrieving textures, sprites, and tiles.

Change lookups like `TileRegistry.Get(block.definition.LocalId)` to use the full ID. Otherwise two mods can both define an asset named `copper_ore` and overwrite one another.

## Done when

- Definition and resource registries use the same namespaced ID format.
- Two mods can use the same local name without collision.

Previous: [[07 - Complete content registries]]  
Next: [[09 - Add typed tag keys]]
