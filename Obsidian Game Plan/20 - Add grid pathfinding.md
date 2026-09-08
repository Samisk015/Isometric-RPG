---
tags: [plan, movement, pathfinding]
---
# Add grid pathfinding

Implement A* over logical `Vector3Int` standing cells.

Neighbor generation should consider four horizontal directions, allowed step-ups, and allowed drops. Ask the world standing rules whether each candidate is valid.

The pathfinder returns logical cells. `GridMovement` converts the next cell to a rendered feet position and moves smoothly toward it.

## Done when

- The player walks around a simple obstacle.
- The player can step onto a one-block rise when allowed.
- Unreachable clicks fail without teleporting.

Previous: [[19 - Define valid standing cells]]  
Next: [[21 - Track player chunk changes]]
