Assets = 
{
	Asset("ATLAS", "images/mark_1.xml"),
	Asset("ATLAS", "images/mark_2.xml"),
	Asset("ATLAS", "images/mark_3.xml"),
	Asset("ATLAS", "images/mark_4.xml"),
	Asset("ATLAS", "images/mark_5.xml"),
	Asset("ATLAS", "images/mark_6.xml"),
	Asset("ATLAS", "images/mark_7.xml"),
	Asset("ATLAS", "images/mark_8.xml"),
	Asset("ATLAS", "images/mark_9.xml"),
	Asset("ATLAS", "images/mark_10.xml"),
	Asset("ATLAS", "images/mark_11.xml"),
	Asset("ATLAS", "images/mark_12.xml"),
	Asset("ATLAS", "images/mark_13.xml"),
	Asset("ATLAS", "images/mark_14.xml"),
	Asset("ATLAS", "images/mark_15.xml"),
	Asset("ATLAS", "images/mark_16.xml"),
	Asset("ATLAS", "images/mark_17.xml"),
	Asset("ATLAS", "images/mark_18.xml"),
	Asset("ATLAS", "images/mark_19.xml"),
	Asset("ATLAS", "images/mark_20.xml"),
	Asset("ATLAS", "images/mark_21.xml"),
	Asset("ATLAS", "images/mark_22.xml"),
}

AddMinimapAtlas("images/mark_1.xml")
AddMinimapAtlas("images/mark_2.xml")
AddMinimapAtlas("images/mark_3.xml")
AddMinimapAtlas("images/mark_4.xml")
AddMinimapAtlas("images/mark_5.xml")
AddMinimapAtlas("images/mark_6.xml")
AddMinimapAtlas("images/mark_7.xml")
AddMinimapAtlas("images/mark_8.xml")
AddMinimapAtlas("images/mark_9.xml")
AddMinimapAtlas("images/mark_10.xml")
AddMinimapAtlas("images/mark_11.xml")
AddMinimapAtlas("images/mark_12.xml")
AddMinimapAtlas("images/mark_13.xml")
AddMinimapAtlas("images/mark_14.xml")
AddMinimapAtlas("images/mark_15.xml")
AddMinimapAtlas("images/mark_16.xml")
AddMinimapAtlas("images/mark_17.xml")
AddMinimapAtlas("images/mark_18.xml")
AddMinimapAtlas("images/mark_19.xml")
AddMinimapAtlas("images/mark_20.xml")
AddMinimapAtlas("images/mark_21.xml")
AddMinimapAtlas("images/mark_22.xml")

local function Mark(inst)
	if not inst.components.wormhole_marks:CheckMark() then
		inst.components.wormhole_marks:MarkEntrance()
	end
	
	local other = inst.components.teleporter.targetTeleporter	
	if  not other.components.wormhole_marks:CheckMark() then
			other.components.wormhole_marks:MarkExit()
	end
end

function WormholePrefabPostInit(inst)
	if not inst.components.wormhole_marks then
		inst:AddComponent("wormhole_marks")
	end
	inst:ListenForEvent("starttravelsound", Mark)
end

AddPrefabPostInit("wormhole", WormholePrefabPostInit)

function WorldPrefabPostInit(inst)
	if inst:HasTag("forest") then
		inst:AddComponent("wormhole_counter")
	elseif inst:HasTag("cave") then
		inst:AddComponent("wormhole_counter")
	end
end

if GLOBAL.TheNet:GetIsServer() or GLOBAL.TheNet:IsDedicated() then
	AddPrefabPostInit("world", WorldPrefabPostInit)
end