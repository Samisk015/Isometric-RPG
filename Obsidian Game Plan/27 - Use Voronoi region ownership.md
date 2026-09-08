---
tags: [plan, world-generation, voronoi, regions]
---
# Use Voronoi region ownership

For a world column, examine deterministic Voronoi feature points in nearby cells. The closest point owns that column. The point's coordinates and world seed determine its region.

Also keep the second-nearest distance. `secondNearest - nearest` approaches zero at a region border and can later drive blending.

Do not build a global list containing only previously generated biome points; direct coordinate-based sampling must work for any chunk independently.

## Done when

- Region borders continue seamlessly across chunk boundaries.
- Sampling the same position always returns the same region point.

Previous: [[26 - Introduce a world seed]]  
Next: [[28 - Complete terrain shape definitions]]
