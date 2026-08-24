local game = {}

local services = {}

function game:GetService(name)
    local service = services[name]

    if not service then
        error(("Unknown service '%s'"):format(tostring(name)), 2)
    end

    return service
end

return game, services