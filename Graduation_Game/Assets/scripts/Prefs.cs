using Assets.scripts.sound;
using UnityEngine;

namespace Assets.scripts
{
    public class Prefs {

        private const string SOUND_MASTER = "pref_master_sound";
		public const string LEVEL1STATUS = "Level1status";
		public const string LEVEL2STATUS = "Level2status";
		public const string LEVEL3STATUS = "Level3status";
		public const string LEVEL4STATUS = "Level4status";
		public const string LEVEL5STATUS = "Level5status";
		public const string COMPLETED = "completed";
		public const string CURRENT = "current";
		public const string LOCKED = "locked";
		public const string STATUS = "status";
		public const string STARS = "stars";
		public const string LEVEL_UNLOCK_INDEX = "LevelUnlockIndex";
		public const string LEVEL_WON_STARS = "LevelWonStars";
		public const string LEVEL_LAST_PLAYED_NAME = "LevelPlayedName";
		public const string TOOLTIPS = "tooltips";

		public static void SetLevelLastPlayedName(string levelPlayedLastName) {
			PlayerPrefs.SetString(LEVEL_LAST_PLAYED_NAME, levelPlayedLastName);
		}

		public static string GetLevelLastPlayedName() {
			return PlayerPrefs.GetString(LEVEL_LAST_PLAYED_NAME);
		}

		public static void SetLevelWonStars(string levelName, int stars) {
			PlayerPrefs.SetInt(levelName, stars);
		}

		public static int GetLevelWonStars(string levelName) {
			return PlayerPrefs.GetInt(levelName);
		}

		public static int GetLevelUnlockIndex() {
			return PlayerPrefs.GetInt(LEVEL_UNLOCK_INDEX);
		}

		public static void SetLevelUnlockIndex(int levelIndexToUnlock) {
			PlayerPrefs.SetInt(LEVEL_UNLOCK_INDEX, levelIndexToUnlock);
		}

		public static void SetLevelStatus(string levelName, string status) {
			PlayerPrefs.SetString(levelName, status);
		}

		public static string GetLevelStatus(string levelName) {
			return PlayerPrefs.GetString(levelName);
		}

		public static bool IsLevelStatusComplete(string levelName) {
			return PlayerPrefs.GetString(levelName) == COMPLETED;
		}


		public static bool IsLevelStatusCurrent(string levelName) {
			return PlayerPrefs.GetString(levelName) == CURRENT;
		}

		public static bool IsTooltipsOn() {
			return PlayerPrefs.GetInt(TOOLTIPS) == 1;
		}
			 
		public static void SetTooltips(int active) {
			PlayerPrefs.SetInt(TOOLTIPS, active);
		}

		public static void SetMaster(bool musicOn) {
            PlayerPrefs.SetString(SOUND_MASTER, musicOn ? SoundConstants.Master.MASTER_MUTE: SoundConstants.Master.MASTER_UNMUTE);
        }

        public static bool MasterOn() {
            return SoundConstants.Master.MASTER_UNMUTE.Equals(PlayerPrefs.GetString(SOUND_MASTER, SoundConstants.Master.MASTER_UNMUTE));
        }






    }
}