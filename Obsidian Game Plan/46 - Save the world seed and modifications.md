---
tags: [plan, saving, world]
---
# Save the world seed and modifications

Save the seed and player-made changes rather than every naturally generated block.

Store changed or removed blocks, placed blocks, inventories, player position, persistent mobs, and necessary per-mob state.

When loading a chunk, regenerate it from the seed, apply saved modifications, restore entities, and then render it.

## Done when

- A broken and placed block survives quitting and returning.
- Unmodified terrain does not inflate save size.
- Saves record a format version.

Previous: [[45 - Configure isometric rendering]]  
Next: [[47 - Save IDs instead of object references]]
