---
tags: [plan, performance, profiling]
---
# Profile individual systems

Measure content loading, chunk data generation, Tilemap rendering, collider rebuilding, selection, pathfinding, AI, and saving separately.

Remove `Debug.Log` calls from frame loops. Put temporary diagnostics behind a debug flag or log only when state changes.

Optimize measured bottlenecks rather than replacing working systems based on assumptions.

## Done when

- Profiler samples clearly identify chunk generation and rendering separately.
- Startup and Play Mode shutdown no longer stall unexpectedly.

Previous: [[50 - Add small automated tests]]  
Next: [[52 - Create version-control checkpoints]]
