using UnityEngine;
using System.Collections;
using Assets.scripts.sound;

public class PenguinSounds : MonoBehaviour {

	//Deaths
	public void PlayPenguinDeathElectrocution() {
		AkSoundEngine.PostEvent ("penguin_death_electrocution", gameObject);
	}

	public void PlayPenguinDeathExcavator() {
		AkSoundEngine.PostEvent ("penguin_death_excavator", gameObject);
	}

	public void PlayPenguinDeathDrowning() {
		AkSoundEngine.PostEvent ("penguin_death_drowning", gameObject);
	}

	public void PlayPenguinDeathSpikes() {
		AkSoundEngine.PostEvent ("penguin_death_spikes", gameObject);
	}
	//Deaths end

	//Slide
	public void PlayPenguinSlideTakeoff() {
		AkSoundEngine.PostEvent ("penguin_slide_takeoff", gameObject);
	}

	public void PlayPenguinSlideLoop() {
		AkSoundEngine.PostEvent ("penguin_slide_loop", gameObject);
	}

	public void PlayPenguinSlideEnd() {
		AkSoundEngine.PostEvent ("penguin_slide_end", gameObject);
	}
	//Slide end

	//Moves
	public void PlayPenguinJump() {
		AkSoundEngine.PostEvent ("penguin_tool_jump_used", gameObject);
	}

	public void PlayPenguinCelebrate() {
		AkSoundEngine.PostEvent ("penguin_celebrate", gameObject);
	}

	public void PlayPenguinEdgeFall() {
		AkSoundEngine.PostEvent ("penguin_edge_fall", gameObject);
	}

	public void PlayPenguinGetUp() {
		AkSoundEngine.PostEvent ("penguin_get_up", gameObject);
	}

	public void PlayPenguinLand() {
		AkSoundEngine.PostEvent ("penguin_land", gameObject);
	}

	public void PlayPenguinReact() {
		AkSoundEngine.PostEvent ("penguin_react", gameObject);
	}

	public void PlayPenguinSpawn() {
		AkSoundEngine.PostEvent ("penguin_spawn", gameObject);
	}

	public void PlayPenguinMove() {
		AkSoundEngine.PostEvent ("penguin_move", gameObject);
	}

	public void PlayPenguinMoveVoice() {
		AkSoundEngine.PostEvent ("penguin_move_voice", gameObject);
	}
	//Moves end
}