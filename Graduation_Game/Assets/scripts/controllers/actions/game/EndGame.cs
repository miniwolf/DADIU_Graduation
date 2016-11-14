using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.game {
	class EndGame : Action {
		private GameObject gameObject;
		private GameObject endScene;
		private Text plutoniumCounter;
		private Text plutoniumTotal;
		private int target;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			foreach (Transform g in gameObject.GetComponentsInChildren<Transform>(true))
				if (g.tag == TagConstants.ENDSCENE)
					endScene = g.gameObject;
			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
			
		}

		public void Execute() {
			if (!endScene.active) {
				endScene.SetActive(true);
				plutoniumTotal = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_TOTAL).GetComponent<Text>();
				target = PlayerPrefs.GetInt("Plutonium") + int.Parse(plutoniumCounter.text);
				plutoniumTotal.text = PlayerPrefs.GetInt("Plutonium").ToString();
			}

			if (int.Parse(plutoniumTotal.text) < target) {
				if (!PlayerPrefs.HasKey("Plutonium")) {
					PlayerPrefs.SetInt("Plutonium", 0);
					PlayerPrefs.Save();
				}
				if (int.Parse(plutoniumTotal.text) < target) {
					plutoniumTotal.text = (int.Parse(plutoniumTotal.text) + 1).ToString();
				}

				if (int.Parse(plutoniumTotal.text) == target) {
					PlayerPrefs.SetInt("Plutonium", int.Parse(plutoniumTotal.text));
					PlayerPrefs.Save();
				}
			}
		}
	}
}
