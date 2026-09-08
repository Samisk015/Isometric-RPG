---
tags: [plan, items, tags]
---
# Prefer item tags over a fixed purpose enum

An optional primary category can help organize inventory UI, but gameplay classification should use tags because one item may have several roles.

Examples include `base:tools`, `base:weapons`, `base:pickaxes`, `base:placeable_blocks`, and `base:consumables`.

Use behavior IDs for what an item actually does. Tags describe membership; they should not replace behavior logic.

## Done when

- One item can belong to multiple categories.
- Mods can add their items to existing gameplay groups.

Previous: [[33 - Replace Item with ItemStack]]  
Next: [[35 - Add item behaviors]]
