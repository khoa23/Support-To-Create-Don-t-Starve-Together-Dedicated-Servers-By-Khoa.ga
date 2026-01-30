name = "Display Attack Range [fixed]"

description = [[Shows the attack range of mobs when they are targeting a player. The display will change color when they attack. Configuration options:

Red, Green, Blue: Adjust the RGB color that shown for attack.

Range appearance: Select range appearing while targeting player or during attack phase.
 
Type of Range: Select range type to be attack range, hit range, or switch between both.

Display Projectile Range: Select to display the range of projectile.]]

author = "ClumsyNoob; modified by a penguin"

version = "1.0"

api_version = 10
--dst_api_version = 10

forumthread = ""
all_clients_require_mod = true
client_only_mod = false
dst_compatible = true
--dont_starve_compatible = true

icon_atlas = "modicon.xml"
icon = "modicon.tex"


configuration_options =
{
    {
        name = "Red",
        label = "Red",
        options =   {
			{description = "0.0", data = 0.0},
			{description = "0.1", data = 0.1},
			{description = "0.2", data = 0.2},
                        {description = "0.3", data = 0.3},
                        {description = "0.4", data = 0.4},
			{description = "0.5", data = 0.5},
			{description = "0.6", data = 0.6},
			{description = "0.7", data = 0.7},
			{description = "0.8", data = 0.8},
			{description = "0.9", data = 0.9},
			{description = "1.0", data = 1.0},
                    },
        default = 1.0,
	hover = "Adjust the RGB color that shown for attack",
    },

    {
        name = "Green",
        label = "Green",
        options =   {
			{description = "0.0", data = 0.0},
			{description = "0.1", data = 0.1},
			{description = "0.2", data = 0.2},
                        {description = "0.3", data = 0.3},
                        {description = "0.4", data = 0.4},
			{description = "0.5", data = 0.5},
			{description = "0.6", data = 0.6},
			{description = "0.7", data = 0.7},
			{description = "0.8", data = 0.8},
			{description = "0.9", data = 0.9},
			{description = "1.0", data = 1.0},
                    },
        default = 0.0,
	hover = "Adjust the RGB color that shown for attack",
    },

    {
        name = "Blue",
        label = "Blue",
        options =   {
			{description = "0.0", data = 0.0},
			{description = "0.1", data = 0.1},
			{description = "0.2", data = 0.2},
                        {description = "0.3", data = 0.3},
                        {description = "0.4", data = 0.4},
			{description = "0.5", data = 0.5},
			{description = "0.6", data = 0.6},
			{description = "0.7", data = 0.7},
			{description = "0.8", data = 0.8},
			{description = "0.9", data = 0.9},
			{description = "1.0", data = 1.0},
                    },
        default = 0.0,
	hover = "Adjust the RGB color that shown for attack",
    },

    {
        name = "Display",
        label = "Range appearance",
        options =   {
			{description = "While Target", data = "target"},
			{description = "During Attack", data = "attack"},
                    },
        default = "target",
	hover = [[While Target: range appears while targeting player
During Attack: range appears during attack phase]],
    },

    {
        name = "Type",
        label = "Type of Range",
        options =   {
			{description = "Hit Range", data = "hit"},
			{description = "Attack or Hit", data = "both"},
			{description = "Attack Range", data = "attack"},
                    },
        default = "hit",
	hover = [[Attack Range: range where mobs start their attack
Hit Range: range where players will get attack]],
    },

	{
        name = "Projectile",
        label = "Display Projectile Range",
        options =   {
			{description = "On", data = true},
			{description = "Off", data = false},
                    },
        default = true,
	hover = "Show the range of projectile",
    },
}
