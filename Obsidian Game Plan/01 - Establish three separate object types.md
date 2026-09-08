---
tags: [plan, architecture, definitions]
---
# Establish three separate object types

Use three layers for every content category:

1. A JSON data class such as `BlockDefinitionData` that exactly matches the file format.
2. A resolved definition such as `BlockDefinition` that contains validated IDs and resolved references.
3. A runtime instance such as `Block` that contains changing state.

Apply the same pattern to items, mobs, regions, and terrain shapes. Definitions stay as plain C# objects. Only active scene objects inherit `MonoBehaviour`.

## Done when

- Every content type clearly follows Data -> Definition -> Runtime.
- JSON data objects are not used directly by gameplay.
- Definitions contain no scene-specific state.

Next: [[02 - Standardize content IDs]]
