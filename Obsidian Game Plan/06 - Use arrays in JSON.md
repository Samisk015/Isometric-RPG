---
tags: [plan, json, modding]
---
# Use arrays in JSON

Unity's `JsonUtility` does not handle dictionaries reliably. Store extensible collections as arrays and turn them into dictionaries after loading.

Use:

```json
{
  "types": [
    { "id": "base:oak", "weight": 80 },
    { "id": "base:birch", "weight": 20 }
  ]
}
```

Avoid using IDs as JSON property names when those IDs need to be dynamic.

## Done when

- All mod-facing collections deserialize with `JsonUtility`.
- Runtime dictionaries are built only after validation.

Previous: [[05 - Add content validation]]  
Next: [[07 - Complete content registries]]
