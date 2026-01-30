_G = GLOBAL
require = _G.require
tostring = _G.tostring
tonumber = _G.tonumber
ismastersim = _G.TheNet:GetIsServer()
TUNING = _G.TUNING

Assets =
{
	Asset("ANIM", "anim/domesticationbeefalowidget.zip"),
	Asset("ANIM", "anim/obediencebeefalowidget.zip")
}
local info = GetModConfigData("info")

TUNING.BEEFALOWIDGET = {}
TUNING.BEEFALOWIDGET.POS = GetModConfigData("position") or "bottom"
TUNING.BEEFALOWIDGET.HOR = GetModConfigData("horizontal") or 0
TUNING.BEEFALOWIDGET.VER = GetModConfigData("vertical") or "auto"
TUNING.BEEFALOWIDGET.SLOTS45 = false

local conflict_mods = "disabled"

for k, v in ipairs(_G.ModManager:GetEnabledServerModNames()) do
	if v == "workshop-666155465" then
		conflict_mods = "enabled"
	end
	if v == "workshop-786556008" or v == "workshop-2166704267" or v == "workshop-2801880191" or v == "workshop-2568821043" or v == "workshop-2886543901" then
		TUNING.BEEFALOWIDGET.SLOTS45 = true
	end			
end
if info and conflict_mods == "disabled" then
	modimport("scripts/patches/beefaloinformation.lua")
end
modimport("scripts/patches/beefalodomestication.lua")