using System;
using UnityEngine;
using Assets.scripts.UI.screen;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.UI.translations;
using Assets.scripts.sound;

namespace Assets.scripts.UI.settingsmenu {
	public class Settings : UIController, LanguageChangeListener {
		private Image languageImage;
		private Button language;
		private Image lanButtonImage;
		private GameObject ukText;
		private GameObject dkText;
		private Button music;
		private Image musButtonImage;
		private GameObject onText;
		private GameObject offText;
		private Button back;
		private Text settingsText;
		private bool musicOn;
		private bool englishOn;
		public Color selected;
		public Color noSelected;

		void Start() {
			TranslateApi.Register(this);
			// flag image
			languageImage = GameObject.FindGameObjectWithTag(TagConstants.UI.LANGUAGE_IMAGE).GetComponent<Image>();
			// get all components
			GetLanguagePanel();
			GetMusicPanel();
			// back button
			back = GameObject.FindGameObjectWithTag(TagConstants.UI.BACK_SETTINGS).GetComponent<Button>();
			back.onClick.AddListener(() => Back());

			settingsText = GameObject.FindGameObjectWithTag(TagConstants.UI.SETTINGS_TEXT).GetComponent<Text>();

			UpdateTexts();
		}


		private void GetMusicPanel() {
			// music is on by default
			musicOn = true; // todo Laura refactor this so it takes into consideration value stored in Prefs.MasterOn();
			// music button and image on it
			GameObject mus = GameObject.FindGameObjectWithTag(TagConstants.UI.TOGGLE_CHANGE_MUSIC);
			music = mus.GetComponent<Button>();
			musButtonImage = mus.GetComponentInChildren<Image>();
			// on/off text for music
			onText = GameObject.FindGameObjectWithTag(TagConstants.UI.ON_TEXT);
			Selection(onText, selected, true); //music is on
			offText = GameObject.FindGameObjectWithTag(TagConstants.UI.OFF_TEXT);
			Selection(offText, noSelected, false);
			// functionality on click
			music.onClick.AddListener(() => ChangeMusic());
			// flip image to select left texts from the start
			FlipButtonImage(musButtonImage);
		}

		private void GetLanguagePanel() {
			// english is on by default
			englishOn = true;
			// language button and image on it
			GameObject lan = GameObject.FindGameObjectWithTag(TagConstants.UI.DROPDOWN_CHANGE_LANGUAGE);
			language = lan.GetComponent<Button>();
			lanButtonImage = lan.GetComponentInChildren<Image>();
			// text language
			ukText = GameObject.FindGameObjectWithTag(TagConstants.UI.UK_TEXT);
			Selection(ukText, selected, true); //uk is on
			dkText = GameObject.FindGameObjectWithTag(TagConstants.UI.DK_TEXT);
			Selection(dkText, noSelected, false);
			// functionality on click
			language.onClick.AddListener(() => ChangeLanguage());
			// flip image to select left texts from the start
			FlipButtonImage(lanButtonImage);
		}

		private void Selection(GameObject text, Color col, bool isEnable) {
			text.GetComponent<Text>().color = col;
			Outline[] outlines = text.GetComponents<Outline>();
			foreach(Outline outline in outlines) {
				outline.enabled = isEnable;
			}
		}

		public void ChangeMusic() {
			ChangeButtons(musicOn, onText, offText, musButtonImage);
			// music is on and we press button
			if(musicOn) {
//				AkSoundEngine.PostEvent(SoundConstants.Master.MUSIC_MUTE, Camera.main.gameObject);
				AkSoundEngine.PostEvent(SoundConstants.Master.MASTER_MUTE, Camera.main.gameObject);
				musicOn = false;
			} else {
//				AkSoundEngine.PostEvent(SoundConstants.Master.MUSIC_UNMUTE, Camera.main.gameObject);
				AkSoundEngine.PostEvent(SoundConstants.Master.MASTER_UNMUTE, Camera.main.gameObject);
				musicOn = true;
			}
			// store the actual state to persistent storage
			Prefs.SetMaster(musicOn);
		}

		public void ChangeLanguage() {
			UpdateLanguageImage();
			ChangeButtons(englishOn, ukText, dkText, lanButtonImage);
			SupportedLanguage newLanguage = ResolveLangauge();
			englishOn = !englishOn;
			TranslateApi.ChangeLanguage(newLanguage);
		}

		private SupportedLanguage ResolveLangauge() {
			return englishOn == true ? SupportedLanguage.DEN : SupportedLanguage.ENG;
		}

		public void OnLanguageChange(SupportedLanguage newLanguage) {
			UpdateTexts();
		}

		private void UpdateTexts() {
			settingsText.text = TranslateApi.GetString(LocalizedString.settings);
			back.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.backsettings);
		}

		private void Back() {
			SceneManager.LoadScene("MainMenuScene");
		}

		private void ChangeButtons(bool leftIsOn, GameObject leftText, GameObject rightText, Image buttonImage) {
			FlipButtonImage(buttonImage);

			if(leftIsOn) {
				Selection(leftText, noSelected, false);
				Selection(rightText, selected, true);
			} else {
				Selection(leftText, selected, true);
				Selection(rightText, noSelected, false);
			}
		}

		private void FlipButtonImage(Image buttonImage) {
			// flip 180 degrees in x axis to get the blue in the other side
			Vector3 scaleImage = buttonImage.gameObject.transform.localScale;
			buttonImage.gameObject.transform.localScale = new Vector3(scaleImage.x * -1, scaleImage.y, scaleImage.z);
		}

		private void UpdateLanguageImage() {
			if(englishOn) {
				// this is looking in the folder Assets/Resources so these two flags must be there
				languageImage.sprite = (Sprite)Resources.Load<Sprite>("Flag - Dk");
			} else {
				languageImage.sprite = (Sprite)Resources.Load<Sprite>("Flag - Uk");
			}
		}

		void OnDestroy() {
			TranslateApi.UnRegister(this);
		}
	}
}