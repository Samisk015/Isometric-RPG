---
tags: [plan, tags, json]
---
# Load tag files

Load separate JSON files containing `replace` and `values`.

```json
{
  "replace": false,
  "values": ["#base:dirt", "base:sand"]
}
```

A normal ID adds one definition. An ID beginning with `#` includes another tag. Merge files for the same tag unless `replace` is true.

Resolve nested tags after all mods have loaded. Detect missing IDs and cycles such as tag A including tag B while tag B includes tag A.

## Done when

- Mods can append their blocks to base tags.
- Nested tags resolve consistently regardless of mod load order.

Previous: [[09 - Add typed tag keys]]  
Next: [[11 - Centralize world mutations]]
