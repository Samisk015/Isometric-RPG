---
tags: [plan, selection, isometric]
---
# Replace the block selection algorithm

`WorldToCell` alone cannot identify which isometric cube owns visible side pixels. Build a `BlockPicker` that finds nearby candidate blocks, constructs their visible top/left/right face polygons, tests the mouse against them, and selects the visually frontmost match.

Return both the logical block coordinate and face. Keep sprite dimensions and face offsets in one selection configuration rather than scattering constants through player movement.

## Done when

- Clicking a visible side selects the block owning that side.
- Clicking a top selects the correct solid block.
- Overlapping candidates choose the visually frontmost block.

Previous: [[15 - Add a block behavior registry]]  
Next: [[17 - Use one selection result for every interaction]]
