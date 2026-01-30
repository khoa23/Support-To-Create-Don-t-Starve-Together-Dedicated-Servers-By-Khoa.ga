--[[
Author: Miooowo
Date: 2025-08-10 13:16:40
LastEditors: Miooowo
LastEditTime: 2025-08-10 13:17:56
Description: Although I rarely use github, I still registered it: https://github.com/Miooowo

Copyright (c) 2025 by Miooowo, All Rights Reserved. 
--]]
local L = locale == "zh" or locale == "zhr" -- 是否为中文
name = L and "更强的晾肉架" or "Stronger drying rack"
description = L and [[
晾肉架单格可晾晒一组食物，可使用弹性空间制造器突破堆叠上限。
成品如肉干、干海带叶等不会在晾肉架内腐烂。
]] or [[
ach grid of the Drying Rack can dry a stack of foods. Use the Elastispacer to increase the stacking limit.
Product items like Jerky and Dried Kelp Fronds will not spoil in the Drying Rack.
]]
author = "ClockCycas"
version = "1.3"
api_version = 10

dont_starve_compatible = false
reign_of_giants_compatible = false
dst_compatible = true
restart_required = false
all_clients_require_mod = true

icon = "modicon.tex"
icon_atlas = "modicon.xml"
server_filter_tags = {""}
forumthread = ""
configuration_options = {
  {
    name = "maxstack",
    label = L and "单格堆叠" or "Max Stack",
    hover = L and "设置为1个或一组" or "Set to solo one or a one stack",
    options =	{
            {description = L and "1个" or "solo one", data = false,hover = L and "警告：启用此选项后弹性空间制造器升级将失效！" or "Warning: Elastispacer Upgrade will fail when enabled!"},
            {description = L and "一组" or "one stack", data = true},
          },
    default = true,
  },
  {
      name = "upgradeable_meatrack",
      label = L and "弹性空间制造器升级" or "Elastispacer Upgrade",
      hover = L and "使用弹性空间制造器突破堆叠上限" or "Use the Elastispacer to increase the stacking limit.",
      options =	{
          {description = L and "是" or "yes", data = true},
          {description = L and "否" or "no", data = false},
        },
  default = true,
  },
  {
      name = "nospoil_meatrack",
      label = L and "晾晒产物不腐烂" or "Products do not spoil",
      hover = L and "成品如肉干、干海带叶等不会在晾肉架内腐烂。" or "Product items like Jerky and Dried Kelp Fronds will not spoil in the Drying Rack.",
      options =	{
          {description = L and "是" or "yes", data = true},
          {description = L and "否" or "no", data = false},
        },
  default = true,
  },
  {
      name = "nospoil_rain",
      label = L and "下雨不导致食物腐烂" or "Rain does not cause food to spoil",
      hover = L and "下雨不影响晾肉架" or "Rain does not affect the drying rack.",
      options =	{
          {description = L and "是" or "yes", data = true},
          {description = L and "否" or "no", data = false},
        },
  default = true,
  },
}



