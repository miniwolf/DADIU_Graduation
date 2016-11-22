using Assets.scripts.sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.gamestate {
	public class GameStateManager : MonoBehaviour {
		private bool isGameFrozen;

		void Start() {
			SceneManager.sceneLoaded += NewLevelLoaded;
		}

		void OnDestroy() {
			SceneManager.sceneLoaded -= NewLevelLoaded;
		}

		public void SetGameFrozen(bool frozen) {
			isGameFrozen = frozen;
		}

		public bool IsGameFrozen() {
			return isGameFrozen;
		}

		void NewLevelLoaded(Scene scene, LoadSceneMode mode) {
			string ev;
		    AkSoundEngine.PostEvent(SoundConstants.Music.STOP_ALL, gameObject);
		    AkSoundEngine.StopAll();
			if(scene.name.Equals("MainMenuScene")) {
				ev = SoundConstants.Music.MAIN_MENU_MUSIC;
			} else {
				ev = SoundConstants.Music.IN_GAME_MUSIC;
			}
			AkSoundEngine.PostEvent(ev, Camera.main.gameObject);
		}
	}
}