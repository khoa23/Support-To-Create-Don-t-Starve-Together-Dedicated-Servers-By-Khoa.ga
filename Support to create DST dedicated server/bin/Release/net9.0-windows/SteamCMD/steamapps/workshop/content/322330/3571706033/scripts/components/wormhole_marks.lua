local modname = KnownModIndex:GetModActualName("Wormhole Marks [DST Continued]")
local fow_setting = GetModConfigData("Draw over FoW", modname)

local Wormhole_Marks = Class(function(self, inst)
    self.inst = inst
	self.marked = false
	self.wormhole_number = nil
end)

function Wormhole_Marks:MarkEntrance()
	self:GetNumber()
	if self.wormhole_number <= 22 then 
		self.marked = true
		if fow_setting == "enabled" then
			self.inst.MiniMapEntity:SetDrawOverFogOfWar(true)
		end
		self.inst.MiniMapEntity:SetIcon("mark_"..self.wormhole_number..".tex")
	end
end

function Wormhole_Marks:MarkExit()
	self:GetNumber()
	if self.wormhole_number <= 22 then 
		self.marked = true
		if fow_setting == "enabled" then
			self.inst.MiniMapEntity:SetDrawOverFogOfWar(true)
		end
		self.inst.MiniMapEntity:SetIcon("mark_"..self.wormhole_number..".tex")
		TheWorld.components.wormhole_counter:Set()
	end
end

function Wormhole_Marks:GetNumber()
	self.wormhole_number = TheWorld.components.wormhole_counter:Get()
end

function Wormhole_Marks:CheckMark()
	return self.marked
end

function Wormhole_Marks:OnSave()
	local data = {}
	data.marked = self.marked
	data.wormhole_number = self.wormhole_number
	return data
end

function Wormhole_Marks:OnLoad(data)
	if data then
		self.marked = data.marked
		self.wormhole_number = data.wormhole_number
		if self.marked and self.wormhole_number then
		self.inst.entity:AddMiniMapEntity()
		self.inst.MiniMapEntity:SetIcon("mark_"..self.wormhole_number..".tex")
			if fow_setting == "enabled" then
				self.inst.MiniMapEntity:SetDrawOverFogOfWar(true)
			end
		end
	else
		self.marked = false
		self.wormhole_number = 0
	end
end

return Wormhole_Marks