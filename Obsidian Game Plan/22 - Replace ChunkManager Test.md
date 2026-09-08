---
tags: [plan, chunks, streaming]
---
# Replace ChunkManager Test

Replace the startup-only `Test()` method with a real lifecycle:

- `InitializeAroundPlayer`
- `UpdateAroundPlayer`
- `RequestChunk`
- `GenerateQueuedChunks`
- `UnloadDistantChunks`

Render distance is a radius. Distance 1 means a 3 by 3 chunk area centered on the player.

## Done when

- Initial chunks load around the player's actual starting chunk.
- Moving into another chunk queues only missing chunks.
- Already loaded chunks are not generated twice.

Previous: [[21 - Track player chunk changes]]  
Next: [[23 - Limit chunk work per frame]]
