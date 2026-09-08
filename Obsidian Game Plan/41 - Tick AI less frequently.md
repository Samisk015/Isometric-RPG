---
tags: [plan, mobs, ai, performance]
---
# Tick AI less frequently

Separate smooth movement from decisions. Movement may update every frame, but AI target selection and path requests should run several times per second or only when events occur.

Give the AI controller a configurable decision interval and distribute mob ticks across frames to avoid spikes.

## Done when

- Mobs move smoothly while decisions run less often.
- Many mobs do not all request paths on the same frame.

Previous: [[40 - Store per-mob state]]  
Next: [[42 - Add mob spawning]]
