using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts;

public class SFXSoundSlider : MonoBehaviour {

	private int value;

	void Start(){
		value = Prefs.GetSFXValue();
		GetComponent<Slider>().value = (float)value;
		AkSoundEngine.SetRTPCValue("rtpc_sfx_volume", value);
	}

	public void ChangeVolume(){
		value = (int)GetComponent<Slider>().value;
		AkSoundEngine.SetRTPCValue("rtpc_sfx_volume", value);
		Prefs.SetSFXValue(value);
	}
}
