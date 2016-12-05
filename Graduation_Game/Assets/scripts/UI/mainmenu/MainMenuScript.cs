﻿using System;
using Assets.scripts.UI.inventory;
using Assets.scripts.UI.screen;
using Assets.scripts.UI.translations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.sound;
using Assets.scripts.controllers.actions.game;
using System.Collections;
using Assets.scripts.UI.mainmenu;

namespace Assets.scripts.UI.mainmenu {
	public class MainMenuScript : UIController, TouchInputListener, MouseInputListener {

		public LvlData[] levels;
		public LvlUnlockMarkers[] worldUnlockMarkers;
		private Dropdown languageDropdown;
		private Image popup;
		private InputManager inputManager;
		private FillImage fillImageScript;

		public int firstLvlIdxInNextWorld = 5;

		public Sprite stars1;
		public Sprite stars2;
		public Sprite stars3;

		public Sprite levelBtnCurrent;
		public Sprite levelBtnCompleted;
		public Sprite levelBtnLocked;
		public Sprite levelBtnNotAccessable; // Coming soon...


		// TODO this needs heavy refactoring when everything is working as intended!
		protected void Start() {

			inputManager = FindObjectOfType<InputManagerImpl>(); // not registering in injection system yet
			inputManager.SubscribeForMouse(this);
			inputManager.SubscribeForTouch(this);

			string lastLevelName = Prefs.GetLevelLastPlayedName();

			InitilizeLevels();
			UnlockLevels(Prefs.GetLevelUnlockIndex());

			// Checks if the newest available level has been beat
			if (EndGame.isNewLevelWon) {
				fillImageScript = GetComponent<FillImage>();
				SetStarPrefsPerLevel();
				if (isLastLevelIdx()) LoadLevelButtonsStatusColors();
				else StartCoroutine(WaitForFill());

			} else {
				// LOAD latest progression
				LoadLevelButtonsStatusColors();
			}
			LoadStars();

			// first time we set up the language as English, tooltips and music on 
			if (!PlayerPrefs.HasKey("NoIntroScreen")) {
				Prefs.SetTooltips(1);
				Prefs.SetLanguage(0);
				Prefs.SetMaster(true);
			}

			popup = GameObject.FindGameObjectWithTag(TagConstants.UI.POPUP_PENGUIN_REQUIRED).GetComponent<Image>();
			DisablePopup();
		}

	    // Waits for level line to finish filling up and then changes the next available level to green
	    void Update() {
			UpdateWorldStarCounterText();


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

		void UpdateWorldStarCounterText() {
			// init texts at the beginning
			foreach (var marker in worldUnlockMarkers) {
				marker.btnFromScene.GetComponentInChildren<Text>().text = 
					TranslateApi.GetString(marker.localizedText) + " " + StarsCollectedCountText.totalStars + "/" + marker.starsNeeded; // maxstars
			}
		}

		void LoadLevelButtonsStatusColors() {
			bool currentLvl = false;

			for (int i = levels.Length - 1; i >= 0; i--) {
				string levelName = levels[i].sceneFileName;
				if (Prefs.IsLevelStatusComplete(levelName)) {
					levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnCompleted;
					if (!currentLvl && i < levels.Length - 1) {
						levels[i + 1].btnFromScene.GetComponent<Image>().sprite = levelBtnCurrent;
						currentLvl = true; // Current level has been set
					}
				}
			}
		}


		// Waits for level line to finish filling up and then changes the next available level to green
		IEnumerator WaitForFill() {
			bool tmpCurrent = true;
			for (int i = levels.Length - 1; i >= 0; i--) {
				string levelName = levels[i].sceneFileName;
				if (Prefs.IsLevelStatusComplete(levelName)) {
					if(tmpCurrent) {
						// Stores the level won last time temporarily as the current level 
						// while waiting for the 
						levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnCurrent;
						tmpCurrent = false;
					} else {
						levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnCompleted;
					}
					
				}
			}
			yield return new WaitForSeconds(fillImageScript.GetFillAmountTime());

			UpdateButtonColors();
		}


		void UpdateButtonColors() {
			for (int i = levels.Length - 1; i >= 0; i--) {
				string levelName = levels[i].sceneFileName;

				if (Prefs.IsLevelStatusComplete(levelName)) {
					levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnCompleted;
					levels[i + 1].btnFromScene.GetComponent<Image>().sprite = levelBtnCurrent;
					break;
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

			LockNonInteractableLevels();

			// Makes the next world accessible if totalStars >= starsNeeded
			foreach (var marker in worldUnlockMarkers) {
				if (StarsCollectedCountText.totalStars < marker.starsNeeded) {
					MakeLevelsNotAccessible(firstLvlIdxInNextWorld);
				}
			}
		}

		void MakeLevelsNotAccessible(int fromLevelIdx) {
			for(int i = fromLevelIdx; i < levels.Length; i++) {
				//levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnNotAccessible; // Use levelBtnNotAccessible when available
				levels[i].btnFromScene.GetComponent<Image>().sprite = levelBtnCurrent; // GREEN FOR TESTING ONRY
			}
		}

		// Buttons that are not interactable are marked as locked (gray)
		void LockNonInteractableLevels() {
			foreach(var lvlData in levels) {
				var c = lvlData;
				if(!c.btnFromScene.IsInteractable()) {
					lvlData.btnFromScene.GetComponent<Image>().sprite = levelBtnLocked;
				}
			}
		}

		private bool isLastLevelIdx() {
			if (levels.Length - 1 < Prefs.GetLevelUnlockIndex()) return true;
			return false;
		}

		// Unlocks numOfLvlsToUnlock + 1 levels
		void UnlockLevels(int numOfLvlsToUnlock) {
			if (isLastLevelIdx()) return;

			for (int i = 0; i < numOfLvlsToUnlock + 1; i++) {
				levels[i].btnFromScene.interactable = true;
			}
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
			SceneManager.LoadScene(PrefsConstants.SETTINGS);	
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