---
tags: [plan, items, inventory, ui]
---
# Add inventory

Start with a fixed slot count and a selected hotbar slot.

Implement adding, removing, finding, merging, splitting, swapping, and enforcing maximum stack size. Keep inventory operations independent from UI components.

The UI should request inventory operations and redraw from inventory state; it should not modify arrays or stack amounts directly.

## Done when

- Picked-up items merge into compatible stacks.
- Full inventory rejects remaining items cleanly.
- The selected hotbar item is available to interaction contexts.

Previous: [[35 - Add item behaviors]]  
Next: [[37 - Use one GameObject per active entity]]
