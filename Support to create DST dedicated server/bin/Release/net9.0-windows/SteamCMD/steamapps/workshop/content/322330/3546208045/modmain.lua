GLOBAL.setmetatable(env,{__index=function(t,k) return GLOBAL.rawget(GLOBAL,k) end})
local can_use_elastispacer = GetModConfigData("upgradeable_meatrack")
local no_spoil_meatrack = GetModConfigData("nospoil_meatrack")
local no_spoil_rain = GetModConfigData("nospoil_rain")
local maxstack = GetModConfigData("maxstack")
local containers = require "containers"
local params = containers.params
if maxstack then
	params.meatrack.acceptsstacks =  true
else
	params.meatrack.acceptsstacks = false
end

local function onhammered(inst, worker)
    if inst.components.burnable ~= nil and inst.components.burnable:IsBurning() then
        inst.components.burnable:Extinguish()
    end
    inst.components.lootdropper:DropLoot()
    if inst.components.container ~= nil then
        inst.components.container:DropEverything()
    end
    local fx = SpawnPrefab("collapse_small")
    fx.Transform:SetPosition(inst.Transform:GetWorldPosition())
    fx:SetMaterial("wood")
    inst:Remove()
end

local function regular_ShouldCollapse(inst)
	if inst.components.container and inst.components.container.infinitestacksize then
		--NOTE: should already have called DropEverything(nil, true) (worked or burnt or deconstructed)
		--      so everything remaining counts as an "overstack"
		local overstacks = 0
		for k, v in pairs(inst.components.container.slots) do
			local stackable = v.components.stackable
			if stackable then
				overstacks = overstacks + math.ceil(stackable:StackSize() / (stackable.originalmaxsize or stackable.maxsize))
				if overstacks >= TUNING.COLLAPSED_CHEST_EXCESS_STACKS_THRESHOLD then
					return true
				end
			end
		end
	end
	return false
end

local function regular_Upgrade_OnHammered(inst, worker)
	if regular_ShouldCollapse(inst) then
		if TheWorld.Map:IsPassableAtPoint(inst.Transform:GetWorldPosition()) then
			inst.components.container:DropEverythingUpToMaxStacks(TUNING.COLLAPSED_CHEST_MAX_EXCESS_STACKS_DROPS)
			if not inst.components.container:IsEmpty() then
				regular_ConvertToCollapsed(inst, true, false)
				return
			end
		else
			--sunk, drops more, but will lose the remainder
			if inst.components.burnable ~= nil and inst.components.burnable:IsBurning() then
				inst.components.burnable:Extinguish()
			end
			inst.components.lootdropper:DropLoot()
			inst.components.container:DropEverythingUpToMaxStacks(TUNING.COLLAPSED_CHEST_EXCESS_STACKS_THRESHOLD)
			local fx = SpawnPrefab("collapse_small")
			fx.Transform:SetPosition(inst.Transform:GetWorldPosition())
			fx:SetMaterial("wood")
			inst:Remove()
			return
		end
	elseif inst.components.container ~= nil then
        --If not burnt, we might still have some overstacks, just not enough to "collapse"
        inst.components.container:DropEverything()
	end

	--fallback to default
	onhammered(inst, worker)
end

AddPrefabPostInit("meatrack", function(inst)
	if can_use_elastispacer and inst.components.upgradeable == nil and maxstack then
        inst:AddComponent("upgradeable")
        inst.components.upgradeable.upgradetype = UPGRADETYPES.CHEST  -- 这里可以自定义类型

        -- 定义升级时的效果
        inst.components.upgradeable.onupgradefn = function(inst, doer, item)
            if inst.components.container ~= nil then
                inst.components.container:EnableInfiniteStackSize(true)
            end
			inst.components.workable:SetOnFinishCallback(regular_Upgrade_OnHammered)
			if inst.components.lootdropper ~= nil then
				inst.components.lootdropper:SetLoot({ "chestupgrade_stacksize" })
			end
        end
        inst.components.upgradeable:SetOnUpgradeFn(inst.components.upgradeable.onupgradefn)
    end
	-- if inst.components.preserver == nil then
	-- 	inst:AddComponent("preserver")
	-- end
	-- inst.components.preserver:SetPerishRateMultiplier(0)
end)

local function GetUpValve(fn,name,maxlevel,max,level,file)	
	if type(fn) ~= "function" then return end
	local maxlevel = maxlevel or 5 		--默认最多追5层
	local level = level or 0			--当前层数 建议默认
	local max = max or 20				--最大变量的upvalue的数量 默认20
	for i=1,max,1 do
		local upname,upvalue = debug.getupvalue(fn,i)
		if upname and upname == name then
			if file and type(file) == "string" then			--限定文件 防止被别人提前hook导致取错
				local fninfo = debug.getinfo(fn)
				if fninfo.source and fninfo.source:match(file) then
					return upvalue
				end
			else
				return upvalue
			end
		end
		if level < maxlevel and upvalue and type(upvalue) == "function" then
			local upupvalue  = GetUpValve(upvalue,name,maxlevel,max,level+1,file) --找不到就递归查找
			if upupvalue then return upupvalue end
		end
	end
end
local function SetUpValve(fn,name,set,maxlevel,max,level,file)
	if type(fn) ~= "function" then return end
	local maxlevel = maxlevel or 5 		--默认最多追5层
	local level = level or 0			--当前层数 建议默认
	local max = max or 20				--最大变量的upvalue的数量 默认20
	for i=1,max,1 do
		local upname,upvalue = debug.getupvalue(fn,i)
		if upname and upname == name then
			if file and type(file) == "string" then			--限定文件 防止被别人提前hook导致取错
				local fninfo = debug.getinfo(fn)
				if fninfo.source and fninfo.source:match(file) then
					return debug.setupvalue(fn,i,set)
				end
			else
				return debug.setupvalue(fn,i,set)
			end
		end
		if level < maxlevel and upvalue and type(upvalue) == "function" then
			local upupvalue  = SetUpValve(upvalue,name,set,maxlevel,max,level+1,file) --找不到就递归查找
			if upupvalue then return upupvalue end
		end
	end
end
local DEFAULT_MEAT_BUILD = "meat_rack_food"
local dofind_OnDoneDrying = true
local function onpickup(inst)
    --These last longer when held
	if inst.components.perishable then
		inst.components.perishable:StartPerishing()
		inst.components.perishable:SetLocalMultiplier(1)
	end
end
AddComponentPostInit("dryingrack", function(self)
	--下雨不影响晾肉架
	if no_spoil_rain then
		self._dryingperishratefn = function(containerinst, item)
			return item and item.components.dryable and 0 or nil
		end
	end

	if dofind_OnDoneDrying then
		dofind_OnDoneDrying = false

		local OnDoneDrying = GetUpValve(self.ResumeDrying,"OnDoneDrying")	
		if OnDoneDrying ~= nil then
			local old_OnDoneDrying = OnDoneDrying
			local function newOnDoneDrying(inst, self, item)
				if self.inst.prefab == "meatrack" then --只改晒肉架
					self.dryinginfo[item] = nil
					local slot = self.container:GetItemSlot(item)
					local product = item.components.dryable and item.components.dryable:GetProduct() or nil
					-- if slot and product then
						product = SpawnPrefab(product)
						if product then
							local build = item.components.dryable:GetDriedBuildFile() or DEFAULT_MEAT_BUILD
							if product.components.inventoryitem then
								product.components.inventoryitem:InheritMoisture(item.components.inventoryitem:GetMoisture(), item.components.inventoryitem:IsWet())
							end
							-- ★ 新增：肉干不腐烂
							if no_spoil_meatrack and product.components.perishable then
								--product.components.perishable:SetLocalMultiplier( TUNING.SEG_TIME * 3/ TUNING.PERISH_SLOW )-- 1/80
								product.components.perishable:StopPerishing()
							end
							local stack = item.components.stackable and item.components.stackable.stacksize or 1
							item:Remove()
							self:_dbg_print("Done drying", product.prefab)
							self.container:GiveItem(product, slot)
							if stack > 1 and product and product.components.stackable then
								product.components.stackable:SetStackSize(stack)
							end
							if product.components.inventoryitem then
								product.components.inventoryitem.SetOnPutInInventoryFn = onpickup
								product:ListenForEvent("onputininventory", onpickup)
							end
							local info = self.dryinginfo[product]
							if info == nil then --just making sure it's not another dryable item
								if build ~= DEFAULT_MEAT_BUILD then
									self.dryinginfo[product] = { build = build }
								end
								if self.showitemfn then
									self.showitemfn(self.inst, slot, product.prefab, build)
								end
							end
							
							return product --returned for LongUpdate
						end
					end
				-- else
				-- 	return old_OnDoneDrying(inst, self, item)
				-- end
			end
			SetUpValve(self.ResumeDrying,"OnDoneDrying",newOnDoneDrying)
		end	
	end
end)