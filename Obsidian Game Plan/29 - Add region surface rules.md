---
tags: [plan, world-generation, regions]
---
# Add region surface rules

Each region should define its top, filler, and deep blocks plus filler depth.

For every generated column:

1. Select the region.
2. Resolve its terrain shape.
3. Calculate height.
4. Place the region's surface blocks.
5. Add features only after base terrain exists.

Validate all referenced block IDs when definitions are resolved.

## Done when

- A desert can generate sand over sandstone or stone.
- A forest can generate grass over dirt using the same terrain algorithm.

Previous: [[28 - Complete terrain shape definitions]]  
Next: [[30 - Add climate-based region selection]]
