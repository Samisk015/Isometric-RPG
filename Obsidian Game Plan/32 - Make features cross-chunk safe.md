---
tags: [plan, world-generation, chunks, features]
---
# Make features cross-chunk safe

A feature near a chunk edge may write into another chunk. Choose a consistent system rather than cutting features at borders.

The simplest robust option is to calculate features in world coordinates and store pending block changes for neighbor chunks that have not generated yet. Apply those pending changes when the target chunk is created.

Ensure the same feature is not applied twice when both neighboring chunks generate.

## Done when

- Trees and ore veins continue across chunk boundaries.
- Chunk generation order does not change feature results.

Previous: [[31 - Make features reusable generators]]  
Next: [[33 - Replace Item with ItemStack]]
