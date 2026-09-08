---
tags: [plan, git, workflow]
---
# Create version-control checkpoints

Commit after each independently working phase, such as definition loading, tags, world mutation, selection, chunk streaming, pathfinding, inventory, AI, and saving.

Do not combine several large rewrites in one commit. Before committing, run the relevant tests and a short Play Mode check.

Keep Unity `.meta` files committed and continue ignoring generated folders such as `Library`, `Temp`, and `Logs`.

## Done when

- Every major feature has a recoverable working checkpoint.
- Commit messages describe outcomes rather than vague work-in-progress changes.

Previous: [[51 - Profile individual systems]]  
Next: [[53 - Maintain an example mod]]
