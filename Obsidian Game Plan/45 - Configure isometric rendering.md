---
tags: [plan, rendering, isometric]
---
# Configure isometric rendering

Before replacing Tilemaps, test Unity's intended isometric setup:

- TilemapRenderer Individual mode where entity interweaving is required.
- A custom transparency sorting axis.
- Consistent sprite pivots at contact points.
- Separate ground, structure, and overhead visual groups where needed.
- Entity sorting calculated from rendered feet position.

Use a custom renderer only if stacked blocks still create depth contradictions that cannot be represented this way.

## Done when

- A player renders correctly in front of and behind test obstacles.
- Height-layer ordering works in several terrain arrangements.

Previous: [[44 - Fix initialization ordering]]  
Next: [[46 - Save the world seed and modifications]]
