using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.scripts;

namespace Assets.scripts.UI {
	public class IntroScreenController : MonoBehaviour {

		public Sprite[] introScreens;
		private int currentScreen;
		private Text skipIntro;
		private GameObject tvImage;
		private Button intro;
		private Canvas canvas;
		//public GameObject introPlayer;
		public Material movies;
		//private MovieTexture movie;
		private bool moviePlaying;

		void Start() {
			canvas = GameObject.Find("MainMenuCanvas").GetComponent<Canvas>();
			tvImage = GameObject.FindGameObjectWithTag(TagConstants.UI.INTRO_IMAGE);
			tvImage.SetActive(false);
			//intro = GameObject.FindGameObjectWithTag(TagConstants.UI.INTRO_BUTTON).GetComponent<Button>();
			//intro.onClick.AddListener(() => LoadIntro());
			currentScreen = 0;

			//movie = (MovieTexture)introPlayer.GetComponent<Renderer>().material.mainTexture;	
			//PlayerPrefs.DeleteKey("NoIntroScreen"); //for testing
			//skipIntro =	GameObject.FindGameObjectWithTag(TagConstants.SKIPINTROTEXT).GetComponent<Text>();
			if (!PlayerPrefs.HasKey("NoIntroScreen")) {															      // Enable this $#!? when ready for release.
				LoadIntro();
				StartCoroutine(HackSound());
			} else {
				SkipIntro();
			}

			PlayerPrefs.SetInt("NoIntroScreen", 2);
			//LoadIntro();

		}

		IEnumerator HackSound(){
			yield return new WaitForSeconds(0.2f);
			AkSoundEngine.PostEvent("music_state_none", Camera.main.gameObject);
		}

		void Update(){
		/*	if (moviePlaying) {
				if (movie.isPlaying) {
					SkipIntro();
				}
			}*/

			if (Input.GetMouseButtonDown(0)) {
				SkipIntro();
			}
		}

		public void LoadNextScreen() {
			if (introScreens.Length > ++currentScreen)
				gameObject.GetComponent<Image>().sprite = introScreens[currentScreen];
			if (introScreens.Length == currentScreen + 1) {
				var next = GameObject.FindGameObjectWithTag(TagConstants.NEXTINTROBUTTON).GetComponent<Image>();
				next.enabled = false;
				next.GetComponentInChildren<Text>().enabled = false;
				//skipIntro.text = "Go to Menu";
			}
			//if (introScreens.Length == currentScreen) {
			//	SkipIntro();
			//}
		}

		public void SkipIntro() {
			/*gameObject.GetComponent<Image>().enabled = false;
			foreach (Image i in gameObject.GetComponentsInChildren<Image>()) {
				i.enabled = false;
				i.GetComponentInChildren<Text>().enabled = false;
			}
			tvImage.SetActive(false);
			currentScreen = 0;
			PlayerPrefs.SetInt("NoIntroScreen", 1);																// Enable this $#!? when ready for release.*/
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			GameObject.FindGameObjectWithTag("IntroScreen");
			StopMovie();
			AkSoundEngine.PostEvent("music_state_menu", Camera.main.gameObject);
			//introPlayer.SetActive(false);
			//GameObject.FindGameObjectWithTag("IntroScreen").SetActive(false);
		}

		public void LoadIntro() {
		/*	tvImage.SetActive(true);
			gameObject.GetComponent<Image>().enabled = true;
			//skipIntro.text = "Skip Intro";
			if (introScreens.Length > 0)
				gameObject.GetComponent<Image>().sprite = introScreens[0];
			foreach (Image i in gameObject.GetComponentsInChildren<Image>()) {
				i.enabled = true;
				i.GetComponentInChildren<Text>().enabled = true;
			}*/

			//canvas.renderMode = RenderMode.WorldSpace;
			//introPlayer.SetActive(true);
			PlayMovie();

		}

		private void PlayMovie(){
            #if UNITY_IPHONE || UNITY_ANDROID
                Handheld.PlayFullScreenMovie("cutSceneENwithSound.mp4");
            #endif

		    moviePlaying = true;
			//movie.Play();
		}
		private void StopMovie(){
			//movie.Stop();
		}

	}
}
