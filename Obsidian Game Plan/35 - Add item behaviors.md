---
tags: [plan, items, behaviors, interactions]
---
# Add item behaviors

Create reusable C# behaviors with primary-use and secondary-use callbacks. Avoid naming gameplay callbacks after mouse buttons so controls can be rebound.

An item-use context should contain the `Player`, held `ItemStack`, inventory slot, `World`, and optional block selection including face.

Initial behaviors can include place block, basic tool, consume, heal, basic weapon, and no action.

## Done when

- A block item places its configured block through `World.PlaceBlock`.
- A consumable changes its stack only after successful use.
- Contexts identify the acting player.

Previous: [[34 - Prefer item tags over a fixed purpose enum]]  
Next: [[36 - Add inventory]]
