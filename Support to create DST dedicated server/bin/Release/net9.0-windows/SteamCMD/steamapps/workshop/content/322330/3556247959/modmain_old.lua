
PrefabFiles = 
{
	"display_attack_range"
}

local function rangeScale(self, rangeNum)
	local hitrangescale = math.sqrt(rangeNum * 300 / 1900)
	local s1, s2, s3 = self.inst.Transform:GetScale()
	if self and self.rangedisplay then
		if rangeNum ~= nil and rangeNum > 0 then
			self.rangedisplay.Transform:SetScale(hitrangescale/s1, hitrangescale/s2, hitrangescale/s3 )
		else
			self.rangedisplay.Transform:SetScale(0.001,0.001,0.001)
		end
	end
end

local function setColor(self, change)
	if self and self.rangedisplay then
		if change == true then
			self.rangedisplay.AnimState:SetMultColour(0, 0, 0, 1)
			self.rangedisplay.AnimState:SetAddColour(GetModConfigData("Red"), GetModConfigData("Green"), GetModConfigData("Blue"), 1)
		else
			self.rangedisplay.AnimState:SetMultColour(1, 1, 1, 1)
			self.rangedisplay.AnimState:SetAddColour(0, 0, 0, 0)
		end
	end
end

local function checkStates(sg)
	if sg == nil then return false end

	if sg:HasStateTag("moving") or sg:HasStateTag("running") or sg:HasStateTag("idle") then
		return true
	end

	if sg.currentstate == nil then return false end

	local currentstate = sg.currentstate.name
		
	local statelist = {"idle", "frozen", "walk", "run", "sleeping", "wake", "fossilized", "death", "taunt"}
	for i, v in ipairs(statelist) do
		if currentstate == v then
			return true
		end
	end
	return false
end

local function CreateRangeDisplay(self)
	if self and self.rangedisplay == nil then
		self.rangedisplay = GLOBAL.SpawnPrefab("display_attack_range")
		self.rangedisplay.entity:SetParent(self.inst.entity)
		self.rangedisplay.AnimState:SetLightOverride(0)
		self.rangedisplay.AnimState:SetSortOrder(1)
	end
end

local function RemoveRangeDisplay(self)
	if self and self.rangedisplay then
		self.rangedisplay:Remove()
		self.rangedisplay = nil
	end
end

local function AdjustRangeDisplay(self, showAttack)
	if self and self.rangedisplay then
		if self.inst.sg.currentstate and self.inst.sg.currentstate.name ~= "death" then
			if showAttack == true then
				if GetModConfigData("Type") == "attack" then
					rangeScale(self, self:GetAttackRange())
				else
					rangeScale(self, self:GetHitRange())
				end
				setColor(self, true)
			elseif showAttack == false or (not self.inst.sg:HasStateTag("attack") and checkStates(self.inst.sg)) then
				if  GetModConfigData("Display") == "attack" then
					self.rangedisplay.Transform:SetScale(0.001,0.001,0.001)
				elseif GetModConfigData("Type") == "hit" then
					rangeScale(self, self:GetHitRange())
				else
					rangeScale(self, self:GetAttackRange())
				end
				setColor(self)
			end
		else
			RemoveRangeDisplay(self)
		end
	end
end

local function DidCombat(self) 
	local OldSetTarget = self.SetTarget
	function did_SetTarget(self, target, ...) 
		if self.inst and self.inst:IsValid() then 
			if target ~= nil and target:HasTag("player") and not target:HasTag("playerghost") and target.components.health and target.components.health.currenthealth > 0 then
				CreateRangeDisplay(self)
			elseif self.rangedisplay then
				RemoveRangeDisplay(self)
			end
		else
			RemoveRangeDisplay(self)
		end
		return OldSetTarget(self, target, ...) 
	end
	self.SetTarget = did_SetTarget

	local OldGiveUp = self.GiveUp
	function did_GiveUp(self, ...)
		if self.rangedisplay then
			RemoveRangeDisplay(self)
		end
		return OldGiveUp(self, ...)
	end
	self.GiveUp = did_GiveUp

	local OldCanAttack = self.CanAttack
	function did_CanAttack(self, target, ...)
		if OldCanAttack(self, target, ...) == true then
			AdjustRangeDisplay(self, true)
		else
			AdjustRangeDisplay(self)
		end
		return OldCanAttack(self, target, ...)
	end
	self.CanAttack = did_CanAttack

	local OldDoAttack = self.DoAttack
	function did_DoAttack(self, targ, weapon, projectile, stimuli, instancemult, ...)
		AdjustRangeDisplay(self,false)
		return OldDoAttack(self, targ, weapon, projectile, stimuli, instancemult, ...)
	end
	self.DoAttack = did_DoAttack


	local OldTryAttack = self.TryAttack
	function did_TryAttack(self, target, ...)
		AdjustRangeDisplay(self)
		return OldTryAttack(self, target, ...)
	end
	self.TryAttack = did_TryAttack

	local OldTryRetarget = self.TryRetarget
	function did_TryRetarget(self, ...)
		AdjustRangeDisplay(self)
		return OldTryRetarget(self, ...)
	end
	self.TryRetarget = did_TryRetarget

	--[[
	local OldOnUpdate = self.OnUpdate
	function did_OnUpdate(self, dt, ...)
		AdjustRangeDisplay(self)
		return OldOnUpdate(self, dt, ...)
	end
	self.OnUpdate = did_OnUpdate
	]]

end 

local function DidProjectile(self) 
	local OldThrow = self.Throw
	function did_Throw(self, owner, target, attacker, ...) 
		if self.inst and self.inst:IsValid() then 
			if target ~= nil and target:HasTag("player") and not target:HasTag("playerghost") and target.components.health and target.components.health.currenthealth > 0 then
				CreateRangeDisplay(self)
				rangeScale(self, self.hitdist)
				setColor(self, true)
			elseif self.rangedisplay then
				RemoveRangeDisplay(self)
			end
		else
			RemoveRangeDisplay(self)
		end
		return OldThrow(self, owner, target, attacker, ...) 
	end
	self.Throw = did_Throw
end


AddComponentPostInit("combat" , function(self, inst) 
	if GLOBAL.TheSim:GetGameID() == "DST" and GLOBAL.TheWorld.ismastersim then 
		DidCombat(self)
	elseif GLOBAL.TheSim:GetGameID() ~= "DST" then
		DidCombat(self)
	end 
end)

AddComponentPostInit("projectile" , function(self, inst)
	if GetModConfigData("Projectile") then
		if GLOBAL.TheSim:GetGameID() == "DST" and GLOBAL.TheWorld.ismastersim then 
			DidProjectile(self)
		elseif GLOBAL.TheSim:GetGameID() ~= "DST" then
			DidProjectile(self)
		end 
	end
end)

