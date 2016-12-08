using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts;

public class MusicSlider : MonoBehaviour {

	private int value;

	void Start(){
		value = Prefs.GetMusicValue();
		GetComponent<Slider>().value = (float)value;
		AkSoundEngine.SetRTPCValue("rtpc_music_volume", value);
	}

	public void ChangeVolume(){
		value = (int)GetComponent<Slider>().value;
		AkSoundEngine.SetRTPCValue("rtpc_music_volume", value);
		Prefs.SetMusicValue(value);
	}
}
