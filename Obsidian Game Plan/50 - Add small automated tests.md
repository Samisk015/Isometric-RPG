---
tags: [plan, testing]
---
# Add small automated tests

Prioritize tests for logic that can silently corrupt content or worlds:

- Content ID parsing
- Duplicate registry behavior
- Tag merging and nested resolution
- Negative chunk-coordinate conversion
- Same-seed generation
- Generation-order independence
- Item-stack merging
- Face-to-placement offset conversion
- Basic pathfinding over flat and stepped terrain

Keep these systems in plain C# where possible so tests do not require a full scene.

## Done when

- Tests run from Unity's Test Runner.
- Deterministic generation has regression coverage.

Previous: [[49 - Add the prototype UI]]  
Next: [[51 - Profile individual systems]]
