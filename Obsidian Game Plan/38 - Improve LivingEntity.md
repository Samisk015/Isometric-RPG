---
tags: [plan, entities, health]
---
# Improve LivingEntity

Initialize maximum health from the player or mob definition rather than relying only on an Inspector default.

Add damage, healing, and death events. Replace the simple integer damage event with a small context when sources become relevant. Start with amount and source entity; add damage type and direction only when needed.

Ensure death fires once and dead entities cannot heal unless a deliberate revive operation exists.

## Done when

- Mob definitions determine starting health.
- Damage and death callbacks fire exactly once per action.
- Death triggers cleanup or drops through another component.

Previous: [[37 - Use one GameObject per active entity]]  
Next: [[39 - Implement reusable AI types]]
