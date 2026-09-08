---
tags: [plan, blocks, behaviors]
---
# Define block behavior callbacks

Create a small `IBlockBehaviour` interface with callbacks for placement, breaking, interaction, and neighbor changes.

Avoid an `Update` callback for every block. Later, blocks needing time-based logic can register scheduled or random ticks with the world.

Use separate context types for each callback so the available information is clear and can grow without producing one oversized method signature.

## Done when

- A reusable behavior can respond to a world mutation.
- Blocks without behaviors require no special handling.

Previous: [[12 - Separate logical and rendered positions]]  
Next: [[14 - Include Player in interaction contexts]]
