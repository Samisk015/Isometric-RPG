---
tags: [plan, world-generation, terrain]
---
# Complete terrain shape definitions

A terrain-shape definition selects a registered C# generator and supplies parameters such as base height, frequency, amplitude, and octave count.

Initial generator IDs can include `base:flat`, `base:noise`, `base:mountains`, and `base:plateau`.

JSON mods can create many custom shapes by configuring these generators. Adding a genuinely new algorithm still requires C#.

## Done when

- `terrainShapes.json` deserializes into complete definitions.
- Regions resolve their terrain-shape references during loading.
- Plains and hills produce visibly different terrain from data alone.

Previous: [[27 - Use Voronoi region ownership]]  
Next: [[29 - Add region surface rules]]
