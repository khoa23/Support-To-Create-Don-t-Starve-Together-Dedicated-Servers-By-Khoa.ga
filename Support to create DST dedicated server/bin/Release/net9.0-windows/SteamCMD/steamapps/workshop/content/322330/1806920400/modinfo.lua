name = "Feather Farm"
version = "1.7"
local russian = language == "ru"
description = russian and "версия: "..version or "version: "..version
author = "charlie"
api_version = 10
forumthread = ""
icon_atlas = "modicon.xml"
icon = "modicon.tex"
dst_compatible = true
client_only_mod = false
all_clients_require_mod = false
server_filter_tags = {"feather farm v" .. version}
configuration_options =
{
	{
		name = "periodicity",
        label = russian and "частота выпадения" or "periodicity of drop",
        options =
        {
            {description = russian and "часто" or "often", data = 1},
            {description = russian and "нормально" or "normally", data = 2},
            {description = russian and "редко" or "arely", data = 3},
            {description = russian and "очень редко" or "very rarely", data = 4},
        },
        default = 2,
	},
	{
		name = "amount",
        label = russian and "количество перьев" or "amount of feathers",
        options =
        {
            {description = "x1", data = 1},
            {description = "x2", data = 2},
            {description = "x3", data = 3},
            {description = "x4", data = 4},
        },
        default = 1,
	},
}	