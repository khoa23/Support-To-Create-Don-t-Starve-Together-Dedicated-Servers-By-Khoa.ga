name = "Beefalo Widget"
version = "2.5"
local russian = language == "ru"
description = russian and "версия: "..version or "version: "..version
author = "charlie"
api_version = 10
forumthread = ""
icon_atlas = "modicon.xml"
icon = "modicon.tex"
dst_compatible = true
client_only_mod = false
all_clients_require_mod = true
server_filter_tags = {"beefalo widget v" .. version}
configuration_options = 
{
	{
		name = "horizontal",
        label = russian and "позиция по горизонтали" or "horizontal position",
        options =
        {
            {description = "-1200", data = -1200},
            {description = "-1100", data = -1100},
            {description = "-1000", data = -1000},
            {description = "-900", data = -900},
            {description = "-800", data = -800},
            {description = "-700", data = -700},
            {description = "-600", data = -600},
            {description = "-570", data = -570},
            {description = "-540", data = -540},
            {description = "-510", data = -510},
            {description = "-480", data = -480},
            {description = "-450", data = -450},
            {description = "-420", data = -420},
            {description = "-390", data = -390},
            {description = "-360", data = -360},
            {description = "-330", data = -330},
            {description = "-300", data = -300},
            {description = "-270", data = -270},
            {description = "-240", data = -240},
            {description = "-210", data = -210},
            {description = "-180", data = -180},
            {description = "-150", data = -150},
            {description = "-120", data = -120},
            {description = "-90", data = -90},
            {description = "-60", data = -60},
            {description = "-30", data = -30},
            {description = russian and "0 (центр)" or "0 (center)", hover = russian and "по умолчанию" or "by default", data = 0},
            {description = "30", data = 30},
            {description = "60", data = 60},
            {description = "90", data = 90},
            {description = "120", data = 120},
            {description = "150", data = 150},
            {description = "180", data = 180},
            {description = "210", data = 210},
            {description = "240", data = 240},
            {description = "270", data = 270},
            {description = "300", data = 300},
            {description = "330", data = 330},
            {description = "360", data = 360},
            {description = "390", data = 390},
            {description = "420", data = 420},
            {description = "450", data = 450},
            {description = "480", data = 480},
            {description = "510", data = 510},
            {description = "540", data = 540},
            {description = "570", data = 570},
            {description = "600", data = 600},
            {description = "700", data = 700},
            {description = "800", data = 800},
            {description = "900", data = 900},
            {description = "1000", data = 1000},
            {description = "1100", data = 1100},
            {description = "1200", data = 1200},
        },
        default = 0,
	},
	{
		name = "vertical",
        label = russian and "позиция по вертикали" or "vertical position",
        options =
        {
            {description = "auto", data = "auto"},
            {description = "-50", data = -50},
            {description = "-40", data = -40},
            {description = "-30", data = -30},
            {description = "-20", data = -20},
            {description = "-10", data = -10},
            {description = "0", data = 0},
            {description = "10", data = 10},
            {description = "20", data = 20},
            {description = "30", data = 30},
            {description = "40", data = 40},
            {description = "50", data = 50},
            {description = "60", data = 60},
            {description = "70", data = 70},
            {description = "80", data = 80},
            {description = "90", data = 90},
            {description = "100", data = 100},
            {description = "110", data = 110},
            {description = "120", data = 120},
            {description = "130", data = 130},
            {description = "140", data = 140},
            {description = "150", data = 150},
        },
        default = "auto",
	},	
	{
		name = "position",
		hover = russian and "" or "",
        label = russian and "позиция на экране" or "position on screen",
        options =
        {
			{description = russian and "внизу" or "bottom", data = "bottom"},
            {description = russian and "вверху" or "top", data = "top"},
        },
        default = "bottom",
	},	
	{
		name = "info",
		hover = russian and "показывает информацию при наведении мыши на бифа" or "shows information when you hover the mouse",
        label = russian and "показать информацию" or "show info",
        options =
        {
			{description = russian and "нет" or "no", data = false},
            {description = russian and "да" or "yes", data = true},
        },
        default = false,
	},		
}	