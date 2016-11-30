using System;
using Assets.scripts.UI.inventory;
using Assets.scripts.UI.screen;
using Assets.scripts.UI.translations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.sound;
using Assets.scripts.controllers.actions.game;
using System.Collections;

namespace Assets.scripts.UI.mainmenu {
	public class MainMenuScript : UIController, TouchInputListener, MouseInputListener {

		public LvlData[] levels;
		public LvlUnlockMarkers[] worldUnlockMarkers;
		private Dropdown languageDropdown;
		private Image popup;
		private InputManager inputManager;
		private FillImage fillImageScript;

		public Sprite stars1;
		public Sprite stars2;
		public Sprite stars3;

		public Sprite levelBtnCurrent;
		public Sprite levelBtnCompleted;
		public Sprite levelBtnLocked;
		public Sprite levelBtnNotAccessable; // Coming soon...

		protected void Awake() {
			if(!Prefs.IsLevelStatusComplete(Prefs.LEVEL1)) {
				Prefs.SetLevelStatus(Prefs.LEVEL1, Prefs.CURRENT); // Level 1 is by default the current level
			}
		}

		protected void Start() {
			inputManager = FindObjectOfType<InputManagerImpl>(); // not registering in injection system yet
			inputManager.SubscribeForMouse(this);
			inputManager.SubscribeForTouch(this);

			fillImageScript = GetComponent<FillImage>();

		    InitilizeLevels();
			UnlockLevels(Prefs.GetLevelUnlockIndex());
			SetStarPrefsPerLevel();
			LoadStars();
			UpdateLevelsStatusOnLoad();

			if(Prefs.IsLevelStatusCurrent(Prefs.LEVEL1)) {
				levels[0].btnFromScene.GetComponent<Image>().sprite = levelBtnCurrent;
			} else {
				StartCoroutine(WaitForFill());
			}

			// first time we set up the language as English, tooltips and music on 
			if (!PlayerPrefs.HasKey("NoIntroScreen")) {
				Prefs.SetTooltips(1);
				Prefs.SetLanguage(0);
				Prefs.SetMaster(true);
			}

			popup = GameObject.FindGameObjectWithTag(TagConstants.UI.POPUP_PENGUIN_REQUIRED).GetComponent<Image>();

			DisablePopup();

		    // init texts at the beginning
		    foreach (var marker in worldUnlockMarkers) {
		        marker.btnFromScene.GetComponentInChildren<Text>().text = TranslateApi.GetString(marker.localizedText) + " " +   Prefs.GetTotalStars() + "/" + marker.starsNeeded; // maxstars
		    }
		}

	    // Waits for level line to finish filling up and then changes the next available level to green
	    void Update() {
	        if (Input.GetMouseButtonDown(0)) {
	            DisablePopup();
	        }
	        UpdateLevelPositions();
	    }
        /// <summary>
        /// On each update update level buttons so they are in the correct position
        /// </summary>
	    private void UpdateLevelPositions() {
	        foreach (var lvl in levels) {
	            lvl.btnFromScene.transform.position = Camera.main.WorldToScreenPoint(lvl.levelAnchor.transform.position);
	        }
	        foreach (var marker in worldUnlockMarkers) {
	            marker.btnFromScene.transform.position = Camera.main.WorldToScreenPoint(marker.btnAnchor.transform.position);
	        }
	    }


	    // Waits for level line to finish filling up and then changes the next available level to green
	    IEnumerator WaitForFill() {
			yield return new WaitForSeconds(FillImage.fillAmountTime);
			for (int i = 0; i < levels.Length; i++) {
				if (Prefs.IsLevelStatusCurrent(levels[i].sceneFileName)) {
					levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnCurrent;
				}
			}
		}

		void UpdateLevelsStatusOnLoad() {
			for (int i = 0; i < levels.Length; i++) {
				if (Prefs.GetLevelLastPlayedName() == levels[i].sceneFileName && EndGame.isLevelWon) { // Find which level was won last time
					Prefs.SetLevelStatus(levels[i].sceneFileName, Prefs.COMPLETED);
					
					if (i + 1 < levels.Length)
						Prefs.SetLevelStatus(levels[i + 1].sceneFileName, Prefs.CURRENT); // Sets the next available level as the current level
				}
			}

			// Interpret level status
			for (int i = 0; i < levels.Length; i++) {
				if (Prefs.IsLevelStatusComplete(levels[i].sceneFileName)) {
					levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnCompleted;
				}

				else {
					levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnLocked;
				}
			}
		}

		// Saves preferences for how many stars are collected for each level
		void SetStarPrefsPerLevel() {
			for (int i = 0; i < levels.Length; i++) {
				// Checks which level was won last time and sets the stars accordingly
				if (Prefs.GetLevelLastPlayedName() == levels[i].sceneFileName) {
					Prefs.SetLevelWonStars(levels[i].sceneFileName, Prefs.GetLevelWonStars(levels[i].sceneFileName));
				}
			}
		}

		// Loads stars to view
		void LoadStars() {
			for (int i = 0; i < levels.Length; i++) {
				SetStarSprite(i, Prefs.GetLevelWonStars(levels[i].sceneFileName));
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
			for (int i = 0; i < numOfLvlsToUnlock + 1; i++) {
				levels[i].btnFromScene.interactable = true;
			}
		}

		// Unlocks numOfLvlsToUnlock + 1 levels
		void UnlockLevels(int numOfLvlsToUnlock) {
		    MakeLevelsInteractable(numOfLvlsToUnlock);
		}

		void OnDestroy() {
			inputManager.UnsubscribeForMouse(this);
			inputManager.UnsubscribeForTouch(this);
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

	    [Serializable] public class LvlUnlockMarkers {
	        [Tooltip("Specify the key for text displayed here. All keys are defined in Resources/Translations. Text is already translated.")]
	        public LocalizedString localizedText;
	        [Tooltip("Specify the button from the scene that represents the anchor")]
	        public Button btnFromScene;
	        [Tooltip("Specify where in the world the \"Btn from scene\" should be placed")]
	        public GameObject btnAnchor;
	        [Tooltip("Specify how many stars player needs to have to unlock the world")]
	        public int starsNeeded;
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