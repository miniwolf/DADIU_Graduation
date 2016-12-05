using Assets.scripts.eggHatching;
using Assets.scripts.sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.UI.inventory;

namespace Assets.scripts {
    public class Prefs {
		// TODO save status is level has been beat before
		public const string STATUS = "status";
		public const string STARS = "stars";
		public const string SOUND_MASTER = "pref_master_sound";

		public const string CURRENT = "current";
		public const string COMPLETED = "completed";
		public const string LOCKED = "locked";
		private const string WON = "won";

		public const string LEVEL_UNLOCK_INDEX = "LevelUnlockIndex";
		public const string LEVEL_WON_STARS = "LevelWonStars";
		public const string LEVEL_LAST_PLAYED_NAME = "LevelPlayedName";
		public const string TOOLTIPS = "tooltips";
		public const string LANGUAGE = "language"; //0 English, 1 Danish
        private const int TRUE = 1;
        private const int FALSE = 0;
		private const string TOTALSTARS = "TotalStars";
        private const string LAST_HATCH_TIME = "pref_last_hatch_time";
        private const int DEFAULT_HATCH_DURATION = 15;
        private const string HATCH_DURATION = "pref_hatch_duration";

		
		public static void SetCurrentLevelToWon() {
			PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + WON, 1);
		}

		public static int GetLevelWonStatus(string levelName) {
			return PlayerPrefs.GetInt(levelName + WON);
		}

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

		public static void DeleteKey(string key) {
			PlayerPrefs.DeleteKey(key);
		}

        public static int GetLastHatchTime() {
            return PlayerPrefs.GetInt(LAST_HATCH_TIME, DateTimeUtil.Seconds());
        }

        public static int UpdateLastHatchTime() {
            PlayerPrefs.SetInt(LAST_HATCH_TIME, DateTimeUtil.Seconds());
            return GetLastHatchTime();
        }

        public static int GetHatchDuration() {
            return PlayerPrefs.GetInt(HATCH_DURATION, DEFAULT_HATCH_DURATION);
        }

        // todo use this method when user buys 2x speed hatching increase
        public static void HatchDurationDecrease() {
            var i = GetHatchDuration() / 2;
            PlayerPrefs.SetInt(HATCH_DURATION, i > 1 ? i : 1);
        }
    }
}