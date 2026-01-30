local _G = GLOBAL
local TheNet = _G.TheNet
local TheSim = _G.TheSim

if not (TheNet:GetIsServer() or TheNet:IsDedicated()) then
	return
end


local smokePuffs = GetModConfigData("SmokePuffOnStacking")


local autoStackEnabled = GetModConfigData("AutoStackEnabled")
local stackDuringPopulation = GetModConfigData("StackDuringPopulation")
local autoStackRange = GetModConfigData("AutoStackRange")
local autoStackMakeNewStackMainStack = GetModConfigData("AutoStackMakeNewStackMainStack")
local autoStackTwiggyTreeTwigs = GetModConfigData("AutoStackTwiggyTreeTwigs")
local autoStackAsh = GetModConfigData("AutoStackAsh")
local autoStackPoop = GetModConfigData("AutoStackPoop")
local autoStackSeeds = GetModConfigData("AutoStackSeeds")
local denyAutoStack = {}
denyAutoStack["seeds"] = not autoStackSeeds
denyAutoStack["poop"] = not autoStackPoop
denyAutoStack["ash"] = not autoStackAsh


local manualDropAutoStackEnabled = GetModConfigData("AutoStackManuallyDroppedItems")
local manualDropStackRange = GetModConfigData("ManualDropStackRange")
local manualStackMakeNewStackMainStack = GetModConfigData("ManualStackMakeNewStackMainStack")
local manualStackAsh = GetModConfigData("ManualStackAsh")
local manualStackPoop = GetModConfigData("ManualStackPoop")
local manualStackSeeds = GetModConfigData("ManualStackSeeds")
local denyManualStack = {}
denyManualStack["seeds"] = not manualStackSeeds
denyManualStack["poop"] = not manualStackPoop
denyManualStack["ash"] = not manualStackAsh


local autoPickupEnabled = GetModConfigData("AutoPickupEnabled")
local autoPickupRange = GetModConfigData("AutoPickupRange")
local playerMustHaveOneOfItemToAutoPickup = GetModConfigData("PlayerMustHaveOneOfItemToAutoPickup")
local autoPickupAsh = GetModConfigData("AutoPickupAsh")
local autoPickupPoop = GetModConfigData("AutoPickupPoop")
local autoPickupSeeds = GetModConfigData("AutoPickupSeeds")
local denyAutoPickup = {}
denyAutoPickup["seeds"] = not autoPickupSeeds
denyAutoPickup["poop"] = not autoPickupPoop
denyAutoPickup["ash"] = not autoPickupAsh


local SpecialCreatureChecks = function(inst)
	return not (
		(inst.prefab == "mandrake" and not inst:HasTag("item"))
	)
end

local ShouldIgnoreEntity = function(inst)
	return 
	inst.components.locomotor
	or inst.components.heavyobstaclephysics
	or inst.components.trap
	or (inst.components.bait and inst.components.bait.trap ~= nil)
	or inst.prefab == "fireflies"
	or inst:HasTag("groundspike")
	or inst:HasTag("projectile")
	or inst:HasTag("trap")
	or inst:HasTag("smallcreature")
	or inst:HasTag("small_livestock")
	or inst:HasTag("no_autostack_all")
end

-- This is for edgecases where we don't want to allow Auto-Stacking or -Pickup,
-- but still want to retain Manual Stacking.
local IsAutoPickupOrStackingBlockedByContext = function(inst)
	-- Penguins keep track of the amount of egg-stacks around them, and spawn more until a certain amount is met.
	-- If we allow auto-stacking of those when they're spawned, the penguins end up making a stack of 33-40 eggs per winter,
	-- because they'll never be happy with the number of stacks.
	-- penguinbrain.lua adds a special "penguin_egg" tag when it spawns an egg, whether that be a bird_egg or a rottenegg (lunar version),
	-- so we can easily skip auto-stacking and -pickup for them, by checking for that tag.
	if inst:HasTag("penguin_egg") then
		return true
	end
	return false
end

local function PlaySmokePuff(x, y, z)
	local start_fx = _G.SpawnPrefab("small_puff")
	start_fx.Transform:SetPosition(x, y, z)
	start_fx.Transform:SetScale(.5, .5, .5)
end

local HasAtleastOneOf = function (inventoryComp, prefab)
	-- print("Checking if player has at least one "..prefab.." in inventory already...")
    for k, v in pairs(inventoryComp.itemslots) do
        if v and v.prefab == prefab then
			-- print("Player has one of the item in their inventory!")
			return true
        end
    end

    local overflow = inventoryComp:GetOverflowContainer()
    if overflow ~= nil then
		for k,v in pairs(overflow.slots) do
			if v and v.prefab == prefab then
				-- print("Player has one of the item in their backpack!")
				return true
			end
		end
    end

	-- print("Player DOES NOT have one of the item already!")
    return false
end

local function TryAddToStack(item, target)
	if item and target and item:IsValid() and target:IsValid() -- basic nil- and validity checks
	and target ~= item and target.prefab == item.prefab -- make sure they are not the same instance, but use the same prefab.
	and item.components.inventoryitem and item.components.inventoryitem.owner == nil -- do not stack, if the item is in an inventory
	and target.components.inventoryitem and target.components.inventoryitem.owner == nil -- do not stack, if the target is in an inventory
	and item.components.stackable and not item.components.stackable:IsFull() -- do not stack, if the item is already a full stack
	and target.components.stackable and not target.components.stackable:IsFull() -- do not stack, if the target stack is already full
	and (target.components.perishable == nil or item.components.perishable ~= nil) -- do not auto-stack a non-perishable item into a perishable stack.
	and (item.components.bait == nil or item.components.bait.trap == nil) -- do not auto-stack bait-items when they are put into traps.
	and (target.components.bait == nil or target.components.bait.trap == nil) -- do not auto-stack items into bait-items lying in traps.
	and SpecialCreatureChecks(item) and SpecialCreatureChecks(target)
	then
		-- print("Stacking "..item.prefab)
		target.components.stackable:Put(item, _G.Vector3(TheSim:GetScreenPos(item.Transform:GetWorldPosition())))
		return true
	end
	return false
end

local function AutoStack(inst, range, makeNewStackMainStack)
	if inst:IsValid() and inst.components.inventoryitem	and inst.components.inventoryitem.owner == nil and inst.components.inventoryitem.canbepickedup 
	and inst.components.stackable and not inst.components.stackable:IsFull()
	and not inst:HasTag("fire") then
		local x, y, z = inst:GetPosition():Get()
		local ents = TheSim:FindEntities(x, y, z, range, { "_inventoryitem" }, { "INLIMBO", "NOCLICK", "catchable", "fire" })
		if makeNewStackMainStack then
			-- print("Stacking surrounding items into new item...")
			for _,obj in pairs(ents) do
				if TryAddToStack(obj, inst) then
					if smokePuffs then
						local x1, y1, z1 = obj:GetPosition():Get()
						PlaySmokePuff(x1, y1, z1)
					end
					if inst.components.stackable:IsFull() then
						break
					end
				end
			end
		else
			-- print("Stacking new item into surrounding items...")
			local wasStacked = false
			for _,obj in pairs(ents) do
				if TryAddToStack(inst, obj) then
					wasStacked = true
				end
				-- If the item has been removed from the world, don't try to add itself to more stacks.
				-- Otherwise, the first stack it tried to add itself to was (or became) full when stacking,
				-- so we want to see if there's another stack we can add it to (continue the for-loop.
				if not inst:IsValid() then
					break
				end
			end
			if wasStacked and smokePuffs then
				PlaySmokePuff(x, y, z)
			end
		end
	end
end

local function AutoPickup(inst, range)
	local closestPlayer = nil
	
	if not inst:IsValid() then
		-- print("Auto-pickup failed. Item is no longer valid!")
		return
	else
		-- print("Auto-pickup new item: "..inst.prefab)
	end
	
	if inst:HasTag("fire") then
		-- print("Auto-pickup failed. Item is on fire!")
		return
	end
	
	if (not inst.components.stackable or not inst.components.inventoryitem) then
		-- print("Auto-pickup failed. Item is not stackable or not an inventoryitem!")
	elseif inst.components.inventoryitem.owner ~= nil then
		-- print("Auto-pickup failed. Item has an owner, "..inst.components.inventoryitem.owner.prefab)
	elseif not inst.components.inventoryitem.canbepickedup then
		-- print("Auto-pickup failed. Item has canbepickedup set to false")
	else
		local x, y, z = inst:GetPosition():Get()
		local playersInRange = _G.FindPlayersInRange(x, y, z, range, true)
		for _,player in ipairs(playersInRange) do
			local playerIsBeaver = player.components.beaverness and player.isbeavermode:value()
			if not playerIsBeaver then
				if player.components.inventory and (not playerMustHaveOneOfItemToAutoPickup or HasAtleastOneOf(player.components.inventory, inst.prefab)) then
					closestPlayer = player
					break
				end
			else
				-- print("Player is in beaver mode. Skipping...")
			end
		end
		
		if closestPlayer == nil then
			-- print("No eligible players found within range.")
		else
			-- print("Eligible player found!")
			local result = closestPlayer.components.inventory:GiveItem(inst, nil, _G.Vector3(TheSim:GetScreenPos(inst.Transform:GetWorldPosition())))
			-- print("Raw GiveItem result: "..tostring(result))
			result = (result ~= nil and result ~= false)
			-- print("Sanitized GiveItem result: "..tostring(result))
			if result and smokePuffs then
				PlaySmokePuff(x, y, z)
			end
			return result
		end
	end
	return false
end

local DoAutoPickUpOrAutoStack = function(inst, shouldAutoPickup, shouldStack)
	local autoPickupDone = false
	if shouldAutoPickup and not denyAutoPickup[inst.prefab] then
		autoPickupDone = AutoPickup(inst, autoPickupRange)
	end
	
	if shouldStack and not autoPickupDone and not denyAutoStack[inst.prefab] then
		AutoStack(inst, autoStackRange, autoStackMakeNewStackMainStack)
	end
end

AddComponentPostInit("stackable", function(comp)
	local shouldStack = autoStackEnabled
	local shouldAutoPickup = autoPickupEnabled
	
	if _G.POPULATING then
		shouldStack = (shouldStack and stackDuringPopulation)
		shouldAutoPickup = false
	end
	
	local compInst = comp.inst
	
	-- Edge case fix. When holding CTRL and left-clicking on the ground with an item in your hands,
	-- it subtracts 1 from the stack being held in the mouse, and spawns a new prefab to be that new
	-- instance. It doesn't drop the instance, but spawns it directly to the ground.
	-- This makes it NOT behave like a normal manual drop, and so it is automatically picked up or stacked.
	-- I fix this by using the fact that Stackable:Get(num), which creates this new instance, calls
	-- ondestack on it afterwards. So, I set a bool on the instance, and then use it in the next frame
	-- (my DoTaskInTime) to identify whether this spawned item was just destacked, and if so, do not
	-- do automatic pick up, and if manualDropAutoStackEnabled is false we also cancel automatic stacking.
	-- Note: We don't do this on entities we want to ignore. There is a duplicate check in the DoTaskInTime
	-- below. This is because .prefab isn't set until after the postinits, and we also cannot be sure that
	-- all tags have been applied yet. We still do the check here, though, since we can then skip the whole
	-- destacking-thing altogether.
	if not ShouldIgnoreEntity(compInst) then
		local oldOnDestack = comp.inst.components.stackable.ondestack
		comp.inst.components.stackable.ondestack = function(inst)
			if inst.wasjustcreatedfrombeingdestacked == nil then
				inst.wasjustcreatedfrombeingdestacked = true
			end
			if oldOnDestack ~= nil then
				oldOnDestack(inst)
			end
		end
	end
	
	-- I changed this to 0.1 from 0, so the Quaker-script (and other scripts that spawn items,
	-- is allowed to manipulate the item between the stackable-component being added,
	-- and my script doing its ShouldIgnoreEntity() checks and especially the
	-- .inventoryitem.canbepickedup bool-check, since the Quaker-script sets that to false after
	-- spawning an item, and then to true again once it has landed and possibly been destroyed by the fall.
	-- This should also let other mods that spawn items have at least a frame or two before I destroy the item.
	-- My checks should be solid enough to cancel its functionality if the item has become invalid or destroyed.
	compInst:DoTaskInTime(0.1, function(inst)
		if inst == nil then
			return
		end
	
		-- If it is an entity we want to ignore, we return instantly.
		-- The reason why we have the check here AND above, is that .prefab isn't set until after the postinits,
		-- and we also cannot be sure that all tags have been applied yet, so we have to delay the check until here.
		if ShouldIgnoreEntity(inst) then
			-- print("Ignoring entity "..inst.prefab)
			return
		end
		
		if inst.wasjustcreatedfrombeingdestacked then
			-- print(inst.prefab.." was spawned due to manual destacking. Skipping auto pick up"..(manualDropAutoStackEnabled and "." or " and also auto stacking, since manual drop auto stacking is disabled."))
			shouldAutoPickup = false
			if not manualDropAutoStackEnabled then
				shouldStack = false
			end
			inst.wasjustcreatedfrombeingdestacked = false
		end
		
		if shouldStack and not autoStackTwiggyTreeTwigs and inst.prefab == "twigs" then
			local x,y,z = inst.Transform:GetWorldPosition()
			local ents = _G.TheSim:FindEntities(x,y,z, 8, { "tree" }, { "FX", "NOCLICK", "DECOR","INLIMBO" })
			for i,ent in ipairs(ents) do
				if ent.build == "twiggy" then
					shouldStack = false
					break
				end
			end
		end
		
		-- if shouldStack and not autoStackPenguinEggs and inst.prefab == "bird_egg" then
			-- local x,y,z = inst.Transform:GetWorldPosition()
			-- local ents = _G.TheSim:FindEntities(x,y,z, 4, { "penguin" }, { "FX", "NOCLICK", "DECOR","INLIMBO" })
			-- if #(ents) > 0 then
				-- shouldStack = false
			-- end
		-- end
		
		-- if shouldStack and not autoStackPenguinEggs and inst.prefab == "rottenegg" then
			-- local x,y,z = inst.Transform:GetWorldPosition()
			-- local ents = _G.TheSim:FindEntities(x,y,z, 4, { "penguin", "lunar_aligned" }, { "FX", "NOCLICK", "DECOR","INLIMBO" })
			-- if #(ents) > 0 then
				-- shouldStack = false
			-- end
		-- end
		
		if shouldStack and inst:HasTag("no_autostack_w") then
			shouldStack = false
		end
		
		if shouldAutoPickup and inst:HasTag("no_autopickup") then
			shouldAutoPickup = false
		end
		
		-- This makes it so that items dropping from the ceiling during an earthquake are not Auto-Pickup'ed
		-- nor Auto-Stacked until their .canbepickedup is true (or it runs out of tries after 10 seconds).
		-- This fixes a problem with some falling items disappearing during earhquakes, due to being Auto-Pickup'ed
		-- before their falling sequence has completed.
		-- NOTE: NOT SURE WHAT OTHER IMPLICATIONS THIS MAY HAVE!
		-- if inst.components.inventoryitem and not inst.components.inventoryitem.canbepickedup then
			-- shouldStack = false
			-- shouldAutoPickup = false
		-- end
		
		-- print(inst.prefab.." was spawned and"..((not shouldAutoPickup and not shouldStack) and " should neither auto pick up nor auto stack." or ((shouldAutoPickup and " should auto pick up" or " should NOT auto pick up")..(shouldStack and " and should auto stack." or " and should NOT auto stack."))))
		
		if (shouldAutoPickup or shouldStack) and not IsAutoPickupOrStackingBlockedByContext(inst) then
			if inst.parent == nil or (not inst.parent:HasTag("player") and not inst.parent.components.container) then
				if (inst.Physics and not inst.Physics:IsActive())
				or (inst.components.inventoryitem and not inst.components.inventoryitem.canbepickedup) then
					-- print("Physics were not active on "..inst.prefab.." OR inventoryitem.canbepickedup was false; it was "..inventoryitem.canbepickedup..". Starting task...")
					if inst.parent ~= nil then
						-- print(inst.prefab.." has a parent: "..inst.parent.prefab)
					else
						-- print(inst.prefab.." does NOT have a parent")
					end
					inst.AutoPickUpOrAutoStackTaskTries = 0
					inst.AutoPickUpOrAutoStackTask = inst:DoPeriodicTask(0.5, function(inst)
						inst.AutoPickUpOrAutoStackTaskTries = inst.AutoPickUpOrAutoStackTaskTries + 1
						if inst == nil then
							return
						end
						local x, y, z = inst:GetPosition():Get()
						if inst:IsValid() and (not inst.Physics or inst.Physics:IsActive())
						and inst.components.inventoryitem and inst.components.inventoryitem.canbepickedup
						and not (x == 0 and y == 0 and z == 0) then
							-- print("Physics are now active, canbepickedup is true, and the entity has a position that is not 0,0,0. Trying to auto pick up and auto stack "..inst.prefab)
							if inst.AutoPickUpOrAutoStackTask ~= nil then
								inst.AutoPickUpOrAutoStackTask:Cancel()
								inst.AutoPickUpOrAutoStackTask = nil
							end
							DoAutoPickUpOrAutoStack(inst, shouldAutoPickup, shouldStack)
						end
						if inst.AutoPickUpOrAutoStackTask ~= nil and inst.AutoPickUpOrAutoStackTaskTries >= 20 then
							-- print("Cancelling AutoPickUpOrAutoStackTask after trying for too long.")
							inst.AutoPickUpOrAutoStackTask:Cancel()
							inst.AutoPickUpOrAutoStackTask = nil
						end
					end, 0.5)
				else
					DoAutoPickUpOrAutoStack(inst, shouldAutoPickup, shouldStack)
				end
			else
				-- print(inst.prefab.." has a parent, "..inst.parent.prefab..", which is a player or a container. Skipping auto pick up and stacking!")
			end
		end
		
		-- Add the manual drop stacking if it's enabled, and the inst has not already
		-- been fully absorbed into stacks on the ground by the AutoStack() call above.
		if manualDropAutoStackEnabled and inst:IsValid() and not inst:HasTag("no_autostack_m") and not denyManualStack[inst.prefab] then
			-- print("stackable => adding ondropped listener")
			inst:ListenForEvent("ondropped", function(inst)
				-- Wait for all listeners of "ondropped" to do their thing (next frame; I had a crash otherwise)...
				inst:DoTaskInTime(0, function(inst)
					-- ...then auto stack whenever it is dropped.
					-- print(inst.prefab.." was dropped")
					AutoStack(inst, manualDropStackRange, manualStackMakeNewStackMainStack)
				end)
			end)
		end
	end)
end)