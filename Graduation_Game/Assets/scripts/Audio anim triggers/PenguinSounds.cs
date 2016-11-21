using UnityEngine;
using System.Collections;

public class PenguinSounds : MonoBehaviour {

	public void playMovingSound () {
		AkSoundEngine.PostEvent ("penguin_move", gameObject);
		//Debug.Log ("moving sound is playing here from " + transform.parent.name);
	}
}