using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.translations {
    public class AutoTranslateTextField : MonoBehaviour, LanguageChangeListener {

        public LocalizedString textKey;

        void Start() {
            TranslateApi.Register(this);
            Refresh();
        }

        public void OnLanguageChange(SupportedLanguage newLanguage) {
            Refresh();
        }

        private void Refresh() {
            GetComponent<Text>().text = TranslateApi.GetString(textKey);
        }

        void OnDestroy() {
            TranslateApi.UnRegister(this);
        }
    }
}