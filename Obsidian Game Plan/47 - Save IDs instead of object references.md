---
tags: [plan, saving, modding]
---
# Save IDs instead of object references

Save namespaced definition IDs and resolve them through registries when loading. Never serialize `BlockDefinition`, `ItemDefinition`, or other definition objects directly.

Provide visible fallback definitions such as `base:missing_block` and `base:missing_item`. If a mod is removed, preserve enough information to warn the player without making the entire save unreadable.

## Done when

- Save data remains plain and versionable.
- Missing mod content produces fallbacks and warnings rather than crashes.

Previous: [[46 - Save the world seed and modifications]]  
Next: [[48 - Build a single interaction controller]]
