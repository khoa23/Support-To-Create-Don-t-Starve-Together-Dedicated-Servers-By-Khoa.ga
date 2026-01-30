return Class(function(self, inst)

assert(TheWorld.ismastersim, "Wormhole_Counter should not exist on client")

    self.inst = inst
	self.wormhole_count = 1

function self:Set()
	self.wormhole_count = self.wormhole_count + 1
end

function self:Get()
	return self.wormhole_count
end

function self:OnSave()
	local data = {}
	data.wormhole_count = self.wormhole_count
	return data
end

function self:OnLoad(data)
	if data then
		self.wormhole_count = data.wormhole_count
	else
		self.wormhole_count = 1
	end
end

end)