---
tags: [plan, modding, documentation]
---
# Document supported extension points

Document exactly what JSON mods can and cannot add.

## Supported

- Blocks, items, mobs, regions, and terrain-shape configurations
- Tag membership
- Feature configurations
- Mobs using registered AI types
- Blocks using registered behaviors
- Items using registered behaviors
- Resource assets

## Not supported in the prototype

- New C# algorithms
- New behavior implementations
- New AI implementations
- New terrain-generator implementations
- Runtime scripting

Also document load order, namespaced IDs, file locations, validation messages, and save compatibility expectations.

## Done when

- Mod capabilities are predictable rather than implied.
- Unsupported requests fail with a useful error.

Previous: [[53 - Maintain an example mod]]  
Back to: [[00 - Game Plan Index]]
