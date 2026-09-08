---
tags: [plan, modding, loading]
---
# Replace all-or-nothing content loading

Only `mod.json` should be mandatory. A block-only mod must not fail because it has no `regions.json`.

## Loading sequence

1. Discover the base content pack and mod folders.
2. Read manifests.
3. Determine load order.
4. Load whichever optional definition files exist.
5. Register raw definitions.
6. Load tags.
7. Resolve references.
8. Load resource assets.
9. Produce one loading report.

Optional files can include `blocks.json`, `items.json`, `mobs.json`, `regions.json`, and `terrain_shapes.json`.

## Done when

- The example mod can load with only `mod.json` and `blocks.json`.
- Missing optional files do not produce errors.
- Missing `mod.json` rejects only that mod.

Previous: [[03 - Fix current definition issues]]  
Next: [[05 - Add content validation]]
