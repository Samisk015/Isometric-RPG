---
tags: [plan, world-generation, seed]
---
# Introduce a world seed

Store one integer seed in the world or save metadata. Every permanent generation decision should derive from the seed, world coordinates, and a system-specific salt.

Do not use global `UnityEngine.Random` for terrain, regions, features, or permanent mob placement because results would depend on chunk generation order.

## Done when

- The same seed produces identical chunks across runs.
- Generating chunks in a different order does not change them.
- Different seeds visibly change the world.

Previous: [[25 - Properly unload chunks]]  
Next: [[27 - Use Voronoi region ownership]]
