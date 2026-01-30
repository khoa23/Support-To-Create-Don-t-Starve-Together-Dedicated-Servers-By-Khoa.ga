local Badge = require "widgets/badge"
local UIAnim = require "widgets/uianim"

local BeefaloDomesticationBadge = Class(Badge, function(self, owner)
    Badge._ctor(self, "health", owner)

    self.domesticationanim = self.underNumber:AddChild(UIAnim())
    self.domesticationanim:GetAnimState():SetBank("health")
    self.domesticationanim:GetAnimState():SetBuild("domesticationbeefalowidget")
    self.domesticationanim:GetAnimState():PlayAnimation("anim")
    self.domesticationanim:GetAnimState():SetPercent("anim", 100)
    self.domesticationanim:SetClickable(true)

    self:StartUpdating()
end)

function BeefaloDomesticationBadge:SetPercent(val, max, penaltypercent)
    Badge.SetPercent(self, val, max)
    self.domesticationanim:GetAnimState():SetPercent("anim", 1 - val)
end

function BeefaloDomesticationBadge:OnUpdate(dt)
    self.domesticationanim:GetAnimState():SetPercent("anim", 1-self.percent)
end

return BeefaloDomesticationBadge
