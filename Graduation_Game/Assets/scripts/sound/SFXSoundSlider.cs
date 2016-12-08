using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts;

public class SFXSoundSlider : MonoBehaviour {

	private int value;

	public void ChangeVolume(){
		value = (int)GetComponent<Slider>().value;
		AkSoundEngine.SetRTPCValue("rtpc_sfx_volume", value);
		Prefs.SetSFXValue(value);
	}
}
