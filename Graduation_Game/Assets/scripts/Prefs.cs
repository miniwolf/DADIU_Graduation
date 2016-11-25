using Assets.scripts.sound;
using UnityEngine;

namespace Assets.scripts
{
    public class Prefs {

        private const string SOUND_MASTER = "pref_master_sound";

        public static void SetMaster(bool musicOn) {
            PlayerPrefs.SetString(SOUND_MASTER, musicOn ? SoundConstants.Master.MASTER_MUTE: SoundConstants.Master.MASTER_UNMUTE);
        }

        public static bool MasterOn() {
            return SoundConstants.Master.MASTER_UNMUTE.Equals(PlayerPrefs.GetString(SOUND_MASTER, SoundConstants.Master.MASTER_UNMUTE));
        }
    }
}