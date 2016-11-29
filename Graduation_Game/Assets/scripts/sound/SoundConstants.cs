namespace Assets.scripts.sound {
	public class SoundConstants {

		/// <summary>
		/// Sounds for in-app purchases and currency/penguin interactions (outside of the main game)
		/// </summary>
		public class StoreSounds {
			public const string EGG_HATCH = "egg_hatching";
		}

		/// <summary>
		/// Sounds that are triggered automatically when penguin picks up a specific item
		/// </summary>
		public class PickUpSounds {
			public const string CURRENCY_FLY = "pickup_fly_to_score";
			public const string PICKUP_CURRENCY = "pickup_currency";
			public const string PICK_UP_ADD = "pickup_add_to_score";
		}

		/// <summary>
		/// Sounds that penguins make
		/// </summary>
		public class PenguinSounds {
			public const string START_MOVING = "penguin_move_start";
			public const string STOP_MOVING = "penguin_move_stop";
			public const string SPAWN = "penguin_spawn";
		}

		public class SealSounds {
			public const string START_MOVING = "seal_move_start";
			public const string STOP_MOVING = "seal_move_stop";
			public const string SPAWN = "seal_spawn";
		}

		/// <summary>
		/// Reacts to user touch/click action
		/// </summary>
		public class FeedbackSounds {
			public const string JUMP_PLACED = "tool_jump_triggered";
			public const string CHANGE_LANE_PLACED = "penguin_tool_change_lane_used";
			public const string BUTTON_PRESS = "button_pressed";
			public const string END_SCREEN_TRIGGER = "end_screen_spawn";
			public const string END_SCREEN_SPAWN_STAR = "end_screen_star_spawn";
		}

		/// <summary>
		/// Sounds that are triggered by tools
		/// </summary>
		public class ToolSounds {
			public const string JUMP_TRIGGERED = "tool_jump_triggered";
			public const string CHANGE_LANE_TRIGGERED = "penguin_tool_change_lane_used";
			public const string TOOL_RETURNED = "tool_returned_to_toolbar";
			public const string TOOL_PICK_UP = "tool_picked_up";
		}

		/// <summary>
		/// In game/main menu music, sound settings, etc...
		/// </summary>
		public class Master {
			public const string MUSIC_MUTE = "mute_music";
			public const string MUSIC_UNMUTE = "music_start";
			public const string MASTER_MUTE = "mute_master";
			public const string MASTER_UNMUTE = "unmute_master";
			public const string MAIN_MENU_MUSIC = "music_state_menu";
			public const string IN_GAME_MUSIC = "music_state_in_game";
			public const string STOP_ALL = "stopAll";
		}
	}
}