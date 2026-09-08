---
tags: [plan, initialization, architecture]
---
# Fix initialization ordering

Replace reliance on unrelated `Awake` and `Start` ordering with one bootstrap sequence:

1. Create managers.
2. Discover and load manifests.
3. Load definitions and tags.
4. Resolve and validate content.
5. Load resource assets.
6. Initialize the world.
7. Load initial chunks.
8. Spawn the player.
9. Enable input.

Expose a clear loading state and stop duplicate base-content loading.

## Done when

- World generation cannot run before required registries exist.
- Content is loaded exactly once.
- Input remains disabled during initialization.

Previous: [[43 - Separate resource packs from content mods]]  
Next: [[45 - Configure isometric rendering]]
