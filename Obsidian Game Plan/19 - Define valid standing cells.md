---
tags: [plan, movement, world]
---
# Define valid standing cells

Create a single rule that determines whether an entity can occupy a logical cell.

At minimum:

- The body cell is air or otherwise passable.
- The cell below contains a block that supports standing.
- Required headroom is passable.
- The destination lies in loaded world data.

Later parameters can include entity height, step height, allowed drop height, and occupied cells.

## Done when

- Player and mob movement use the same standing rules.
- Placement can reject cells occupied by an entity.

Previous: [[18 - Separate input from selection]]  
Next: [[20 - Add grid pathfinding]]
