---
tags: [plan, chunks, saving]
---
# Properly unload chunks

When a chunk leaves the retained radius:

1. Save modifications if it is dirty.
2. Clear its Tilemap area in batches.
3. remove or serialize its active mobs.
4. Remove it from `loadedChunks`.
5. Release temporary references.

Consider an unload radius slightly larger than render distance to avoid repeatedly unloading chunks near a boundary.

## Done when

- Distant chunks disappear and leave no Tilemap cells behind.
- Returning to an unloaded chunk restores the same terrain and changes.

Previous: [[24 - Batch Tilemap updates]]  
Next: [[26 - Introduce a world seed]]
