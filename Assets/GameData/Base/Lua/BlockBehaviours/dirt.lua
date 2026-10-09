local dirt = {}

function dirt.on_random_tick(ctx)
	local currentGrowthStage = ctx.state:get_number("growth_stage", 0)
	currentGrowthStage = currentGrowthStage + 1
	ctx.state:set_number("growth_stage", currentGrowthStage)
	ctx
end

return dirt
