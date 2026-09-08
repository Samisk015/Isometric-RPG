---
tags: [plan, tilemap, performance]
---
# Batch Tilemap updates

Prepare tile positions and tile values for a complete chunk or height layer, then apply them using `SetTiles` or `SetTilesBlock` instead of thousands of individual `SetTile` calls.

Avoid repeated block-registry lookups inside the deepest render loop. Cache the air definition and resolved tile references.

Profile whether representing every air cell with a `Block` allocation remains acceptable. Consider `null` or a compact ID later, only if measured allocations justify it.

## Done when

- Rendering a chunk performs a small number of Tilemap calls.
- Startup and shutdown times are measurably reduced.

Previous: [[23 - Limit chunk work per frame]]  
Next: [[25 - Properly unload chunks]]
