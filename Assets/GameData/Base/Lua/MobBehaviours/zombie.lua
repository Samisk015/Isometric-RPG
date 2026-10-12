local zombie = {}

function zombie.on_spawn(ctx)
	ctx.state:set_string("mode", "idle")
	ctx.state:set_number("next_wander_tick", ctx.time.game_tick)
end

function zombie.on_tick(ctx)
	if not ctx.self:is_alive() then
		ctx.navigation:stop()
		return
	end

	local nextTick = ctx.state:get_number("next_wander_tick", 0)
	if ctx.time.game_tick < nextTick then
		return
	end

	local current = ctx.self:position()
	local target = current:offset(
		ctx.random:range_int(-2, 2),
		ctx.random:range_int(-2, 2),
		0)

	ctx.navigation:move_to(target)
	ctx.state:set_number("next_wander_tick", ctx.time.game_tick + ctx.random:range_int(20, 40))
end

function zombie.on_interact(ctx)
	ctx.world:message(ctx.player, "The zombie growls.")
end

return zombie
