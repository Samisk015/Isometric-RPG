---
tags: [plan, world, blocks]
---
# Centralize world mutations

Normal gameplay should never change `chunk.blocks` directly. Route changes through `World` methods such as `SetBlock`, `BreakBlock`, `PlaceBlock`, `MoveBlock`, `IsAir`, and `NotifyNeighbours`.

Each mutation should update block data, update rendering, run behaviors, notify neighbors, and mark the chunk as changed for saving.

World generation may fill a new chunk directly because no gameplay callbacks are needed yet.

## Done when

- Breaking and placement use the same world mutation path.
- Behaviors cannot be skipped by ordinary gameplay code.

Previous: [[10 - Load tag files]]  
Next: [[12 - Separate logical and rendered positions]]
