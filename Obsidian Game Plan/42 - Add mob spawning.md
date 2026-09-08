---
tags: [plan, mobs, spawning, regions]
---
# Add mob spawning

Region feature rules should provide allowed mob IDs, selection weights, group-size ranges, and spawn density.

Spawn only on valid standing cells in loaded chunks, outside a minimum player distance, and below a per-chunk or global active-mob limit.

When a chunk unloads, remove temporary mobs or serialize persistent ones according to a clear rule.

## Done when

- Regions produce different configured mobs.
- Spawn placement uses normal standing-cell validation.
- Mob count remains bounded.

Previous: [[41 - Tick AI less frequently]]  
Next: [[43 - Separate resource packs from content mods]]
