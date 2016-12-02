using System;
using UnityEngine;
using Assets.scripts.UI.screen;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.scripts.UI.translations;
using Assets.scripts.sound;

namespace Assets.scripts.UI.credits {
	public class Credits : MonoBehaviour {
		private Button backSettingsButton;

		void Start() {
			gameObject.SetActive(false);	
		}

		public void CreditsButton() {
			gameObject.SetActive(true);	
		}

		public void BackSettingsButton() {
			gameObject.SetActive(false);
		}
	}
}

