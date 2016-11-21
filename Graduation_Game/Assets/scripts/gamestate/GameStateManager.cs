using Assets.scripts.sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.gamestate {
	public class GameStateManager : MonoBehaviour {
		private bool isGameFrozen;

		public void SetGameFrozen(bool frozen) {
			isGameFrozen = frozen;
		}

		public bool IsGameFrozen() {
			return isGameFrozen;
		}

        void OnLevelWasLoaded ()
        {
            string ev;
            if (SceneManager.GetActiveScene().name.Equals("MainMenuScene")){
                ev = SoundConstants.Music.MAIN_MENU_MUSIC;
            }else{
                ev = SoundConstants.Music.IN_GAME_MUSIC;
            }
            AkSoundEngine.PostEvent(ev, Camera.main.gameObject);
        }
    }
}