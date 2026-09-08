---
tags: [plan, registries, architecture]
---
# Complete content registries

Create registries for blocks, items, mobs, regions, terrain shapes, block behaviors, item behaviors, mob AI types, tags, textures, and sprites.

Each definition registry should support:

- `Register`
- `Get`
- `TryGet`
- `Contains`
- Enumerating all definitions
- `Clear` for returning to the title screen or tests

Loading code should use `TryGet` so it can report friendly errors. Game code may use `Get` when a missing required definition is a programming error.

## Done when

- Every definition is located through a registry rather than a scattered dictionary.
- Registry duplicate policy is consistent.

Previous: [[06 - Use arrays in JSON]]  
Next: [[08 - Remove local ID asset collisions]]
