using System;
using Assets.scripts.UI.inventory;
using Assets.scripts.UI.screen;
using Assets.scripts.UI.translations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.sound;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Assets.scripts.UI.mainmenu {
	public class MainMenuScript : UIController, LanguageChangeListener, TouchInputListener, MouseInputListener {

		public LvlData[] levels;
		private Dropdown languageDropdown;
		private Image popup;
		private InputManager inputManager;
		private int unlockIdx = 0; // unlockIdx 0 : level 1, unlockIdx 1 : level 2 and so on
		private int starsWon = 0; // Stars gathered after last level played
		private string levelPlayedName;
		private string[] levelNames;
		public Sprite stars1;
		public Sprite stars2;
		public Sprite stars3;

		//Dictionary<string, int> starsAchieved; // Stars for each levels are stored here

		protected void Start() {
			TranslateApi.Register(this);
			inputManager = FindObjectOfType<InputManagerImpl>(); // not registering in injection system yet
			inputManager.SubscribeForMouse(this);
			inputManager.SubscribeForTouch(this);

			levelNames = new string[10];
			for(int i = 0; i < levelNames.Length; i++) {
				levelNames[i] = "Level" + (i+1).ToString();
			}

			unlockIdx = PlayerPrefs.GetInt("LevelUnlockIndex");

			InitilizeLevels();
			UnlockLevels(unlockIdx);

			starsWon = PlayerPrefs.GetInt("LevelWonStars");
			levelPlayedName = PlayerPrefs.GetString("LevelPlayedName");


			SetStarPrefsPerLevel(levelNames);

			LoadStars();

			/* languageDropdown = GameObject.FindGameObjectWithTag(TagConstants.UI.DROPDOWN_CHANGE_LANGUAGE).GetComponent<Dropdown>();

             languageDropdown.onValueChanged.AddListener(delegate {
                 OnDropdownChanged();
             });
			*/
			popup = GameObject.FindGameObjectWithTag(TagConstants.UI.POPUP_PENGUIN_REQUIRED).GetComponent<Image>();

			DisablePopup();
			//UpdateTexts();
		}

		// Saves preferences for how many stars are collected for each level
		void SetStarPrefsPerLevel(string[] levelNames) {
			for (int i = 0; i < levelNames.Length; i++) {

				// Checks which level was won last time and sets the stars accordingly
				if (levelPlayedName == levelNames[i]) {
					PlayerPrefs.SetInt(levelNames[i], starsWon);
				}
			}
		}

		// Loads stars to view
		void LoadStars() {
			int starsWonForLevel = 0;
			for (int i = 0; i < levelNames.Length; i++) {
				starsWonForLevel = PlayerPrefs.GetInt(levelNames[i]);
				SetStarSprite(i, starsWonForLevel);
			}
		}

		// Replaces the sprite on a level button image according to how many stars were won
		void SetStarSprite(int lvlIdx, int numOfStars) {
			switch (numOfStars) {
				case 1:
					levels[lvlIdx].btnFromScene.transform.GetChild(1).GetComponent<Image>().sprite = stars1;
					break;
				case 2:
					levels[lvlIdx].btnFromScene.transform.GetChild(1).GetComponent<Image>().sprite = stars2;
					break;
				case 3:
					levels[lvlIdx].btnFromScene.transform.GetChild(1).GetComponent<Image>().sprite = stars3;
					break;
				default:
					break;
			}
		}
 

		// Every level except the first is locked from the start
		void InitilizeLevels() {
			foreach (var lvlData in levels) {
				var c = lvlData;
				c.btnFromScene.interactable = false;
				lvlData.btnFromScene.onClick.AddListener(() => CheckLoadLevel(c));
			}
			levels[0].btnFromScene.interactable = true;
		}

		// Makes levels available depending on how many levels to unlock
		void MakeLevelsInteractable(int numOfLvlsToUnlock) {
			for (int i = 0; i <= numOfLvlsToUnlock; i++) {
				Debug.Log("Levels are interactable");
				levels[i].btnFromScene.interactable = true;
			}
		}

		// Unlocks numOfLvlsToUnlock + 1 levels
		void UnlockLevels(int numOfLvlsToUnlock) {
			switch (numOfLvlsToUnlock) {
				case 1:
					MakeLevelsInteractable(1);
					break;
				case 2:
					MakeLevelsInteractable(2);
					break;
				case 3:
					MakeLevelsInteractable(3);
					break;
				case 4:
					MakeLevelsInteractable(4);
					break;
				case 5:
					MakeLevelsInteractable(5);
					break;
				case 6:
					MakeLevelsInteractable(6);
					break;
				case 7:
					MakeLevelsInteractable(7);
					break;
				case 8:
					MakeLevelsInteractable(8);
					break;
				case 9:
					MakeLevelsInteractable(9);
					break;
				case 10:
					MakeLevelsInteractable(10);
					break;
				default:
					break;
			}
		}

		void OnDestroy() {
			TranslateApi.UnRegister(this);
			inputManager.UnsubscribeForMouse(this);
			inputManager.UnsubscribeForTouch(this);
		}

		private void OnDropdownChanged() {
			SupportedLanguage newLanguage = ResolveLangauge();
			TranslateApi.ChangeLanguage(newLanguage);
		}

		private SupportedLanguage ResolveLangauge() {
			return languageDropdown.value == 0 ? SupportedLanguage.ENG : SupportedLanguage.DEN;
		}

		private void CheckLoadLevel(LvlData lvl) {
			AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.BUTTON_PRESS, Camera.main.gameObject);
			if(popup.enabled) {
				DisablePopup();
				return;
			}

			if(Inventory.penguinCount.GetValue() >= lvl.penguinsRequired) {
				SceneManager.LoadSceneAsync(lvl.sceneFileName);
			} else {
				EnablePopup();
			}
		}

		public void OnLanguageChange(SupportedLanguage newLanguage) {
			//UpdateTexts();
		}

		private void UpdateTexts() {
			foreach(var lvl in levels) {
				lvl.btnFromScene.GetComponentInChildren<Text>().text = TranslateApi.GetString(lvl.localizedText);
			}
			popup.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.popupNotEnoguhPenguins);
		}

		public void SettingsButton() {
			SceneManager.LoadScene("Settings");	
		}

		private void DisablePopup() {
			popup.transform.localScale = Vector3.zero;
			popup.enabled = false;
		}

		private void EnablePopup() {
			popup.transform.localScale = Vector3.one;
			popup.enabled = true;
		}

		public void OnTouch(Touch[] allTouches) {
		//	DisablePopup(); //apparently it will activate and get removed super fast
		}

		[Serializable] public struct LvlData {
			public string sceneFileName;
			public Button btnFromScene;
			public LocalizedString localizedText;
			public int penguinsRequired;
		}


		//
		// UNUSED LISTENERS
		//

		public void OnMouseRightDown() {
			DisablePopup();
		}

		public void OnMouseRightUp() {

		}

		public void OnMouseRightPressed() {

		}

		public void OnMouseLeftDown() {
			DisablePopup();
		}

		public void OnMouseLeftUp() {
		}

		public void OnMouseLeftPressed() {
		}

	}
}