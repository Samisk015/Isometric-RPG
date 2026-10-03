local grass = {}

function grass.on_random_tick(ctx)
	local currentGrowthStage = ctx.state:get_number("growth_stage", 0)
	currentGrowthStage = currentGrowthStage + 1
	ctx.state:set_number("growth_stage", currentGrowthStage)

	-- Events.UpdateBlock(ctx.state:get_position())

	-- local above = ctx.pos:offset(0, 0, 1)

	-- if not ctx.world:is_air(above) then
	-- 	return
	-- end

	-- local target = ctx.pos:offset(ctx.random:range_int(-1, 1), ctx.random:range_int(-1, 1), 0)

	-- if ctx.world:block_has_tag(target, "base:grass_spread_target") then
	-- 	ctx.world:set_block(target, "base:grass")
	-- end
end

return grass
