using System;
using System.Xml.Schema;
using Assets.scripts.components.registers;
using Assets.scripts.UI.inventory;
using Assets.scripts.UI.screen;
using Assets.scripts.UI.translations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.level;

namespace Assets.scripts.UI.mainmenu {
	public class MainMenuScript : UIController, LanguageChangeListener, TouchInputListener, MouseInputListener {

		public LvlData[] levels;
		private Dropdown languageDropdown;
		private Image popup;
		private InputManager inputManager;

		protected void Start() {
			TranslateApi.Register(this);
			inputManager = FindObjectOfType<InputManagerImpl>(); // not registering in injection system yet
			inputManager.SubscribeForMouse(this);
			inputManager.SubscribeForTouch(this);

			// Every level except the first is locked from the start
			levels[1].btnFromScene.interactable = false;
			levels[2].btnFromScene.interactable = false;
			levels[3].btnFromScene.interactable = false;
			levels[4].btnFromScene.interactable = false;
			//levels[5].btnFromScene.interactable = false;

			// TODO in a loop this doesn't work, it doesn't load the correct level when you click a button
			levels[0].btnFromScene.onClick.AddListener(() => CheckLoadLevel(levels[0]));
			levels[1].btnFromScene.onClick.AddListener(() => CheckLoadLevel(levels[1]));
			levels[2].btnFromScene.onClick.AddListener(() => CheckLoadLevel(levels[2]));
			levels[3].btnFromScene.onClick.AddListener(() => CheckLoadLevel(levels[3]));
			levels[4].btnFromScene.onClick.AddListener(() => CheckLoadLevel(levels[4]));
		//	levels[5].btnFromScene.onClick.AddListener(() => CheckLoadLevel(levels[5]));

		   /* languageDropdown = GameObject.FindGameObjectWithTag(TagConstants.UI.DROPDOWN_CHANGE_LANGUAGE).GetComponent<Dropdown>();

			languageDropdown.onValueChanged.AddListener(delegate {
				OnDropdownChanged();
			});
*/
			popup = GameObject.FindGameObjectWithTag(TagConstants.UI.POPUP_PENGUIN_REQUIRED).GetComponent<Image>();

			DisablePopup();
			//UpdateTexts();
		}

		void Update() {
//			print(PlayerPrefs.GetInt("LevelUnlockIndex"));

			if(PlayerPrefs.GetInt("LevelUnlockIndex") == 1) {
				//print("UNLOCK LEVEL 1");
				levels[1].btnFromScene.interactable = true;
			}

			if (PlayerPrefs.GetInt("LevelUnlockIndex") == 2) {
				//print("UNLOCK LEVEL 2");
				levels[1].btnFromScene.interactable = true;
				levels[2].btnFromScene.interactable = true;
			}

			if (PlayerPrefs.GetInt("LevelUnlockIndex") == 3) {
				//print("UNLOCK LEVEL 3");
				levels[1].btnFromScene.interactable = true;
				levels[2].btnFromScene.interactable = true;
				levels[3].btnFromScene.interactable = true;
			}

			if (PlayerPrefs.GetInt("LevelUnlockIndex") == 4) {
				//print("UNLOCK LEVEL 4");
				levels[1].btnFromScene.interactable = true;
				levels[2].btnFromScene.interactable = true;
				levels[3].btnFromScene.interactable = true;
				levels[4].btnFromScene.interactable = true;
			}

			if (PlayerPrefs.GetInt("LevelUnlockIndex") == 5) {
				//print("UNLOCK LEVEL 5");
				levels[1].btnFromScene.interactable = true;
				levels[2].btnFromScene.interactable = true;
				levels[3].btnFromScene.interactable = true;
				levels[4].btnFromScene.interactable = true;
				levels[5].btnFromScene.interactable = true;
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
			if(popup.enabled) {
				DisablePopup();
				return;
			}

			if(Inventory.penguinCount.GetValue() >= lvl.penguinsRequired) {
				SceneManager.LoadScene(lvl.sceneFileName);
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
//	        popup.gameObject.SetActive(false);//enabled =  false);
//	        popup.GetComponent<Text>().enabled = false;

		}

		private void EnablePopup() {
			print(Inventory.penguinCount.GetValue());
		
			popup.transform.localScale = Vector3.one;
			popup.enabled = true;
//	        popup.gameObject.SetActive(true);
//	        popup.enabled =  true;
//	        popup.GetComponent<Text>().enabled = true;

		}

		public void OnTouch(Touch[] allTouches) {
		//	DisablePopup(); //apparently it will activate and get removed super fast
		}

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

		[Serializable] public struct LvlData {
			public string sceneFileName;
			public Button btnFromScene;
			public LocalizedString localizedText;
			public int penguinsRequired;
		}
	}
}