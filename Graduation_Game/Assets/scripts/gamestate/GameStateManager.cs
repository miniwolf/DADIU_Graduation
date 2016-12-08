using System.Collections;
using Assets.scripts.sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.gamestate {
	public class GameStateManager : MonoBehaviour {
		private bool isGameFrozen;

	    private void OnEnable() {
	        SceneManager.sceneLoaded += NewLevelLoaded;
	    }

	    private void OnDisable() {
	        SceneManager.sceneLoaded -= NewLevelLoaded;
	    }

		public void SetGameFrozen(bool frozen) {
			isGameFrozen = frozen;
		}

		public bool IsGameFrozen() {
			return isGameFrozen;
		}

	    void NewLevelLoaded(Scene scene, LoadSceneMode mode) {
	        Debug.Log("New scene loaded: " + scene.name + " masterOn: " + Prefs.MasterOn());

	        if (!Prefs.MasterOn()) {
	            AkSoundEngine.PostEvent(SoundConstants.Master.MUSIC_MUTE, Camera.main.gameObject);
	            AkSoundEngine.PostEvent(SoundConstants.Master.MASTER_MUTE, Camera.main.gameObject);
	            return;
	        }

	        string ev;

	        if(scene.name.Equals("MainMenuScene") || scene.name.Equals("Settings")) {
	            ev = SoundConstants.Master.MAIN_MENU_MUSIC;
	        } else {
	            ev = SoundConstants.Master.IN_GAME_MUSIC;
	        }
	        Debug.Log("Sound: " + ev);
	        StartCoroutine(PostponeSound(ev));
	    }

	    IEnumerator PostponeSound(string ev)	    {
//		    AkSoundEngine.PostEvent(SoundConstants.Master.STOP_ALL, gameObject);
//		    AkSoundEngine.StopAll();
	        yield return new WaitForSeconds(0.1f);
//		    AkSoundEngine.PostEvent(SoundConstants.Master.STOP_ALL, gameObject);
		    AkSoundEngine.StopAll();
	        AkSoundEngine.PostEvent(SoundConstants.Master.MUSIC_UNMUTE, Camera.main.gameObject);
	        AkSoundEngine.PostEvent(ev, Camera.main.gameObject);
	    }
	}
}