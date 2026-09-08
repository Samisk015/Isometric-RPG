---
tags: [plan, modding, validation]
---
# Add content validation

Validate content after loading raw JSON but before starting the world.

Check for duplicate IDs, missing references, invalid stack sizes, unknown behavior IDs, unknown AI IDs, missing textures, invalid climate ranges, missing tag entries, and circular tag references.

Errors should include the mod and definition involved, for example:

`[Example Mod] Block example:blue_grass references missing texture example:blue_grass`

Collect errors into a report instead of stopping at the first exception.

## Done when

- Broken content produces readable messages.
- One broken optional definition does not hide unrelated errors.
- World creation is prevented only when required content is invalid.

Previous: [[04 - Replace all-or-nothing content loading]]  
Next: [[06 - Use arrays in JSON]]
