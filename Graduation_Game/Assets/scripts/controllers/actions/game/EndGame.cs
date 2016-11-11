using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.game {
	class EndGame : Action {
		private GameObject gameObject;
		private GameObject endScene;
		private Text plutoniumCounter;
		private Text plutoniumTotal;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			foreach (Transform g in gameObject.GetComponentsInChildren<Transform>(true))
				if (g.tag == TagConstants.ENDSCENE)
					endScene = g.gameObject;
			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
		}

		public void Execute() {
			endScene.SetActive(true);
			if (!PlayerPrefs.HasKey("Plutonium")) {
				PlayerPrefs.SetInt("Plutonium", 0);
				PlayerPrefs.Save();
			}
			plutoniumTotal = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_TOTAL).GetComponent<Text>();
			int old = PlayerPrefs.GetInt("Plutonium");
			plutoniumTotal.text = (old + int.Parse(plutoniumCounter.text)).ToString();
			PlayerPrefs.SetInt("Plutonium", old + int.Parse(plutoniumCounter.text));
			PlayerPrefs.Save();
		}
	}
}
