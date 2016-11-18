using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.UI;
using Assets.scripts.components;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.scripts.controllers.actions.game {
	class EndGame : Action {
		private GameObject gameObject;
		private CanvasController canvas;
		private GameObject endScene;
		private Text plutoniumCounter;
		private Text plutoniumTotal;
		private Text penguinCounter;
		private Image[] star = new Image[3];
		private int target;
		private int starsSpawned;
		private bool scoreUpdated = false;
		private readonly CouroutineDelegateHandler handler;
		private PlutoniumCounterController pcc;
		private SceneManager scenes;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			canvas = gameObject.GetComponent<CanvasController>();
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			foreach (Transform g in gameObject.GetComponentsInChildren<Transform>(true))
				if (g.tag == TagConstants.ENDSCENE) {
					endScene = g.gameObject;
					break;
				}
			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
			starsSpawned = 0;
		}


		public EndGame(CouroutineDelegateHandler handler) {
			this.handler = handler;
			
		}

		public void Execute() {
			if (!endScene.active) {
				SetupEndScene();
			}

			if (!scoreUpdated) {
				handler.StartCoroutine(UpdateScore());
			}
		}

		private void SetupEndScene() {
			endScene.SetActive(true);
			plutoniumCounter.transform.parent = endScene.GetComponentInChildren<Image>().transform;
			plutoniumCounter.alignment = TextAnchor.MiddleLeft;
			plutoniumCounter.transform.localPosition = new Vector3(-106, -79, 0);
			plutoniumTotal = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_TOTAL).GetComponent<Text>();
			star[0] = GameObject.FindGameObjectWithTag(TagConstants.STAR1).GetComponent<Image>();
			star[1] = GameObject.FindGameObjectWithTag(TagConstants.STAR2).GetComponent<Image>();
			star[2] = GameObject.FindGameObjectWithTag(TagConstants.STAR3).GetComponent<Image>();
			target = PlayerPrefs.GetInt("Plutonium") + int.Parse(plutoniumCounter.text);
			plutoniumTotal.text = PlayerPrefs.GetInt("Plutonium").ToString();
			pcc = plutoniumCounter.GetComponent<PlutoniumCounterController>();
			pcc.SetupFlowing();
		}

		private IEnumerator UpdateScore() {
			if (!PlayerPrefs.HasKey("Plutonium")) {
				PlayerPrefs.SetInt("Plutonium", 0);
				PlayerPrefs.Save();
			}
			while (pcc.FlowPlutonium()) {
				yield return new WaitForSeconds(0.1f);
			}
			while (target != int.Parse(plutoniumTotal.text)) {
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
			if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name)) {
				PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, starsSpawned);
				PlayerPrefs.SetInt("TotalStars", totalStars + starsSpawned);
				PlayerPrefs.Save();
			}
			else {
				int starsThisLevel = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name);
				if (totalStars > starsThisLevel) {
					PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, starsSpawned);
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
					if (int.Parse(penguinCounter.text) >= (int)canvas.GetType().GetField("penguinsRequiredFor" + (i + 1).ToString() + "Stars").GetValue(canvas)) {
						star[i].sprite = Resources.Load<Sprite>("Images/star");
						starsSpawned++;
						return true;
					}
				}
			}
			return false;
		}
	}
}
