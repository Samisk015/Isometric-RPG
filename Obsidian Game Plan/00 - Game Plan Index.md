# Isometric RPG development plan

This vault folder breaks the prototype roadmap into one actionable note per implementation point. Complete the notes roughly in numerical order. Each note links to the next step.

## Foundation

- [[01 - Establish three separate object types]]
- [[02 - Standardize content IDs]]
- [[03 - Fix current definition issues]]
- [[04 - Replace all-or-nothing content loading]]
- [[05 - Add content validation]]
- [[06 - Use arrays in JSON]]
- [[07 - Complete content registries]]
- [[08 - Remove local ID asset collisions]]

## Tags and world mutation

- [[09 - Add typed tag keys]]
- [[10 - Load tag files]]
- [[11 - Centralize world mutations]]
- [[12 - Separate logical and rendered positions]]
- [[13 - Define block behavior callbacks]]
- [[14 - Include Player in interaction contexts]]
- [[15 - Add a block behavior registry]]

## Selection and movement

- [[16 - Replace the block selection algorithm]]
- [[17 - Use one selection result for every interaction]]
- [[18 - Separate input from selection]]
- [[19 - Define valid standing cells]]
- [[20 - Add grid pathfinding]]
- [[21 - Track player chunk changes]]

## Chunk streaming and performance

- [[22 - Replace ChunkManager Test]]
- [[23 - Limit chunk work per frame]]
- [[24 - Batch Tilemap updates]]
- [[25 - Properly unload chunks]]

## World generation

- [[26 - Introduce a world seed]]
- [[27 - Use Voronoi region ownership]]
- [[28 - Complete terrain shape definitions]]
- [[29 - Add region surface rules]]
- [[30 - Add climate-based region selection]]
- [[31 - Make features reusable generators]]
- [[32 - Make features cross-chunk safe]]

## Items and inventory

- [[33 - Replace Item with ItemStack]]
- [[34 - Prefer item tags over a fixed purpose enum]]
- [[35 - Add item behaviors]]
- [[36 - Add inventory]]

## Entities and mobs

- [[37 - Use one GameObject per active entity]]
- [[38 - Improve LivingEntity]]
- [[39 - Implement reusable AI types]]
- [[40 - Store per-mob state]]
- [[41 - Tick AI less frequently]]
- [[42 - Add mob spawning]]

## Rendering and resources

- [[43 - Separate resource packs from content mods]]
- [[44 - Fix initialization ordering]]
- [[45 - Configure isometric rendering]]

## Saving, UI, testing, and mod support

- [[46 - Save the world seed and modifications]]
- [[47 - Save IDs instead of object references]]
- [[48 - Build a single interaction controller]]
- [[49 - Add the prototype UI]]
- [[50 - Add small automated tests]]
- [[51 - Profile individual systems]]
- [[52 - Create version-control checkpoints]]
- [[53 - Maintain an example mod]]
- [[54 - Document supported extension points]]

## Prototype milestone

The player can enter a deterministic world, correctly select any visible block face, walk to valid top surfaces, break and place blocks through an inventory, cross chunk boundaries smoothly, quit, and reload the changed world.
