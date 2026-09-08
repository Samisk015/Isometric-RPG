---
tags: [plan, tags, registries]
---
# Add typed tag keys

A tag key is a typed identifier for a named group. It is not the group contents.

```csharp
public readonly record struct TagKey<T>(string Id);
```

Create separate tag registries for blocks, items, mobs, and regions. Define static keys for tags used frequently by C# code, while still allowing mods to introduce new keys from JSON.

Initial block tags might include `base:logs`, `base:replaceable`, `base:flammable`, and `base:supports_sugar_cane`.

## Done when

- C# can ask whether a definition belongs to a typed tag.
- A block tag cannot accidentally be passed to an item-tag lookup.

Previous: [[08 - Remove local ID asset collisions]]  
Next: [[10 - Load tag files]]
