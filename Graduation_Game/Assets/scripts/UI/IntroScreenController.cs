using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.scripts;

public class IntroScreenController : MonoBehaviour {

	public Sprite[] introScreens;
	private int currentScreen;
	public Text skipIntro;

	void Start() {
		currentScreen = 0;
		skipIntro =	GameObject.FindGameObjectWithTag(TagConstants.SKIPINTROTEXT).GetComponent<Text>();
		/*if (!PlayerPrefs.HasKey("NoIntroScreen")) {															      // Enable this $#!? when ready for release.
			LoadIntro();
		}*/
	}

	public void LoadNextScreen() {
		if (introScreens.Length > ++currentScreen)
			gameObject.GetComponent<Image>().sprite = introScreens[currentScreen];
		if (introScreens.Length == currentScreen + 1) {
			var next = GameObject.FindGameObjectWithTag(TagConstants.NEXTINTROBUTTON).GetComponent<Image>();
			next.enabled = false;
			next.GetComponentInChildren<Text>().enabled = false;
			skipIntro.text = "Go to Menu";
		}
	}

	public void SkipIntro() {
		gameObject.GetComponent<Image>().enabled = false;
		foreach (Image i in gameObject.GetComponentsInChildren<Image>()) {
			i.enabled = false;
			i.GetComponentInChildren<Text>().enabled = false;
		}
		//PlayerPrefs.SetInt("NoIntroScreen", 1);																// Enable this $#!? when ready for release.
	}

	public void LoadIntro() {
		gameObject.GetComponent<Image>().enabled = true;
		skipIntro.text = "Skip Intro";
		if (introScreens.Length > 0)
			gameObject.GetComponent<Image>().sprite = introScreens[0];
		foreach (Image i in gameObject.GetComponentsInChildren<Image>()) {
			i.enabled = true;
			i.GetComponentInChildren<Text>().enabled = true;
		}
	}
}
