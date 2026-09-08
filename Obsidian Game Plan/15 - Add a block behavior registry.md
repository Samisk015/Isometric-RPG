---
tags: [plan, blocks, behaviors, registries]
---
# Add a block behavior registry

Register reusable C# behavior implementations by namespaced ID. Block JSON contains an array of behavior IDs.

Start with simple behaviors such as `base:falling`, `base:flammable`, `base:rotatable`, and `base:placeable_on_support`.

Behaviors should normally be stateless singleton objects. Per-block changing state belongs to the `Block` instance or a dedicated block-state object.

## Done when

- Sand can select falling behavior from JSON.
- One block can combine multiple behaviors.
- Missing behavior IDs are caught during content validation.

Previous: [[14 - Include Player in interaction contexts]]  
Next: [[16 - Replace the block selection algorithm]]
