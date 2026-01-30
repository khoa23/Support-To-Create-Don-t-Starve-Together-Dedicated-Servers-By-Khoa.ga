name = "Wormhole Marks [DST Continued]"
description = "Changes wormholes icons on mini map to colored icons."
author = "KittyKit"
version = "1.0.0a"
forumthread = ""


api_version = 10

configuration_options =
{	
	{
        name = "Draw over FoW",
        options =
        {
            {description = "Disabled", data = "disabled"},
			{description = "Enabled", data = "enabled"},
        },
        default = "disabled",
    },	
}

--This lets the clients know that they need to download the mod before they can join a server that is using it.
all_clients_require_mod = true

--This let's the game know that this mod doesn't need to be listed in the server's mod listing
client_only_mod = false

--Let the mod system know that this mod is functional with Don't Starve Together
dst_compatible = true

--These tags allow the server running this mod to be found with filters from the server listing screen
server_filter_tags = {"wormhole marks continued"}

icon_atlas = "modicon.xml"
icon = "wormhole_marks.tex"

