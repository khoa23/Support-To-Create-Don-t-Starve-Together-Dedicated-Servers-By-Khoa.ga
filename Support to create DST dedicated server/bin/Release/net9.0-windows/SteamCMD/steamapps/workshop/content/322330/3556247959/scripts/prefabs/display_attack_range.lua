local assets=
{
	Asset("ANIM", "anim/firefighter_placement.zip")    
}


local function fn()
	local inst = CreateEntity()
	
	inst.persists = false

	local trans = inst.entity:AddTransform()
	local anim = inst.entity:AddAnimState()
	trans:SetScale(0.001,0.001,0.001)
	
	anim:SetBank("firefighter_placement")
	anim:SetBuild("firefighter_placement")
	anim:PlayAnimation("idle")
	anim:SetOrientation(ANIM_ORIENTATION.OnGround)
	anim:SetLayer(LAYER_BACKGROUND)
	
    	inst:AddTag("FX")

	if TheSim:GetGameID() == "DST" then
		inst.entity:AddNetwork()
		inst.entity:SetPristine()
		if not TheWorld.ismastersim then
			return inst
        	end
	end

   	 return inst
end

return Prefab( "common/display_attack_range", fn, assets) 