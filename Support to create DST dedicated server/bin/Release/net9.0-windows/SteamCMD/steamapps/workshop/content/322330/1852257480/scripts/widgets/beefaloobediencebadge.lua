local Badge = require "widgets/badge"
local UIAnim = require "widgets/uianim"

local BeefaloObedienceBadge = Class(Badge, function(self, owner)
    Badge._ctor(self, "health", owner)

    self.Obedienceanim = self.underNumber:AddChild(UIAnim())
    self.Obedienceanim:GetAnimState():SetBank("health")
    self.Obedienceanim:GetAnimState():SetBuild("obediencebeefalowidget")
    self.Obedienceanim:GetAnimState():PlayAnimation("anim")
    self.Obedienceanim:GetAnimState():SetPercent("anim", 100)
    self.Obedienceanim:SetClickable(true)

    self:StartUpdating()
end)

function BeefaloObedienceBadge:SetPercent(val, max, penaltypercent)
    Badge.SetPercent(self, val, max)
    self.Obedienceanim:GetAnimState():SetPercent("anim", 1 - val)
end

function BeefaloObedienceBadge:OnUpdate(dt)
    self.Obedienceanim:GetAnimState():SetPercent("anim", 1-self.percent)
end

return BeefaloObedienceBadge
