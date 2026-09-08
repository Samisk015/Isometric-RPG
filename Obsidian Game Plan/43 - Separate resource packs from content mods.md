---
tags: [plan, resources, modding]
---
# Separate resource packs from content mods

Content definitions reference namespaced asset IDs. Resource packs provide or override the texture registered under those IDs.

A texture pack should be able to replace `base:grass` without redefining the grass block. Define pack priority so later or higher-priority resource packs override earlier ones predictably.

## Done when

- Replacing a texture does not alter gameplay definitions.
- Missing assets use a visible fallback and loading warning.
- All resource lookups use full IDs.

Previous: [[42 - Add mob spawning]]  
Next: [[44 - Fix initialization ordering]]
