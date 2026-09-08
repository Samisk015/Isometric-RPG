---
tags: [plan, ui, prototype]
---
# Add the prototype UI

Build only the UI needed for the gameplay loop:

- Health display
- Hotbar
- Inventory window
- Selected-item indicator
- Block highlight
- Loading indicator
- Optional debug display for block and chunk coordinates

UI reads game state and requests operations through controllers. It should not directly change health, inventory arrays, blocks, or movement.

## Done when

- The player can select and use a hotbar item.
- Inventory changes update the display.
- World input is blocked while appropriate UI is active.

Previous: [[48 - Build a single interaction controller]]  
Next: [[50 - Add small automated tests]]
