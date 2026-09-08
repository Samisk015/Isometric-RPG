---
tags: [plan, coordinates, rendering]
---
# Separate logical and rendered positions

Treat `Vector3Int` as the logical block or standing-cell coordinate. Treat `Vector3` as the rendered Unity position.

Create one coordinate conversion service for:

- Block coordinate to rendered block position
- Standing cell to entity-feet position
- Rendered position to candidate grid coordinate
- World coordinate to chunk coordinate
- World coordinate to local chunk coordinate

Do not repeat Tilemap conversion calculations in player movement, selection, mobs, and rendering.

## Done when

- Game rules use logical coordinates.
- Only rendering and input conversion use rendered positions.

Previous: [[11 - Centralize world mutations]]  
Next: [[13 - Define block behavior callbacks]]
