---
tags: [plan, input, architecture]
---
# Separate input from selection

Split the current `PlayerMovement` responsibilities into:

- `PlayerInput`: reads controls.
- `BlockPicker`: determines the current target.
- `PlayerInteraction`: turns an input action into move, break, place, or interact.
- `GridMovement`: follows a path.

Before handling a world click, reject it when the pointer is over UI.

## Done when

- Selection can be tested without moving the player.
- Movement contains no raycasting or UI checks.
- Rebinding controls does not affect item or block behaviors.

Previous: [[17 - Use one selection result for every interaction]]  
Next: [[19 - Define valid standing cells]]
