using System;
using UnityEngine;
using Assets.scripts.UI.screen;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.UI.translations;
using Assets.scripts.sound;

namespace Assets.scripts.UI.settingsmenu {
	public class Settings : UIController, LanguageChangeListener {
		private Toggle language;
		private Toggle music;
		private Button back;
		private Text settings;

		void Start() {
			TranslateApi.Register(this);

			language = GameObject.FindGameObjectWithTag(TagConstants.UI.DROPDOWN_CHANGE_LANGUAGE).GetComponent<Toggle>();
			music = GameObject.FindGameObjectWithTag(TagConstants.UI.TOGGLE_CHANGE_MUSIC).GetComponent<Toggle>();
			back = GameObject.FindGameObjectWithTag(TagConstants.UI.BACK_SETTINGS).GetComponent<Button>();
			settings = GameObject.FindGameObjectWithTag(TagConstants.UI.SETTINGS_TEXT).GetComponent<Text>();

			language.onValueChanged.AddListener(delegate {
				ChangeLanguage();
			});

			music.onValueChanged.AddListener(delegate {
				ChangeMusic();
			});

			back.onClick.AddListener(() => Back());

			UpdateTexts();
		}

		public void ChangeMusic() {
			if ( music.isOn ) {
				AkSoundEngine.PostEvent(SoundConstants.Music.MUSIC_UNMUTE, Camera.main.gameObject);
				AkSoundEngine.PostEvent(SoundConstants.Music.SOUND_UNMUTE, Camera.main.gameObject);
			} else {
				AkSoundEngine.PostEvent(SoundConstants.Music.MUSIC_MUTE, Camera.main.gameObject);
				AkSoundEngine.PostEvent(SoundConstants.Music.SOUND_MUTE, Camera.main.gameObject);
			}
		}

		public void ChangeLanguage() {
			SupportedLanguage newLanguage = ResolveLangauge();
			TranslateApi.ChangeLanguage(newLanguage);
		}

		private SupportedLanguage ResolveLangauge() {
			return language.isOn == true ? SupportedLanguage.ENG : SupportedLanguage.DEN;
		}

		public void OnLanguageChange(SupportedLanguage newLanguage) {
			UpdateTexts();
		}

		private void UpdateTexts() {
			settings.text =  TranslateApi.GetString(LocalizedString.settings);
			back.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.backsettings);
		}

		private void Back() {
			SceneManager.LoadScene("MainMenuScene");
		}

		void OnDestroy() {
			TranslateApi.UnRegister(this);
		}

	}
}

