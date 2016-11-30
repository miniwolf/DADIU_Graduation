using Assets.scripts.sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.UI.inventory;

namespace Assets.scripts {
    public class Prefs {
		public const string LEVEL1 = "W0Level0";
		public const string LEVEL2 = "W0Level1";
		public const string LEVEL3 = "W0Level2";
		public const string LEVEL4 = "W1Level1";
		public const string LEVEL5 = "W1Level2";
		public const string LEVEL6 = "W1Level3";
		public const string LEVEL7 = "W1Level4";
		public const string LEVEL8 = "W1Level5";
		public const string LEVEL9 = "W1Level6";
		public const string LEVEL10 = "W1Level7";

		public const string STATUS = "status";
		public const string STARS = "stars";
		public const string SOUND_MASTER = "pref_master_sound";

		public const string CURRENT = "current";
		public const string COMPLETED = "completed";
		public const string LOCKED = "locked";

		//public const string LEVEL1STATUS = InventoryConstants.LEVEL1STATUS;
		public const string LEVEL_UNLOCK_INDEX = "LevelUnlockIndex";
		public const string LEVEL_WON_STARS = "LevelWonStars";
		public const string LEVEL_LAST_PLAYED_NAME = "LevelPlayedName";
		public const string TOOLTIPS = "tooltips";
		public const string LANGUAGE = "language"; //0 English, 1 Danish
        private const int TRUE = 1;
        private const int FALSE = 0;
		private const string TOTALSTARS = "TotalStars";

		public static void SetLevelLastPlayedName(string levelPlayedLastName) {
			PlayerPrefs.SetString(LEVEL_LAST_PLAYED_NAME, levelPlayedLastName);
		}

		public static string GetLevelLastPlayedName() {
			return PlayerPrefs.GetString(LEVEL_LAST_PLAYED_NAME);
		}

		public static void SetLevelWonStars(string levelName, int stars) {
			PlayerPrefs.SetInt(levelName + STARS, stars);
		}

		public static int GetLevelWonStars(string levelName) {
			return PlayerPrefs.GetInt(levelName + STARS);
		}

		public static int GetLevelUnlockIndex() {
			return PlayerPrefs.GetInt(LEVEL_UNLOCK_INDEX);
		}

		public static void SetLevelUnlockIndex(int levelIndexToUnlock) {
			PlayerPrefs.SetInt(LEVEL_UNLOCK_INDEX, levelIndexToUnlock);
		}

		public static void SetLevelStatus(string levelName, string status) {
			PlayerPrefs.SetString(levelName + STATUS, status);
		}

		public static string GetLevelStatus(string levelName) {
			return PlayerPrefs.GetString(levelName + STATUS);
		}

		public static bool IsLevelStatusComplete(string levelName) {
			return PlayerPrefs.GetString(levelName + STATUS) == COMPLETED;
		}

		public static bool IsLevelStatusCurrent(string levelName) {
			return PlayerPrefs.GetString(levelName + STATUS) == CURRENT;
		}

		public static bool IsTooltipsOn() {
			return PlayerPrefs.GetInt(TOOLTIPS) == TRUE;
		}
			 
		public static void SetTooltips(int active) {
			PlayerPrefs.SetInt(TOOLTIPS, active);
		}

		public static bool IsEnglishOn() {
			return PlayerPrefs.GetInt(LANGUAGE) == 0;
		}

		public static void SetLanguage(int language) {
			PlayerPrefs.SetInt(LANGUAGE, language);
		}

		public static void SetMaster(bool musicOn) {
            PlayerPrefs.SetInt(SOUND_MASTER, musicOn ? TRUE: FALSE);
        }

        public static bool MasterOn() {
            return PlayerPrefs.GetInt(SOUND_MASTER, TRUE) == TRUE;
        }

        public static void SetTotalStars(int totalStars) {
             PlayerPrefs.SetInt(TOTALSTARS, totalStars);
        }

		public static int GetTotalStars() {
			return PlayerPrefs.GetInt(TOTALSTARS);
		}

		public static int GetStarsForCurrentLevel() {
            return PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + STARS);
        }

        public static void SetStarsForCurrentLevel(int value) {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + STARS, value);
        }
    }
}