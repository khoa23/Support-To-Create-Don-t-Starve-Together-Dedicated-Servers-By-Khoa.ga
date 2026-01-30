require "class"
local Widget = require "widgets/widget"
local ItemTile = require "widgets/itemtile"
local ItemSlot = require "widgets/itemslot"
local Text = require "widgets/text"
local BeefaloHealthBadge = require "widgets/healthbadge"
local BeefaloDomesticationBadge = require "widgets/beefalodomesticationbadge"
local BeefaloObedienceBadge = require "widgets/beefaloobediencebadge"

local hshow, hhide, vshow, vhide

if TUNING.BEEFALOWIDGET.POS == "bottom" then
	hshow, hhide, vshow, vhide = TUNING.BEEFALOWIDGET.HOR, TUNING.BEEFALOWIDGET.HOR, 10, -300
elseif TUNING.BEEFALOWIDGET.POS == "top" then
	hshow, hhide, vshow, vhide = TUNING.BEEFALOWIDGET.HOR, TUNING.BEEFALOWIDGET.HOR, -118, 0
end

local BeefaloWidget = Class(Widget, function(self, owner)
    Widget._ctor(self, "Beefalo")
    self.inv = {}
    self.owner = owner
	self.hidden_position = Vector3(hhide, vhide, 0)
	self:SetPosition(self.hidden_position)
    self.slotsperrow = 4
    
	self.bg = self:AddChild(Image("images/hud.xml", "craftingsubmenu_fullvertical.tex"))
	self.bg:SetScale(1.5,1,1)
	if TUNING.BEEFALOWIDGET.POS == "bottom" then
		self.bg:SetRotation(180)
	end	
	if TUNING.BEEFALOWIDGET.POS == "top" then
		self.bg:SetPosition(0, 275)
	end		
	
    self.buck = self:AddChild(Text(NUMBERFONT, 40))
	self.buck:SetHAlign(ANCHOR_MIDDLE)
	self.buck:SetScale(1,.78,1)
	self.buck:SetPosition(-141, 100)
	local function OnBuckDelayDirty(classified)
		local current_time = classified.mountbuckdelay:value()
		if current_time >= 0 then
			local seconds = math.floor(current_time % 60)
			local displayTime = math.floor(current_time / 60) .. ":" .. (seconds < 10 and "0" .. seconds or seconds)
			self.buck:SetString(displayTime) 
		else
			self.buck:SetString("0:00") 
		end	
	end
	self.owner.player_classified:ListenForEvent("buckdelaydeltadirty", OnBuckDelayDirty)
	OnBuckDelayDirty(self.owner.player_classified)
	
	self.health = self:AddChild(BeefaloHealthBadge(owner))
	self.health:SetScale(1.3, 1.3, 1)
	self.health:SetPosition(-53, 153)
	self.health:StopUpdating()
	local function OnHealthDirty(classified)
		self.health:SetPercent(classified.mounthealth:value()/classified.mountmaxhealth:value(),
			classified.mountmaxhealth:value())
	end
	self.owner.player_classified:ListenForEvent("mounthealthdirty", OnHealthDirty)
	self.owner.player_classified:ListenForEvent("mountmaxhealthdirty", OnHealthDirty)
	OnHealthDirty(self.owner.player_classified)

	self.obedience = self:AddChild(BeefaloObedienceBadge(owner))
	self.obedience:SetScale(1.3,1.3,1)
	self.obedience:SetPosition(42,153)
	self.obedience:StopUpdating()
	local function OnObedienceDirty(classified)
		self.obedience:SetPercent(classified.mountobedience:value()/100,100)
	end
	self.owner.player_classified:ListenForEvent("mountobediencedirty", OnObedienceDirty)
	OnObedienceDirty(self.owner.player_classified)
	
	self.domestication = self:AddChild(BeefaloDomesticationBadge(owner))
	self.domestication:SetScale(1.3,1.3,1)
	self.domestication:SetPosition(137,153)
	self.domestication:StopUpdating()
	local function OnDomesticationDirty(classified)
		self.domestication:SetPercent(classified.mountdomestication:value()/100,100)
	end
	self.owner.player_classified:ListenForEvent("mountdomesticationdirty", OnDomesticationDirty)
	OnDomesticationDirty(self.owner.player_classified)
	
	self.saddleslot = self:AddChild(ItemSlot("images/hud.xml", "inv_slot.tex", self.owner))
	self.saddleslot:SetPosition(-142, 154)
	
	self.isopen = false
end)

function BeefaloWidget:GetShownPosition()
    local ypos = vshow
    if TUNING.BEEFALOWIDGET.VER ~= "auto" then
		ypos = ypos + TUNING.BEEFALOWIDGET.VER
	elseif TUNING.BEEFALOWIDGET.SLOTS45 and TUNING.BEEFALOWIDGET.POS == "bottom" then
		ypos = ypos + 45
	elseif (Profile:GetIntegratedBackpack() or TheInput:ControllerAttached()) and TUNING.BEEFALOWIDGET.POS == "bottom" then
        local backpack = self.owner.replica.inventory:GetOverflowContainer()
        if backpack and backpack:IsOpenedBy(self.owner) then 
			ypos = ypos + 45 
		end 
	end
	return Vector3(hshow, ypos, 0)
end

function BeefaloWidget:GetHiddenPosition()
	return self.hidden_position
end

function BeefaloWidget:Open()
    self:Close()
    self.isopen = true
	self.saddleslot:SetTile(ItemTile(self.owner.player_classified.ridersaddle:value()))
	self.saddleslot.tile.GetDescriptionString = function(self)
		local str = ""
		if self.item ~= nil and self.item:IsValid() and self.item.replica.inventoryitem ~= nil then
			local adjective = self.item:GetAdjective()
			if adjective ~= nil then
				str = adjective.." "
			end
			str = str..self.item:GetDisplayName()
		end
		return str
	end
    self:Show()
	self:CancelMoveTo()
	self:MoveTo(self:GetPosition(), self:GetShownPosition(), 0.5)
end

function BeefaloWidget:Close()
	if self.isopen then
		self.isopen = false
		self:CancelMoveTo()
		self:MoveTo(self:GetPosition(), self:GetHiddenPosition(), 0.5, function() self:Hide() end)
	end
end

function BeefaloWidget:UpdatePosition()
	local correct_position = self.isopen and self:GetShownPosition() or self:GetHiddenPosition()
	if (self.inst.components.uianim.pos_dest or self:GetPosition()) ~= correct_position then
		self:CancelMoveTo()
		self:MoveTo(self:GetPosition(), self.isopen and self:GetShownPosition() or self:GetHiddenPosition(), 0.2, function() if not self.isopen then self:Hide() end end)
	end
end

return BeefaloWidget