using UnityEngine;
using System.Collections;
using Assets.scripts.sound;

public class PenguinSounds : MonoBehaviour {

	public void playMovingSound () {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
		//Debug.Log ("moving sound is playing here from " + transform.parent.name);
	}
}