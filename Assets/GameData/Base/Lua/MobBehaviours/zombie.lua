local Zombie = {}

-- idea is block and mob (and other) behaviour .lua scripts operate on all available Mobs / blocks sharing the same behaviour to avoid lag and save memory
-- structure of mob object
-- "uniqueId" :  {
--     "position" == Vector3Int,
--     "health" == int,
--     "state" == {
--         "stateName" = value
--     }
--     possibly other variables
-- }
Health = 1
Zombie.Defense = 10
Zombie.Position = { 0, 0, 0 }

Zombie.State = "Idle"

Zombie.Helmet = nil
Zombie.Leggings = nil
Zombie.Chestplate = nil
Zombie.Boots = nil

function Zombie.OnSpawn() end

function Zombie:SunBurn()
	if self.Health ~= nil then
		self:Damage()
		if self.State ~= "OnFire" then
			print("On fire")
			self.State = "OnFire"
		end
	end
end

-- ridiculius

function Zombie:Damage(amount)
	self.Health = self.Health - amount
end

function Zombie:UpdateState(stateName, value)
	if self.state[stateName] ~= nil then
		self.state[stateName] = value
	end
end

function Zombie:OnTick()
	if game.TimeOfDay == "day" then
		self:SunBurn()
	elseif self.State == "OnFire" then
		self.State = "Extinguished"
	end
end

return Zombie
