---
tags: [plan, chunks, player]
---
# Track player chunk changes

Store the player's previous chunk coordinate. Recalculate it after logical movement and request a chunk update only when it changes.

Use floor-based division so negative world coordinates map correctly. The chunk manager should receive the new center chunk rather than reading rendered player coordinates itself.

## Done when

- Crossing a chunk boundary triggers one streaming update.
- Moving inside the same chunk does not rescan loaded chunks.
- Negative coordinates produce the expected chunk.

Previous: [[20 - Add grid pathfinding]]  
Next: [[22 - Replace ChunkManager Test]]
