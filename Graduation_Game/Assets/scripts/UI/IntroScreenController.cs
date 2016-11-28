using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.scripts;

namespace Assets.scripts.UI {
	public class IntroScreenController : MonoBehaviour {

		public Sprite[] introScreens;
		private int currentScreen;
		private Text skipIntro;
		private Image tvImage;
		private Button intro;

		void Start() {
			tvImage = GameObject.FindGameObjectWithTag(TagConstants.UI.INTRO_IMAGE).GetComponent<Image>();
			tvImage.enabled = false;
			intro = GameObject.FindGameObjectWithTag(TagConstants.UI.INTRO_BUTTON).GetComponent<Button>();
			intro.onClick.AddListener(() => LoadIntro());
			currentScreen = 0;
			//PlayerPrefs.DeleteKey("NoIntroScreen"); //for testing
			skipIntro =	GameObject.FindGameObjectWithTag(TagConstants.SKIPINTROTEXT).GetComponent<Text>();
			if (!PlayerPrefs.HasKey("NoIntroScreen")) {															      // Enable this $#!? when ready for release.
				LoadIntro();
			}
		}

		void Update(){
			if (Input.GetMouseButtonDown(0)) {
				LoadNextScreen();
			}
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
			if (introScreens.Length == currentScreen) {
				SkipIntro();
			}
		}

		public void SkipIntro() {
			gameObject.GetComponent<Image>().enabled = false;
			foreach (Image i in gameObject.GetComponentsInChildren<Image>()) {
				i.enabled = false;
				i.GetComponentInChildren<Text>().enabled = false;
			}
			tvImage.enabled = false;
			currentScreen = 0;
			PlayerPrefs.SetInt("NoIntroScreen", 1);																// Enable this $#!? when ready for release.
		}

		public void LoadIntro() {
			tvImage.enabled = true;
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
}
