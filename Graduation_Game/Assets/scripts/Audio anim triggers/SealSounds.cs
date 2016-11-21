using UnityEngine;
using System.Collections;
using Assets.scripts.sound;

public class SealSounds : MonoBehaviour {

	//Deaths
	public void PlaySealDeathElectrocution() {
		AkSoundEngine.PostEvent ("seal_death_electrocution", gameObject);
	}

	public void PlaySealDeathExcavator() {
		AkSoundEngine.PostEvent ("seal_death_excavator", gameObject);
	}

	public void PlaySealDeathDrowning() {
		AkSoundEngine.PostEvent ("seal_death_drowning", gameObject);
	}

	public void PlaySealDeathSpikes() {
		AkSoundEngine.PostEvent ("seal_death_spikes", gameObject);
	}
	//Deaths end



	//Moves
	public void PlaySealEdgeFall() {
		AkSoundEngine.PostEvent ("seal_edge_fall", gameObject);
	}

	public void PlaySealJump() {
		AkSoundEngine.PostEvent ("seal_jump", gameObject);
	}

	public void PlaySealLand() {
		AkSoundEngine.PostEvent ("seal_land", gameObject);
	}
	//Moves end
}