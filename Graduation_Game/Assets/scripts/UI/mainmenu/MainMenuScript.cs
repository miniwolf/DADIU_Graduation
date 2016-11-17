using System;
using System.Xml.Schema;
using Assets.scripts.UI.screen;
using Assets.scripts.UI.translations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.scripts.UI.mainmenu {
	public class MainMenuScript : UIController, LanguageChangeListener {

	    public LvlData[] levels;
	    private Dropdown languageDropdown;

	    protected void Start() {
	        TranslateApi.Register(this);
	        foreach (var lvl in levels)	        {
	            lvl.btnFromScene.onClick.AddListener(() => LoadLevel(lvl.sceneFileName));
	        }

	        languageDropdown = GameObject.FindGameObjectWithTag(TagConstants.UI.DROPDOWN_CHANGE_LANGUAGE).GetComponent<Dropdown>();
            languageDropdown.onValueChanged.AddListener(delegate {
                OnDropdownChanged();
            });

	        UpdateTexts();
		}

	    void OnDestroy() {
	        TranslateApi.UnRegister(this);
	    }

	    private void OnDropdownChanged() {
	        SupportedLanguage newLanguage = ResolveLangauge();
            TranslateApi.ChangeLanguage(newLanguage);
	    }

	    private SupportedLanguage ResolveLangauge() {
	        return languageDropdown.value == 0 ? SupportedLanguage.ENG : SupportedLanguage.DEN;
	    }

	    private void LoadLevel(string sceneName) {
			SceneManager.LoadScene(sceneName);
		}

	    public void OnLanguageChange(SupportedLanguage newLanguage)	{
	        UpdateTexts();
	    }

	    private void UpdateTexts() {

	        foreach (var lvl in levels)
	        {
	            lvl.btnFromScene.GetComponentInChildren<Text>().text = TranslateApi.GetString(lvl.localizedText);
	        }
	    }

	    [Serializable] public struct LvlData {
	        public string sceneFileName;
	        public Button btnFromScene;
	        public LocalizedString localizedText;
	    }
	}
}