using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.scripts.UI.translations;
using Assets.scripts;


public class TranslateApi {
	private static readonly object syncLock = new object();
	private static SupportedLanguage languageLoaded;
	// todo if LocalizedString.Parse is slow, just change to plain strings and instead of translationLookupTable[key] call translationLookupTable[Key.toString()]
	private static Dictionary<LocalizedString, string> translationLookupTable = new Dictionary<LocalizedString, string>();
	private static TextAsset txtFile;
    private static List<LanguageChangeListener> listeners = new List<LanguageChangeListener>();

	public static string GetString(LocalizedString key) {
		lock(syncLock) {
			if (translationLookupTable.Count == 0) {
				languageLoaded = Prefs.IsEnglishOn() ? SupportedLanguage.ENG : SupportedLanguage.DEN;
				LoadLanguage(languageLoaded);
			}
		}

		return translationLookupTable[key];
	}

	public static void ChangeLanguage(SupportedLanguage newLanguage) {
		if (!languageLoaded.Equals(newLanguage)) {
			lock(syncLock) {
				LoadLanguage(newLanguage);
			}
		    NotifyListeners();
		}
	}

    private static void NotifyListeners() {
        foreach (var l in listeners) {
            l.OnLanguageChange(languageLoaded);
        }
    }

    public static void Register(LanguageChangeListener l) {
        listeners.Add(l);
    }

    public static void UnRegister(LanguageChangeListener l) {
        listeners.Remove(l);
    }

	private static void LoadLanguage(SupportedLanguage language)  {
		txtFile = (TextAsset)Resources.Load("Translations/" + language.ToString().ToLower(), typeof(TextAsset));

		string data = txtFile.text;
		string[] splits = data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None); // Environment.NewLine

		translationLookupTable.Clear();

		for (int i = 0; i < splits.Length; i++) {
			string split = splits[i];
			if ( split.Length < 1 ) {
				throw new EntryPointNotFoundException("Cannot load string on line " + i);
			}

			string[] keyValuePair = split.Split(';');
			LocalizedString key = (LocalizedString) Enum.Parse(typeof(LocalizedString), keyValuePair[0].Trim());
			string value = keyValuePair[1].Trim();

			translationLookupTable.Add(key, value);
		}
		languageLoaded = language;
	}

	public static SupportedLanguage GetCurrentLanguage() {
		return languageLoaded;
	}
}
