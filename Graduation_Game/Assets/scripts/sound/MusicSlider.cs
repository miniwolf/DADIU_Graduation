using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour {

	private int value;

	public void ChangeVolume(){
		value = (int)GetComponent<Slider>().value;
		AkSoundEngine.SetRTPCValue("rtpc_music_volume", value);
	}
}
