---
tags: [plan, architecture, ids]
---
# Standardize content IDs

Use namespaced IDs everywhere, such as `base:grass` and `example:blue_stone`.

Inside a mod's own definition list, allow a local ID such as `grass`. Combine it with the namespace from `mod.json`. References to other definitions should use full IDs.

Create one ID utility that validates IDs, splits namespace from path, combines local IDs with a namespace, and prevents invalid results such as `base:base:forest`.

## Done when

- Registries use full IDs as keys.
- Asset IDs and definition IDs follow the same rules.
- Invalid IDs produce clear loading errors.

Previous: [[01 - Establish three separate object types]]  
Next: [[03 - Fix current definition issues]]
