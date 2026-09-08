---
tags: [plan, mobs, ai, state]
---
# Store per-mob state

Use typed fields inside each built-in AI class for values such as home position, current target, next decision time, and current state.

Keep the current string-to-object custom-state dictionary only for cases that truly need dynamic data. Arbitrary objects are difficult to validate and save.

Persistent AI state must have an explicit save representation using supported values such as numbers, strings, IDs, booleans, and grid positions.

## Done when

- A passive mob remembers its own home position.
- Necessary state can be serialized without saving arbitrary C# references.

Previous: [[39 - Implement reusable AI types]]  
Next: [[41 - Tick AI less frequently]]
