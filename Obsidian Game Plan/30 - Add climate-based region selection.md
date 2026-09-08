---
tags: [plan, world-generation, climate]
---
# Add climate-based region selection

Generate deterministic temperature and humidity for each Voronoi region point. Find region definitions whose ranges contain those values, then choose deterministically among the matches.

Provide a fallback region so a bad set of ranges never leaves terrain without a definition. Choose and document whether temperature is normalized or expressed in another scale.

## Done when

- Region climate ranges affect placement.
- Overlapping matches are resolved deterministically.
- Missing matches use a clear fallback and warning.

Previous: [[29 - Add region surface rules]]  
Next: [[31 - Make features reusable generators]]
