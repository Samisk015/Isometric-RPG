---
tags: [plan, modding, documentation]
---
# Maintain an example mod

Keep one example mod that demonstrates the complete supported JSON surface:

- One block
- One item
- One tag addition
- One region
- One configured terrain shape
- One mob using a built-in AI type
- One resource texture

Make it part of loading tests so changes to the format reveal compatibility problems immediately.

## Done when

- A new mod author can copy the example and rename its namespace.
- The example loads without warnings.

Previous: [[52 - Create version-control checkpoints]]  
Next: [[54 - Document supported extension points]]
