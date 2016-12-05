using UnityEngine;

namespace Assets.scripts {
	public class AnimationConstants {
		public const string SHAKE = "Shake";
		//	public const string ORCADEATH = "deathSpike";
		//	public const string DROWNING = "deathDrown";
		public static readonly string[] ELECTRICUTION = { "deathElectric" };
		public static readonly string[] SPIKEDEATHWALL = { "deathSpike", "deathSpike2", "deathSpike3" };
		public static readonly string[] SPIKEDEATHGROUND = { "deathPit", "deathPit2", "deathPit3" };
		public static readonly string[] PITDEATH = { "deathPit" };
		public static readonly string[] JUMP = { "jumping", "jumping2", "jumping3" };
		public static readonly string[] LANDING = {"land", "land2"};
		public static readonly string[] DROWNDEATH = { "deathDrown", "deathDrown2" };
		public static readonly string[] CELEBRATE = { "celebrateGoodTimes",  "celebrateGoodTimes2",  "celebrateGoodTimes3"};
		public static readonly string[] SLIDE = { "ifSliding" };
		public static readonly string[] PENGUIN_FALL = { "isFalling" };
		public static readonly string[] TRIGGER_REACT_TO_DEATH = { "reactToDeath" };
		//	public const string SPEED = "ifSpeed";
		//	public const string ENLARGE = "isEnlarged";
		//	public const string MINIMIZE = "isShrinked";
		public const string SEAL_MOVE = "moveSeal";
		public const string SEAL_JUMP = "isJumping";
		public const string SEAL_FALL = "isFalling";
		public const string PANELIN = "PanelIn";
		public const string PANELOUT = "PanelOut";

		public class Tools {
			public const string SPRING = "spring";
		}
	}
}
