using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.UI;
using Assets.scripts.components;
using System.Collections;
using UnityEditor;
using Assets.scripts.tools;

namespace Assets.scripts.controllers.actions.game {
	class EndGame : Action {
		private GameObject gameObject;
		private CanvasController canvas;
		private GameObject endScene;
		private Text plutoniumCounter;
		private GameObject plutoniumTotal;
		private GameObject penguinCounter;
		private GameObject[] star = new GameObject[3];
		private bool isSetUp = false;
		private int target;
		private int starsSpawned;
		private bool scoreUpdated = false;
		private readonly CouroutineDelegateHandler handler;
		private PlutoniumCounterController pcc;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			canvas = gameObject.GetComponent<CanvasController>();
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT);
			plutoniumTotal = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_TOTAL);
			endScene = GameObject.FindGameObjectWithTag(TagConstants.ENDSCENE);
			star[0] = GameObject.FindGameObjectWithTag(TagConstants.STAR1);
			star[1] = GameObject.FindGameObjectWithTag(TagConstants.STAR2);
			star[2] = GameObject.FindGameObjectWithTag(TagConstants.STAR3);

			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
			starsSpawned = 0;
		}


		public EndGame(CouroutineDelegateHandler handler) {
			this.handler = handler;
			
		}

		public void Execute() {
			if (!isSetUp) {
				SetupEndScene();
			}

			if (!scoreUpdated) {
				handler.StartCoroutine(UpdateScore());
			}
		}

		private void SetupEndScene() {
			plutoniumCounter.transform.parent = endScene.GetComponentInChildren<Image>().transform;
			plutoniumCounter.alignment = TextAnchor.MiddleLeft;
			plutoniumCounter.transform.localPosition = new Vector3(-106, -79, 0);

			endScene.GetComponent<Image>().enabled = true;
			plutoniumTotal.GetComponent<Text>().enabled = true;
			plutoniumTotal.GetComponent<Text>().text = PlayerPrefs.GetInt("Plutonium").ToString();

			target = PlayerPrefs.GetInt("Plutonium") + int.Parse(plutoniumCounter.text);
			pcc = plutoniumCounter.GetComponent<PlutoniumCounterController>();
			pcc.SetupFlowing();
			isSetUp = true;
		}

		private IEnumerator UpdateScore() {
			if (!PlayerPrefs.HasKey("Plutonium")) {
				PlayerPrefs.SetInt("Plutonium", 0);
				PlayerPrefs.Save();
			}
			while (pcc.FlowPlutonium()) {
				yield return new WaitForSeconds(0.1f);
			}
			while (target != int.Parse(plutoniumTotal.GetComponent<Text>().text)) {
				yield return new WaitForSeconds(0.5f);
			}

			PlayerPrefs.SetInt("Plutonium", target);
			PlayerPrefs.Save();

			while (SpawnNextStar()) {
				yield return new WaitForSeconds(1f);
			}
			int totalStars = 0;
			if (PlayerPrefs.HasKey("TotalStars")) {
				totalStars = PlayerPrefs.GetInt("TotalStars");
			}
			if (!PlayerPrefs.HasKey(EditorApplication.currentScene)) {
				PlayerPrefs.SetInt(EditorApplication.currentScene, starsSpawned);
				PlayerPrefs.SetInt("TotalStars", totalStars + starsSpawned);
				PlayerPrefs.Save();
			}
			else {
				int starsThisLevel = PlayerPrefs.GetInt(EditorApplication.currentScene);
				if (totalStars > starsThisLevel) {
					PlayerPrefs.SetInt(EditorApplication.currentScene, starsSpawned);
					PlayerPrefs.SetInt("TotalStars", totalStars - starsThisLevel + starsSpawned);
					PlayerPrefs.Save();
				}
			}
			Debug.Log(PlayerPrefs.GetInt("TotalStars"));
			yield return null;
		}
		
		public bool SpawnNextStar() {
			for (int i = 0; i < 3; i++) {
				if (starsSpawned == i) {
					if (int.Parse(penguinCounter.GetComponent<Text>().text) >= (int)canvas.GetType().GetField("penguinsRequiredFor" + (i + 1).ToString() + "Stars").GetValue(canvas)) {
						star[i].GetComponent<Star>().FlyIn();
						starsSpawned++;
						return true;
					}
				}
			}
			return false;
		}
	}
}
