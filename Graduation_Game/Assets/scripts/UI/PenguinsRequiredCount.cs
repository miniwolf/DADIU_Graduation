using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.scripts.UI.mainmenu;

public class PenguinsRequiredCount : MonoBehaviour {
	private MainMenuScript.LvlData[] levels;

	public Text[] penguinRequiredTexts;

	// Use this for initialization
	void Start () {
		levels = GetComponent<MainMenuScript>().levels;
		
		for (int i = 0; i < levels.Length; i++) {
			penguinRequiredTexts[i].text = levels[i].penguinsRequired.ToString();
		}
	}
	
}
