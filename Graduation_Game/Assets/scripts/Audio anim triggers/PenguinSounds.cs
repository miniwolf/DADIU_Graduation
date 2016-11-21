using UnityEngine;
using System.Collections;
using Assets.scripts.sound;

public class PenguinSounds : MonoBehaviour {

	//Deaths
	public void PlayPenguinDeathElectrocution() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}

	public void PlayPenguinDeathExcavator() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}

	public void PlayPenguinDeathDrowning() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}

	public void PlayPenguinDeathSpikes() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}
	//Deaths end

	//Slide
	public void PlayPenguinSlideTakeoff() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}

	public void PlayPenguinSlideLoop() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}

	public void PlayPenguinSlideEnd() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}
	//Slide end

	//Moves
	public void PlayPenguinCelebrate() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}

	public void PlayPenguinEdgeFall() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}

	public void PlayPenguinGetUp() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}

	public void PlayPenguinLand() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}

	public void PlayPenguinSpawn() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}

	public void PlayPenguinReact() {
		AkSoundEngine.PostEvent (SoundConstants.PenguinSounds.MOVE, gameObject);
	}
	//Moves end
}