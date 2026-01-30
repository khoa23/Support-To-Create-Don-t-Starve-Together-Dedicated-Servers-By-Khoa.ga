--[[	Ultroman the Tacoman 2025	 ]]
name = "Auto Stack and Pick Up"
description = "This mod gives you MANY settings for automatically stacking or picking up newly spawned and dropped stackable items. By 'automatically stacking' I mean that if an item is spawned (e.g., loot from monsters or workables, like trees) or it is manually dropped on the ground by a player, it looks for other stacks on the ground to merge with.\nAll features can be enabled individually.\nYou can also individually enable that the nearest player within range will automatically pick up ANY stackable item spawned near them. You can also set whether a player MUST have one of the item in their inventory already for the auto pickup to happen. Useful for avoiding cluttering your inventory with useless things!"
author = "Ultroman"
version = "0.3.0"

forumthread = ""

api_version = 10

dont_starve_compatible = false
reign_of_giants_compatible = false
shipwrecked_compatible = false
hamlet_compatible = false
dst_compatible = true

all_clients_require_mod = false
client_only_mod = false

server_filter_tags = {"auto", "stack"}

priority = 0

icon_atlas = "preview.xml"
icon = "preview.tex"

configuration_options =
{
	-- This is an "empty" setting, which functions as a headline for the coming settings.
	{
		name 	= "",
		label 	= "General Settings",
		options =	{
						{description = "", data = 0},
					},
		default = 0,
	},
	-- A boolean setting. This one for setting whether a smoke puff should be played when auto-stacking.
	{
		name = "SmokePuffOnStacking",
		label = "Puff On Stack",
		hover = "Visualize auto stacking and pick up with a smoke puff where the stacked item was.",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = true,
	},
	-- This is an "empty" setting, which functions as a headline for the coming settings.
	{
		name 	= "",
		label 	= "World Spawn Settings",
		hover 	= "Settings for auto stacking items spawned by anything in the world.",
		options =	{
						{description = "", data = 0},
					},
		default = 0,
	},
	-- A boolean setting.
	{
		name = "AutoStackEnabled",
		label = "World Drop Auto Stack",
		hover = "Enable auto stacking items spawned by anything in the world, except players?",
		options =	{
						{description = "On", data = true},
						{description = "Off", data = false},
					},
		default = true,
	},
	{
		name = "StackDuringPopulation",
		label = "World Gen Auto Stack",
		hover = "Also auto stack items spawned during world generation / population? Will auto stack all items that are close!!!",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	-- An integer setting. This one for setting the range within which spawned items look for a stack to join.
	{
		name = "AutoStackRange",
		label = "World Auto Stack Range",
		hover = "The range within which a spawned item looks for a stack to join.",
		options =	{
						{description = "1", data = 1},
						{description = "2", data = 2},
						{description = "3", data = 3},
						{description = "4", data = 4},
						{description = "5", data = 5},
						{description = "6", data = 6},
						{description = "7", data = 7},
						{description = "8", data = 8},
						{description = "9", data = 9},
						{description = "10", data = 10},
						{description = "11", data = 11},
						{description = "12", data = 12},
						{description = "13", data = 13},
						{description = "14", data = 14},
						{description = "15", data = 15},
						{description = "16", data = 16},
						{description = "17", data = 17},
						{description = "18", data = 18},
						{description = "19", data = 19},
						{description = "20", data = 20},
						{description = "21", data = 21},
						{description = "22", data = 22},
						{description = "23", data = 23},
						{description = "24", data = 24},
						{description = "25", data = 25},
					},
		default = 10,
	},
	{
		name = "AutoStackMakeNewStackMainStack",
		label = "World Auto Stack In",
		hover = "'Existing' means that new drops are added to surrounding stacks.\n'Newest' means surrounding stacks are added to the newest stack.",
		options =	{
						{description = "Newest", data = true},
						{description = "Existing", data = false},
					},
		default = true,
	},
	{
		name = "AutoStackTwiggyTreeTwigs",
		label = "Auto Stack Twiggy Tree Twigs",
		hover = "Twiggy Trees normally stop spawning new Twigs when there are 2 Twigs close to them. 'No' stops their Twigs from stacking, to keep that behavior.",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	{
		name = "AutoStackAsh",
		label = "Auto Stack Ash",
		hover = "Also auto stack newly spawned ash?",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	{
		name = "AutoStackPoop",
		label = "Auto Stack Poop",
		hover = "Also auto stack newly spawned poop?",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	{
		name = "AutoStackSeeds",
		label = "Auto Stack Seeds",
		hover = "Also auto stack newly spawned seeds?",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	{
		name 	= "",
		label 	= "Manual Drop Settings",
		hover 	= "Settings for auto stacking items manually dropped by players.",
		options =	{
						{description = "", data = 0},
					},
		default = 0,
	},
	{
		name = "AutoStackManuallyDroppedItems",
		label = "Manual Drop Auto Stack",
		hover = "Auto stack items dropped manually by players?",
		options =	{
						{description = "On", data = true},
						{description = "Off", data = false},
					},
		default = false,
	},
	{
		name = "ManualDropStackRange",
		label = "Manual Auto Stack Range",
		hover = "The range within which a manually dropped item looks for a stack to join.",
		options =	{
						{description = "1", data = 1},
						{description = "2", data = 2},
						{description = "3", data = 3},
						{description = "4", data = 4},
						{description = "5", data = 5},
						{description = "6", data = 6},
						{description = "7", data = 7},
						{description = "8", data = 8},
						{description = "9", data = 9},
						{description = "10", data = 10},
						{description = "11", data = 11},
						{description = "12", data = 12},
						{description = "13", data = 13},
						{description = "14", data = 14},
						{description = "15", data = 15},
						{description = "16", data = 16},
						{description = "17", data = 17},
						{description = "18", data = 18},
						{description = "19", data = 19},
						{description = "20", data = 20},
						{description = "21", data = 21},
						{description = "22", data = 22},
						{description = "23", data = 23},
						{description = "24", data = 24},
						{description = "25", data = 25},
					},
		default = 5,
	},
	{
		name = "ManualStackMakeNewStackMainStack",
		label = "Manual Auto Stack In",
		hover = "'Existing' means that manually dropped items are added to surrounding stacks.\n'Newest' means surrounding stacks are added to the manually dropped stack.",
		options =	{
						{description = "Newest", data = true},
						{description = "Existing", data = false},
					},
		default = false,
	},
	{
		name = "ManualStackAsh",
		label = "Manual Auto Stack Ash",
		hover = "Also auto stack manually dropped ash?",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	{
		name = "ManualStackPoop",
		label = "Manual Auto Stack Poop",
		hover = "Also auto stack manually dropped poop?",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	{
		name = "ManualStackSeeds",
		label = "Manual Auto Stack Seeds",
		hover = "Also auto stack manually dropped seeds?",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	{
		name 	= "",
		label 	= "Auto Pickup Settings",
		hover 	= "Settings for automatically picking up stackable items spawned by anything in the world.",
		options =	{
						{description = "", data = 0},
					},
		default = 0,
	},
	{
		name = "AutoPickupEnabled",
		label = "Auto Pickup Items",
		hover = "Automatically give stackable items spawned by anything in the world to the closest player? Auto Pickup has priority over World Spawn Auto Stacking.",
		options =	{
						{description = "On", data = true},
						{description = "Off", data = false},
					},
		default = false,
	},
	{
		name = "AutoPickupRange",
		label = "Auto Pickup Range",
		hover = "The range within which a spawned stackable item looks for a player to be automatically picked up by.",
		options =	{
						{description = "1", data = 1},
						{description = "2", data = 2},
						{description = "3", data = 3},
						{description = "4", data = 4},
						{description = "5", data = 5},
						{description = "6", data = 6},
						{description = "7", data = 7},
						{description = "8", data = 8},
						{description = "9", data = 9},
						{description = "10", data = 10},
						{description = "11", data = 11},
						{description = "12", data = 12},
						{description = "13", data = 13},
						{description = "14", data = 14},
						{description = "15", data = 15},
						{description = "16", data = 16},
						{description = "17", data = 17},
						{description = "18", data = 18},
						{description = "19", data = 19},
						{description = "20", data = 20},
						{description = "21", data = 21},
						{description = "22", data = 22},
						{description = "23", data = 23},
						{description = "24", data = 24},
						{description = "25", data = 25},
					},
		default = 10,
	},
	{
		name = "PlayerMustHaveOneOfItemToAutoPickup",
		label = "Require Existing Stack?",
		hover = "To auto pick up an item, the player must already have one of that item in their inventory or backpack.",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	{
		name = "AutoPickupAsh",
		label = "Auto Pickup Ash",
		hover = "Also auto pick up newly spawned ash?",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	{
		name = "AutoPickupPoop",
		label = "Auto Pickup Poop",
		hover = "Also auto pick up newly spawned poop?",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
	{
		name = "AutoPickupSeeds",
		label = "Auto Pickup Seeds",
		hover = "Also auto pick up newly spawned seeds?",
		options =	{
						{description = "Yes", data = true},
						{description = "No", data = false},
					},
		default = false,
	},
}