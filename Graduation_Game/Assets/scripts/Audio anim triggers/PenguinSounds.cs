using UnityEngine;
using System.Collections;
using Assets.scripts.sound;

public class PenguinSounds : MonoBehaviour {

	public void playMovingSound () {
<<<<<<< HEAD
		AkSoundEngine.PostEvent ("penguin_move", gameObject);
=======
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
>>>>>>> develop
		//Debug.Log ("moving sound is playing here from " + transform.parent.name);
	}
}