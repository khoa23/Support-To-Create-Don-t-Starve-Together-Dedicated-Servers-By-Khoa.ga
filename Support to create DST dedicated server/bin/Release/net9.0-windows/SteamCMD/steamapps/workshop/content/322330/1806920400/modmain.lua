local periodicity = GetModConfigData("periodicity") or 2
local amount = GetModConfigData("amount") or 1
local x, y

if periodicity == 1 then
	x = 60
	y = 300
elseif periodicity == 2 then
	x = 300
	y = 900
elseif periodicity == 3 then
	x = 900
	y = 1800
elseif periodicity == 4 then
	x = 1500
	y = 2500
end
	
local birds = {
	crow=1,
	robin=1,
	robin_winter=1,
	canary=1,
	puffin=1,
	pigeon=1,
	quagmire_pigeon=1,
}

local function OccupiableComponentPostInit(self)
	local function randomfeatherspawn(inst, bird)
		local feather
		if inst.feathertask ~= nil then
			inst.feathertask:Cancel()
			inst.feathertask = nil	
		end
		if bird ~= nil and birds[bird.prefab] then
			feather = (bird.prefab == "puffin" and "feather_crow") or ((bird.prefab == "pigeon" or bird.prefab == "quagmire_pigeon") and "feather_robin_winter") or "feather_" .. bird.prefab
		else
			return
		end
		local timerandom = math.floor(GLOBAL.GetRandomMinMax(x, y) + 0.5)
		inst.feathertask = inst:DoTaskInTime(timerandom, function()
			local i = 1
			while i <= amount do
				inst.components.lootdropper:SpawnLootPrefab(feather)
				i = i + 1
			end	
			inst.feathertask:Cancel()
			inst.feathertask = nil
			randomfeatherspawn(inst, bird)
		end)
	end

	function self:Occupy(occupier)
		if self.occupant == nil and occupier ~= nil and occupier.components.occupier ~= nil then
			self.occupant = occupier
			self.occupant.persists = true
			self.occupant.components.occupier:SetOwner(self.inst)
			if occupier.components.occupier.onoccupied ~= nil then
				occupier.components.occupier.onoccupied(occupier, self.inst)
			end
			if self.onoccupied ~= nil then
				self.onoccupied(self.inst, occupier)
			end
			randomfeatherspawn(self.inst, occupier)
			self.inst:AddChild(occupier)
			occupier:RemoveFromScene()
			occupier.occupiableonperish = function(occupier)
				self.inst:RemoveEventCallback("onremove", occupier.occupiableonremove, occupier)
				self.inst:RemoveChild(occupier)
				occupier:ReturnToScene()
				if self.onemptied ~= nil then
					self.onemptied(self.inst)
				end
				if self.onperishfn then
					self.onperishfn(self.inst, self.occupant)
				end
				self.occupant = nil
				occupier:Remove()
				if self.inst.feathertask ~= nil then
					self.inst.feathertask:Cancel()
					self.inst.feathertask = nil
				end	
			end
			occupier.occupiableonremove = function(occupier)
				self.inst:RemoveEventCallback("perished", occupier.occupiableonperish, occupier)
				if self.onemptied ~= nil then
					self.onemptied(self.inst)
				end
				self.occupant = nil
				if self.inst.feathertask ~= nil then
					self.inst.feathertask:Cancel()
					self.inst.feathertask = nil
				end	
			end
			self.inst:ListenForEvent("perished", occupier.occupiableonperish, occupier)
			self.inst:ListenForEvent("onremove", occupier.occupiableonremove, occupier)
			return true
		end
	end

	function self:Harvest()
		if self.occupant ~= nil and self.occupant.components.inventoryitem ~= nil then
			local occupant = self.occupant
			occupant.components.occupier:SetOwner(nil)
			self.occupant = nil
			self.inst:RemoveEventCallback("perished", occupant.occupiableonperish, occupant)
			self.inst:RemoveEventCallback("onremove", occupant.occupiableonremove, occupant)
			self.inst:RemoveChild(occupant)
			if self.onemptied ~= nil then
				self.onemptied(self.inst)
			end
			occupant:ReturnToScene()
			if self.inst.feathertask ~= nil then
				self.inst.feathertask:Cancel()
				self.inst.feathertask = nil
			end	
			return occupant
		end
	end
end
AddComponentPostInit("occupiable", OccupiableComponentPostInit)