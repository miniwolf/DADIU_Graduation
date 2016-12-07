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
		private Button tooltips;
		private Image ttsButtonImage;
		private GameObject onTText;
		private GameObject offTText;
		private Button back;
		private Text settingsText;
		public Color selected;
		public Color noSelected;
		private Button credits;
		private Text soundText, languageText, tooltipsText;

		void Start() {
		    TranslateApi.Register(this);
			// flag image
			languageImage = GameObject.FindGameObjectWithTag(TagConstants.UI.LANGUAGE_IMAGE).GetComponent<Image>();
			// get all components
			GetLanguagePanel();
			GetTooltipsPanel();
			GetMusicPanel();
			// back button
			back = GameObject.FindGameObjectWithTag(TagConstants.UI.BACK_SETTINGS).GetComponent<Button>();
			settingsText = GameObject.FindGameObjectWithTag(TagConstants.UI.SETTINGS_TEXT).GetComponent<Text>();
			languageText = GameObject.FindGameObjectWithTag(TagConstants.UI.LANGUAGE_TEXT).GetComponent<Text>();
			soundText = GameObject.FindGameObjectWithTag(TagConstants.UI.SOUND_TEXT).GetComponent<Text>();
			tooltipsText = GameObject.FindGameObjectWithTag(TagConstants.UI.TOOLTIPS_TEXT).GetComponent<Text>();
			credits = GameObject.FindGameObjectWithTag(TagConstants.UI.CREDITS_BUTTON).GetComponent<Button>();
			UpdateTexts();
		}

		private void GetMusicPanel() {
			// music button and image on it
			GameObject mus = GameObject.FindGameObjectWithTag(TagConstants.UI.TOGGLE_CHANGE_MUSIC);
			music = mus.GetComponent<Button>();
			musButtonImage = mus.GetComponentInChildren<Image>();
			// on/off text for music
			onText = GameObject.FindGameObjectWithTag(TagConstants.UI.ON_TEXT);
			offText = GameObject.FindGameObjectWithTag(TagConstants.UI.OFF_TEXT);
			// check player prefs
			if ( Prefs.MasterOn() ) {
				Selection(onText, selected, true); //music is on
				Selection(offText, noSelected, false);
				// flip image to select left text
				FlipButtonImage(musButtonImage);
			} else {
				Selection(onText, noSelected, false); //music is off
				Selection(offText, selected, true);
			}

			// functionality on click
			music.onClick.AddListener(() => ChangeMusic());
		}

		private void GetTooltipsPanel() {
			// tooltips button and image on it
			GameObject tts = GameObject.FindGameObjectWithTag(TagConstants.UI.TOGGLE_CHANGE_TOOLTIPS);
			tooltips = tts.GetComponent<Button>();
			ttsButtonImage = tts.GetComponentInChildren<Image>();
			// on/off text for tooltips
			onTText = GameObject.FindGameObjectWithTag(TagConstants.UI.ON_TTEXT);
			offTText = GameObject.FindGameObjectWithTag(TagConstants.UI.OFF_TTEXT);
			// check player prefs
			if ( Prefs.IsTooltipsOn() ) {
				Selection(onTText, selected, true); //tooltip is on
				Selection(offTText, noSelected, false);
				// flip image to select left text
				FlipButtonImage(ttsButtonImage);
			} else {
				Selection(onTText, noSelected, false); //tooltip is off
				Selection(offTText, selected, true);
			}

			// functionality on click
			tooltips.onClick.AddListener(() => ChangeTooltips());
		}

		private void GetLanguagePanel() {
			// language button and image on it
			GameObject lan = GameObject.FindGameObjectWithTag(TagConstants.UI.DROPDOWN_CHANGE_LANGUAGE);
			language = lan.GetComponent<Button>();
			lanButtonImage = lan.GetComponentInChildren<Image>();
			// text language
			ukText = GameObject.FindGameObjectWithTag(TagConstants.UI.UK_TEXT);
			dkText = GameObject.FindGameObjectWithTag(TagConstants.UI.DK_TEXT);
			// check player prefs
			if ( Prefs.IsEnglishOn() ) {
				Selection(ukText, selected, true); //uk is on
				Selection(dkText, noSelected, false);
				// flip image to select left text (uk)
				FlipButtonImage(lanButtonImage);
			} else {
				Selection(ukText, noSelected, false); 
				Selection(dkText, selected, true); //dk is on
			}

			//update image flag 
			UpdateLanguageImage();

			// functionality on click
			language.onClick.AddListener(() => ChangeLanguage());
		}

		private void Selection(GameObject text, Color col, bool isEnable) {
			text.GetComponent<Text>().color = col;
			Outline[] outlines = text.GetComponents<Outline>();
			foreach(Outline outline in outlines) {
				outline.enabled = isEnable;
			}
		}

		public void ChangeMusic() {
			ChangeButtons(Prefs.MasterOn(), onText, offText, musButtonImage);
			// music is on and we press button
			if(Prefs.MasterOn()) {
//				AkSoundEngine.PostEvent(SoundConstants.Master.MUSIC_MUTE, Camera.main.gameObject);
				AkSoundEngine.PostEvent(SoundConstants.Master.MASTER_MUTE, Camera.main.gameObject);
				Prefs.SetMaster(false);
			} else {
//				AkSoundEngine.PostEvent(SoundConstants.Master.MUSIC_UNMUTE, Camera.main.gameObject);
				AkSoundEngine.PostEvent(SoundConstants.Master.MASTER_UNMUTE, Camera.main.gameObject);
				Prefs.SetMaster(true);
			}
		}

		public void ChangeTooltips() {
			ChangeButtons(Prefs.IsTooltipsOn(), onTText, offTText, ttsButtonImage);
			// tooltips was on and we press button
			if ( Prefs.IsTooltipsOn() ) {
				Prefs.SetTooltips(0);
			} else {
				Prefs.SetTooltips(1);
			}
		}

		public void ChangeLanguage() {
			ChangeButtons(Prefs.IsEnglishOn(), ukText, dkText, lanButtonImage);
			SupportedLanguage newLanguage = ResolveLangauge();
			TranslateApi.ChangeLanguage(newLanguage);
			UpdateLanguageImage();
		}

		private SupportedLanguage ResolveLangauge() {
			if ( Prefs.IsEnglishOn() ) {
				// change to danish
				Prefs.SetLanguage(1);
				return SupportedLanguage.DEN;
			} else {
				// change to english
				Prefs.SetLanguage(0);
				return SupportedLanguage.ENG;
			}
		}

		public void OnLanguageChange(SupportedLanguage newLanguage) {
			UpdateTexts();
		}

		private void UpdateTexts() {
			settingsText.text = TranslateApi.GetString(LocalizedString.settings);
			Text[] creditsText = credits.GetComponentsInChildren<Text>();
			creditsText[0].text = TranslateApi.GetString(LocalizedString.credits);
			creditsText[1].text = TranslateApi.GetString(LocalizedString.info);
			soundText.text = TranslateApi.GetString(LocalizedString.sound);
			languageText.text = TranslateApi.GetString(LocalizedString.language);
			tooltipsText.text = TranslateApi.GetString(LocalizedString.tooltips);
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
			if( Prefs.IsEnglishOn() ) {
				// this is looking in the folder Assets/Resources so these two flags must be there
				languageImage.sprite = (Sprite)Resources.Load<Sprite>("Flag - Uk");
			} else {
				languageImage.sprite = (Sprite)Resources.Load<Sprite>("Flag - Dk");
			}
		}

		void OnDestroy() {
			TranslateApi.UnRegister(this);
		}
	}
}