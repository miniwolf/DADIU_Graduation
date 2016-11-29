using System;
using Assets.scripts.UI.inventory;
using Assets.scripts.UI.screen;
using Assets.scripts.UI.translations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.sound;
using Assets.scripts.controllers.actions.game;
using Assets.scripts.UI;
using System.Collections;

namespace Assets.scripts.UI.mainmenu {
	public class MainMenuScript : UIController, LanguageChangeListener, TouchInputListener, MouseInputListener {

		public LvlData[] levels;
		public LvlMarkers[] markers;
		private Dropdown languageDropdown;
		private Image popup;
		private InputManager inputManager;
		private int unlockIdx = 0; // unlockIdx 0 : level 1, unlockIdx 1 : level 2 and so on
		private string[] levelNames; // todo why not use levels[i].sceneFileName instead?
	    private int numberOfLevelsInCanvas = 5; // todo why not user levels.Count instead?
		private FillImage fillImageScript;

		public Sprite stars1;
		public Sprite stars2;
		public Sprite stars3;

		public Sprite levelBtnCurrent;
		public Sprite levelBtnCompleted;
		public Sprite levelBtnLocked;
		public Sprite levelBtnNotAccessable; // Coming soon...

		protected void Awake() {
			if(!Prefs.IsLevelStatusComplete(Prefs.LEVEL1STATUS)) {
				Prefs.SetLevelStatus(Prefs.LEVEL1STATUS, Prefs.CURRENT); // Level 1 is by default the current level
			}
		}

		protected void Start() {
			TranslateApi.Register(this);
			inputManager = FindObjectOfType<InputManagerImpl>(); // not registering in injection system yet
			inputManager.SubscribeForMouse(this);
			inputManager.SubscribeForTouch(this);
					
			fillImageScript = GetComponent<FillImage>();

			levelNames = new string[numberOfLevelsInCanvas];
			for(int i = 0; i < 3; i++) {
				levelNames[i] = "W0Level" + (i).ToString();
			}
			for(int i = 3; i < levelNames.Length; i++) {
				levelNames[i] = "W1Level" + (i+1).ToString();
			}

			InitilizeLevels();

			UnlockLevels(Prefs.GetLevelUnlockIndex());

			SetStarPrefsPerLevel(levelNames);

			LoadStars();

			UpdateLevelsStatusOnLoad();

			if(Prefs.IsLevelStatusCurrent(Prefs.LEVEL1STATUS)) {
				levels[0].btnFromScene.GetComponent<Image>().sprite = levelBtnCurrent;
			} else {
				StartCoroutine(WaitForFill());
			}
			
			/* languageDropdown = GameObject.FindGameObjectWithTag(TagConstants.UI.DROPDOWN_CHANGE_LANGUAGE).GetComponent<Dropdown>();

				languageDropdown.onValueChanged.AddListener(delegate {
					OnDropdownChanged();
				});
			*/

			// first time we set up the language as English, tooltips and music on 
			if (!PlayerPrefs.HasKey("NoIntroScreen")) {															    
				Prefs.SetTooltips(1);
				Prefs.SetLanguage(0);
				Prefs.SetMaster(true);
			}

			popup = GameObject.FindGameObjectWithTag(TagConstants.UI.POPUP_PENGUIN_REQUIRED).GetComponent<Image>();

			DisablePopup();

		    // init texts at the beginning
		    foreach (var marker in markers) {
		        marker.btnFromScene.GetComponentInChildren<Text>().text = TranslateApi.GetString(marker.localizedText);
		    }
		}

	    void Update() {
	        UpdateLevelPositions();
	    }
        /// <summary>
        /// On each update update level buttons so they are in the correct position
        /// </summary>
	    private void UpdateLevelPositions() {
	        foreach (var lvl in levels) {
	            lvl.btnFromScene.transform.position = Camera.main.WorldToScreenPoint(lvl.levelAnchor.transform.position);
	        }
	        foreach (var marker in markers) {
	            marker.btnFromScene.transform.position = Camera.main.WorldToScreenPoint(marker.btnAnchor.transform.position);
	        }
	    }

	    // Waits for level line to finish filling up and then changes the next available level to green
		IEnumerator WaitForFill() {
			yield return new WaitForSeconds(FillImage.fillAmountTime);
			for (int i = 0; i < levelNames.Length; i++) {
				if (Prefs.IsLevelStatusCurrent(levelNames[i] + Prefs.STATUS)) {
					levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnCurrent;
				}
			}
		}

		void UpdateLevelsStatusOnLoad() {
			for (int i = 0; i < levelNames.Length; i++) {

				// Find which level was won last time
				if (Prefs.GetLevelLastPlayedName() == levelNames[i] && EndGame.isLevelWon) {
					Prefs.SetLevelStatus(levelNames[i] + Prefs.STATUS, Prefs.COMPLETED);

					// Sets the next available level as the current level
					if (i + 1 < levelNames.Length) 
						Prefs.SetLevelStatus(levelNames[i + 1] + Prefs.STATUS, Prefs.CURRENT);
				}
			}

			// Interpret level status
			for (int i = 0; i < levelNames.Length; i++) {

				if (Prefs.IsLevelStatusComplete(levelNames[i] + Prefs.STATUS)) {
					levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnCompleted;
				}

				else {
					levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnLocked;
				}
			}
		}

		// Saves preferences for how many stars are collected for each level
		void SetStarPrefsPerLevel(string[] levelNames) {
			for (int i = 0; i < levelNames.Length; i++) {

				// Checks which level was won last time and sets the stars accordingly
				if (Prefs.GetLevelLastPlayedName() == levelNames[i]) {
					Prefs.SetLevelWonStars(levelNames[i], Prefs.GetLevelWonStars(levelNames[i]));
				}
			}
		}

		// Loads stars to view
		void LoadStars() {
			//int starsWonForLevel = 0;
			for (int i = 0; i < levelNames.Length; i++) {
				SetStarSprite(i, PlayerPrefs.GetInt(levelNames[i]));
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
				levels[i].btnFromScene.interactable = true;
			}
		}

		// Unlocks numOfLvlsToUnlock + 1 levels
		void UnlockLevels(int numOfLvlsToUnlock) {
		    MakeLevelsInteractable(numOfLvlsToUnlock);
//		    switch (numOfLvlsToUnlock) {
//				case 1:
//					MakeLevelsInteractable(1);
//					break;
//				case 2:
//					MakeLevelsInteractable(2);
//					break;
//				case 3:
//					MakeLevelsInteractable(3);
//					break;
//				case 4:
//					MakeLevelsInteractable(4);
//					break;
//				case 5:
//					MakeLevelsInteractable(5);
//					break;
//				case 6:
//					MakeLevelsInteractable(6);
//					break;
//				case 7:
//					MakeLevelsInteractable(7);
//					break;
//				case 8:
//					MakeLevelsInteractable(8);
//					break;
//				case 9:
//					MakeLevelsInteractable(9);
//					break;
//				case 10:
//					MakeLevelsInteractable(10);
//					break;
//				default:
//					break;
//			}
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
				print(lvl.sceneFileName);
				SceneManager.LoadSceneAsync(lvl.sceneFileName);
			} else {
				EnablePopup();
			}
		}

		public void OnLanguageChange(SupportedLanguage newLanguage) {
			//UpdateTexts();
		}

//		private void UpdateTexts() {
//			foreach(var lvl in levels) {
//				lvl.btnFromScene.GetComponentInChildren<Text>().text = TranslateApi.GetString(lvl.localizedText);
//			}
//			popup.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.popupNotEnoguhPenguins);
//		}

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

		[Serializable] public class LvlData {
		    [Tooltip("Name of the scene file that should be loaded. If project contains multiple levels with the same name, you have to specify the full path")]
			public string sceneFileName;
		    [Tooltip("Specify the button from the scene that will open the level")]
			public Button btnFromScene;
		    [Tooltip("How many penguins are needed to access the level")]
			public int penguinsRequired;
		    [Tooltip("Specify where in the world the \"Btn from scene\" should be placed")]
			public GameObject levelAnchor;
		}

	    [Serializable] public class LvlMarkers {
	        [Tooltip("Specify the key for text displayed here. All keys are defined in Resources/Translations. Text is already translated.")]
	        public LocalizedString localizedText;
	        [Tooltip("Specify the button from the scene that represents the anchor")]
	        public Button btnFromScene;
	        [Tooltip("Specify where in the world the \"Btn from scene\" should be placed")]
	        public GameObject btnAnchor;
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