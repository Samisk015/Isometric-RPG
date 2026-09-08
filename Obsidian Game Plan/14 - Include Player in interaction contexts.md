---
tags: [plan, interactions, player]
---
# Include Player in interaction contexts

Add the acting `Player` reference now to placement, breaking, and interaction contexts.

A placement context should include:

- `World`
- `Player`
- Selected block position
- Selected face
- Calculated placement position
- Held `ItemStack`

A break context should include the player, target position, target block, and tool. An interaction context should additionally identify the clicked face.

## Done when

- Behaviors do not read global player state.
- Every player-caused action explicitly identifies its actor.

Previous: [[13 - Define block behavior callbacks]]  
Next: [[15 - Add a block behavior registry]]
