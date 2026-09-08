---
tags: [plan, selection, interactions]
---
# Use one selection result for every interaction

The `BlockSelection` result should drive highlighting, breaking, placement, inspection, and movement.

Interpret it differently for each action:

- Highlight and break use the selected block.
- Placement uses selected block plus face direction.
- Movement uses the empty standing cell above a selected top face.

Remove the per-frame selection logs. If needed, log only when the result changes.

## Done when

- All block actions agree about what is selected.
- The player no longer moves to the center of a solid block.

Previous: [[16 - Replace the block selection algorithm]]  
Next: [[18 - Separate input from selection]]
