---
tags: [plan, cleanup, definitions]
---
# Fix current definition issues

Complete unfinished constructors and remove unclear duplicate types before extending them.

## Tasks

- Complete `ItemDefinition`, `EntityDefinition`, `MobDefinition`, and `EnchantmentDefinition`.
- Make the `Item` constructor assign its starting amount.
- Remove the empty `IBlockInterface` unless it receives a real responsibility.
- Make `Region` plain data rather than a `MonoBehaviour`.
- Decide whether the current `Entity` class is needed alongside `LivingEntity`, `Mob`, and `Player`.
- Prefer `int` for stack amounts and stack-size limits.
- Change `TryGetBlock` to return the found block through an `out` argument.

## Done when

- Every constructor produces a valid object.
- There are no empty placeholder types in the active gameplay model.
- The project compiles without relying on unfinished definitions.

Previous: [[02 - Standardize content IDs]]  
Next: [[04 - Replace all-or-nothing content loading]]
