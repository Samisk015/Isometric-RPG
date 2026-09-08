---
tags: [plan, chunks, performance]
---
# Limit chunk work per frame

Do not generate and render the entire radius in one `Start()` call. Maintain a request queue ordered by distance from the player.

Process a limited budget per frame, such as one generated chunk or one rendered chunk. Display a loading state until the center and immediate surrounding chunks are ready.

## Done when

- Entering the world does not freeze for several seconds.
- The closest requested chunks appear first.
- The work budget can be adjusted in the Inspector.

Previous: [[22 - Replace ChunkManager Test]]  
Next: [[24 - Batch Tilemap updates]]
