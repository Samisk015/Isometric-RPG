local Mob = {}
Mob.__index = Mob

function Mob.wrap(nativeMob)
    return setmetatable({
        native = nativeMob
    }, Mob)
end

function Mob:getPosition()
    return self.native:GetPosition()
end

function Mob:moveTo(position)
    self.native:MoveTo(position)
end

function Mob:moveRandomly()
    self.native:MoveRandomly()
end

function Mob:isAlive()
    return self.native:IsAlive()
end

function Mob:say(message)
    self.native:Say(message)
end

return Mob