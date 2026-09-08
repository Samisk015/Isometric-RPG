---
tags: [plan, items, inventory]
---
# Replace Item with ItemStack

An item definition describes a type of item. A runtime `ItemStack` stores a definition reference and changing state such as amount, durability, enchantments, and later custom data.

Keep display name, base lore, maximum stack size, texture ID, tags, and behavior IDs in `ItemDefinition`.

Use methods to change amount so invalid negative or over-limit stacks cannot be created accidentally.

## Done when

- Two stacks can refer to the same immutable definition.
- Stack amount is correctly assigned and validated.
- Empty stacks have one consistent representation.

Previous: [[32 - Make features cross-chunk safe]]  
Next: [[34 - Prefer item tags over a fixed purpose enum]]
