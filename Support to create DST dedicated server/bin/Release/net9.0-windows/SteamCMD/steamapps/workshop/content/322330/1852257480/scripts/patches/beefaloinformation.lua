local health = "hp"
local hunger = "hunger"
local obedience = "obedience"
local domestication = "tamed"

local round2 = function(num, idp)
	return tonumber(string.format("%." .. (idp or 0) .. "f", num))
end

AddClassPostConstruct("widgets/hoverer",function(inst) 
	local old_SetString = inst.text.SetString
	inst.text.SetString = function(self,str) 
		local target = _G.TheInput:GetWorldEntityUnderMouse()		
		if target ~= nil and target.prefab and target.prefab == "beefalo" then 
			local c = _G.ThePlayer and _G.ThePlayer.player_classified
			if c ~= nil then
				SendModRPCToServer(MOD_RPC[modname]["ShowInfoBeefalo"], target.GUID, target)
				local beefalo_id 
				local beefalo_hunger
				local beefalo_obedience
				local beefalo_domestication
				local flag_domestik
				local flag_tendencies
				local beefalo_health
				local beefalo_domestik
				local j = string.find(c.beefalo_info,";",1)
				beefalo_id = tonumber(c.beefalo_info:sub(1,j-1))
				if beefalo_id == target.GUID then 
					local i = j+1
					j = string.find(c.beefalo_info,";",i)
					if j ~= nil then
						flag_tendencies = tonumber(c.beefalo_info:sub(i,j-1))
						i = j+1
						j = string.find(c.beefalo_info,";",i)
						flag_domestik = tonumber(c.beefalo_info:sub(i,j-1))
						i = j+1
						j = string.find(c.beefalo_info,";",i)
						beefalo_hunger = c.beefalo_info:sub(i,j-1)
						i = j+1
						j = string.find(c.beefalo_info,";",i)
						beefalo_obedience = c.beefalo_info:sub(i,j-1)
						i = j+1
						j = string.find(c.beefalo_info,";",i)
						beefalo_domestication = c.beefalo_info:sub(i,j-1)
						i = j+1
						j = string.find(c.beefalo_info,";",i)
						beefalo_health = c.beefalo_info:sub(i,j-1)
						
						local countstr = 0
						if tonumber(beefalo_health) < 100 and tonumber(beefalo_domestication) > 0 then
							str = str.."\n"..health..": "..beefalo_health.."%"
							countstr = countstr + 1
						end 						
						if (flag_domestik == 1 and tonumber(beefalo_hunger) > 0) or (flag_domestik == 0 and tonumber(beefalo_domestication) <= 90 and tonumber(beefalo_hunger) > 0) then
							str = str.."\n"..hunger..": "..beefalo_hunger.."%"
							countstr = countstr + 1
						end 
						if (flag_domestik == 1 and tonumber(beefalo_obedience) > 0) or (flag_tendencies == 1 and flag_domestik == 0) then
							str = str.."\n"..obedience..": "..beefalo_obedience.."%"
							countstr = countstr + 1
						end
						if (flag_domestik == 1 and tonumber(beefalo_domestication) > 0) or (flag_domestik == 0 and tonumber(beefalo_domestication) <= 90) then
							str = str.."\n"..domestication..": "..beefalo_domestication.."%"
							countstr = countstr + 1
						end
						if countstr == 3 then
							str = str.."\n "
						elseif countstr == 4 then
							str = str.."\n \n "
						end
					end
				end
			end
		end
		return old_SetString(self,str)
	end
end)
	
AddModRPCHandler(modname, "ShowInfoBeefalo", function(player, guid, item)
	local pc = player.player_classified
	if pc ~= nil then
		local str = guid..";"
		if item ~= nil and item.components ~= nil and item.components.domesticatable ~= nil and item.components.domesticatable:Validate() then
			local c = item.components
			if c.domesticatable:IsDomesticated() and item.tendency ~= nil then
				if item.tendency == _G.TENDENCY.ORNERY then
					str = str.."1;"
				else
					str = str.."0;"
				end
			else
				str = str.."0;"
			end	
			if not c.domesticatable:IsDomesticated() then
				str = str.."1;"
			else
				str = str.."0;"
			end		
			str = str..tostring(round2(((c.hunger.current/375)*100),0))..";"
			str = str..tostring(round2(c.domesticatable:GetObedience()*100,0))..";"
			str = str..tostring(round2(c.domesticatable:GetDomestication()*100,1))..";"
			str = str..tostring(round2(((c.health.currenthealth/c.health.maxhealth)*100),0))..";"
			str = str.."0;"
		end	
		pc.net_beefalo_info:set(str)
	end
end)

local function RegNetList(inst)
	if _G.ThePlayer ~= nil and _G.ThePlayer.player_classified == inst then
		inst:ListenForEvent("beefalo_info_dirty",function(inst)
			inst.beefalo_info = inst.net_beefalo_info:value()
		end)
	end
end

AddPrefabPostInit("player_classified",function(inst)
	inst.beefalo_info = "0;"
	inst.net_beefalo_info = _G.net_string(inst.GUID, "beefalo.info", "beefalo_info_dirty")
	inst:DoTaskInTime(0, RegNetList)
end)