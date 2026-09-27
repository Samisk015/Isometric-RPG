local cow = {}

function cow.on_spawn(ctx)
	ctx.state:set_number("next_wander_tick", ctx.time.game_tick)
end

function cow.on_tick(ctx)
	local nextTick = ctx.state:get_number("next_wander_tick", 0)

	if ctx.time.game_tick < nextTick then
		return
	end

	local target = ctx.self:position():offset(ctx.random:range_int(-4, 4), ctx.random:range_int(-4, 4), 0)

	ctx.navigation:move_to(target)
	ctx.state:set_number("next_wander_tick", ctx.time.game_tick + 30)
end

function cow.on_interact(ctx)
	ctx.world:message(ctx.player, "The cow looks at you.")
end

return cow
