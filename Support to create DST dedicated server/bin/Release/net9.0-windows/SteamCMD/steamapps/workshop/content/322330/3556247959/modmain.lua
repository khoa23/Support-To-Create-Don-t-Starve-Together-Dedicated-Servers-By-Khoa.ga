PrefabFiles = {
    "display_attack_range",
}

-- ========= Utilities =========
local function rangeScale(self, rangeNum)
    local s1, s2, s3 = self.inst.Transform:GetScale()
    local scale
    if type(rangeNum) == "number" and rangeNum > 0 then
        local hitrangescale = math.sqrt(rangeNum * 300 / 1900)
        scale = hitrangescale
    else
        scale = 0.001
    end
    if self and self.rangedisplay then
        self.rangedisplay.Transform:SetScale(scale / s1, scale / s2, scale / s3)
    end
end

local function setColor(self, attacking)
    if not (self and self.rangedisplay) then return end
    if attacking then
        self.rangedisplay.AnimState:SetMultColour(0, 0, 0, 1)
        self.rangedisplay.AnimState:SetAddColour(GetModConfigData("Red"), GetModConfigData("Green"), GetModConfigData("Blue"), 1)
    else
        self.rangedisplay.AnimState:SetMultColour(1, 1, 1, 1)
        self.rangedisplay.AnimState:SetAddColour(0, 0, 0, 0)
    end
end

local function CreateRangeDisplay(self)
    if self and self.rangedisplay == nil then
        self.rangedisplay = GLOBAL.SpawnPrefab("display_attack_range")
        if self.rangedisplay ~= nil then
            self.rangedisplay.entity:SetParent(self.inst.entity)
            self.rangedisplay.AnimState:SetLightOverride(0)
            self.rangedisplay.AnimState:SetSortOrder(1)
        end
    end
end

local function RemoveRangeDisplay(self)
    if self and self.rangedisplay then
        self.rangedisplay:Remove()
        self.rangedisplay = nil
    end
end

local function GetHitRange(self)
    if self.GetHitRange then
        return self:GetHitRange()
    end
    return self.hitrange or 0
end

local function GetAttackRange(self)
    if self.GetAttackRange then
        return self:GetAttackRange()
    end
    return self.attackrange or GetHitRange(self)
end

-- ========= Aggro/Attack detection =========
local function HasAggro(self)
    local t = self.target
    if t == nil or not t:IsValid() then return false end
    if t:HasTag("playerghost") then return false end
    local hp = t.components and t.components.health
    if hp == nil or hp.currenthealth <= 0 then return false end
    return t:HasTag("player")
end

local function IsAttackingSG(sg)
    if sg == nil or sg.currentstate == nil then return false end
    if sg:HasStateTag("attack") or sg:HasStateTag("throw") or sg:HasStateTag("abouttoattack") then
        return true
    end
    local name = sg.currentstate.name
    return name == "attack" or name == "throw" or name == "premove" or name == "abouttoattack"
end

-- ========= Core updater (runs only while aggro is held) =========
local function DAR_Update(self)
    if not (self and self.inst and self.inst:IsValid()) then
        RemoveRangeDisplay(self); return
    end
    -- Hide everything if aggro lost
    if not HasAggro(self) then
        RemoveRangeDisplay(self); return
    end

    -- Always show ring during aggro
    CreateRangeDisplay(self)

    -- Choose which radius to render based on config
    local typ = GetModConfigData("Type")
    if typ == "hit" then
        rangeScale(self, GetHitRange(self))
    elseif typ == "attack" then
        rangeScale(self, GetAttackRange(self))
    else -- both -> default to attack radius for the ring
        rangeScale(self, GetAttackRange(self))
    end

    -- Color: red for the entire attack animation, default otherwise
    local attacking = IsAttackingSG(self.inst.sg)
    setColor(self, attacking)
end

local function StartDAR(self)
    if self._dar_task == nil then
        -- Fast but light: 20 Hz while aggro is held
        self._dar_task = self.inst:DoPeriodicTask(0.05, function() DAR_Update(self) end)
    end
end

local function StopDAR(self)
    if self._dar_task ~= nil then
        self._dar_task:Cancel()
        self._dar_task = nil
    end
end

-- Keep display tidy on death or removal
local function OnInstRemoved(inst, self)
    StopDAR(self)
    RemoveRangeDisplay(self)
end

-- =============== Combat component hooks (server side) ===============
local function DidCombat(self)
    -- Clean up when the entity is removed
    self.inst:ListenForEvent("onremove", function(inst) OnInstRemoved(inst, self) end)

    local OldSetTarget = self.SetTarget
    function self:SetTarget(target, ...)
        local ret = OldSetTarget(self, target, ...)
        if HasAggro(self) then
            CreateRangeDisplay(self)
            StartDAR(self)       -- start periodic updater while aggro is held
        else
            StopDAR(self)
            RemoveRangeDisplay(self)
        end
        return ret
    end

    local OldGiveUp = self.GiveUp
    function self:GiveUp(...)
        StopDAR(self)
        RemoveRangeDisplay(self)
        return OldGiveUp(self, ...)
    end

    -- When the mob performs an attack attempt, the periodic updater will pick up
    -- the 'attack' state and keep the circle red for the whole animation, so we
    -- only need to nudge an update here (no forced flip).
    local OldTryAttack = self.TryAttack
    function self:TryAttack(target, ...)
        local ret = OldTryAttack(self, target, ...)
        DAR_Update(self)
        return ret
    end

    local OldTryRetarget = self.TryRetarget
    function self:TryRetarget(...)
        local ret = OldTryRetarget(self, ...)
        DAR_Update(self)
        return ret
    end
end

-- =============== Projectile hooks (server side) ===============
local function DidProjectile(self)
    local OldThrow = self.Throw
    function self:Throw(owner, target, attacker, ...)
        -- Show ring on throws only if aggro is valid
        local ret = OldThrow(self, owner, target, attacker, ...)
        if HasAggro(attacker and attacker.components and attacker.components.combat or self) then
            CreateRangeDisplay(self)
            local dist = self.hitdist or (self.throwrange or 0)
            rangeScale(self, dist)
            setColor(self, true) -- throwing counts as attack; periodic will revert color after
        end
        return ret
    end
end

-- Register postinits
AddComponentPostInit("combat", function(self, inst)
    if GLOBAL.TheSim:GetGameID() == "DST" then
        if GLOBAL.TheWorld.ismastersim then
            DidCombat(self)
        end
    else
        -- DS single-player
        DidCombat(self)
    end
end)

AddComponentPostInit("projectile", function(self, inst)
    if not GetModConfigData("Projectile") then return end
    if GLOBAL.TheSim:GetGameID() == "DST" then
        if GLOBAL.TheWorld.ismastersim then
            DidProjectile(self)
        end
    else
        DidProjectile(self)
    end
end)
