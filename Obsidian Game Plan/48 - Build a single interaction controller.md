---
tags: [plan, interactions, input, player]
---
# Build a single interaction controller

Create one player interaction controller that receives input actions and the current selection, then dispatches movement, block interaction, item primary use, or item secondary use.

Before world interaction, check whether the pointer is over UI and whether the target is valid. Construct the correct context rather than letting behaviors read global state.

## Done when

- Input mapping and gameplay actions are separate.
- Placing, breaking, and interacting all use contexts containing `Player`.
- UI clicks do not affect the world.

Previous: [[47 - Save IDs instead of object references]]  
Next: [[49 - Add the prototype UI]]
