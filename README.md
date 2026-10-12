# Isometric-RPG

## Lua behaviour API

Each mob behaviour returns a table with optional `on_spawn(ctx)`,
`on_tick(ctx)`, and `on_interact(ctx)` callbacks. Mob callbacks receive:

- `ctx.self`: `id()`, `health()`, `is_alive()`, `position()`, `has_tag(tag_id)`.
- `ctx.state`: `has(key)` and matching `set_number`/`get_number`,
  `set_bool`/`get_bool`, `set_string`/`get_string`, and
  `set_position`/`get_position`. Setters take a key and value; getters take a
  key and optional fallback.
- `ctx.navigation`: `move_to(position)`, `stop()`, `is_moving()`.
- `ctx.player`: available in `on_interact`; check `ctx.player:is_valid()`
  before calling `get_gamemode()` or `set_gamemode(mode)`.
- `ctx.time.game_tick`: current mob decision tick.
- `ctx.random`: `range_int(minimum, maximum)` (inclusive) and `value()` (0
  inclusive to 1 exclusive).
- `ctx.world`: `get_temp_multiplier()`, `get_season()`, `is_air(position)`,
  `block_has_tag(position, tag_id)`, `set_block(position, block_id)`,
  `update_block_texture(position, texture_id)`, and `message(player, text)`.

Positions expose `x`, `y`, and `z` and support
`position:offset(dx, dy, dz)`. API methods and callback names use lowercase
snake_case; call methods with Lua's colon syntax.

Block behaviours can implement `on_random_tick(ctx)`. Their context provides
`ctx.pos`, `ctx.block`, `ctx.state`, `ctx.player`, `ctx.time`, `ctx.random`,
and `ctx.world`. `ctx.block` provides `id()` and `has_tag(tag_id)`. Block
state provides keyed number, boolean, and string `set_*`/`get_*` methods.

Mob definitions in `Assets/GameData/Base/mobs.json` use `id`, `behaviour`,
positive `maxHealth`, `tags`, `aggressionType`, `knockbackResistance` (0
through 1), `drops`, `customValues`, and `animations`. Animation entries are
IDs from the mob's animation group in `animations.json`, not inline animation
objects. Drop entries use `id`, `min`, `max`, and `chance` (0 through 1).

```lua
local cow = {}

function cow.on_spawn(ctx)
    ctx.state:set_number("next_wander_tick", ctx.time.game_tick)
end

function cow.on_tick(ctx)
    if ctx.time.game_tick < ctx.state:get_number("next_wander_tick", 0) then
        return
    end

    local target = ctx.self:position():offset(
        ctx.random:range_int(-4, 4),
        ctx.random:range_int(-4, 4),
        0)
    ctx.navigation:move_to(target)
    ctx.state:set_number("next_wander_tick", ctx.time.game_tick + 30)
end

return cow
```