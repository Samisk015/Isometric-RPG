---
tags: [plan, world-generation, features]
---
# Make features reusable generators

Create registered C# feature generators selected and configured by JSON. Initial types can include tree, plant patch, rock patch, ore vein, and mob spawn group.

Keep spawn density separate from weighted option selection. Density decides whether a feature is attempted; weights decide which configured variant is chosen.

## Done when

- A region can reference a tree feature from JSON.
- Oak and birch selection follows weights.
- Feature placement is deterministic from seed and coordinate.

Previous: [[30 - Add climate-based region selection]]  
Next: [[32 - Make features cross-chunk safe]]
