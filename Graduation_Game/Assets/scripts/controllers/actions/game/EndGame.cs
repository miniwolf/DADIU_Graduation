using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.UI;
using Assets.scripts.components;
using System.Collections;

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
		private bool starsToSpawn;
		private float delay;
		private readonly CouroutineDelegateHandler handler;

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
			starsToSpawn = true;
			delay = 2f;
		}


		public EndGame(CouroutineDelegateHandler handler) {
			this.handler = handler;
			
		}

		public void Execute() {
			if (!endScene.active) {
				endScene.SetActive(true);
				plutoniumTotal = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_TOTAL).GetComponent<Text>();
				star[0] = GameObject.FindGameObjectWithTag(TagConstants.STAR1).GetComponent<Image>();
				star[1] = GameObject.FindGameObjectWithTag(TagConstants.STAR2).GetComponent<Image>();
				star[2] = GameObject.FindGameObjectWithTag(TagConstants.STAR3).GetComponent<Image>();
				target = PlayerPrefs.GetInt("Plutonium") + int.Parse(plutoniumCounter.text);
				plutoniumTotal.text = PlayerPrefs.GetInt("Plutonium").ToString();
			}

			if (int.Parse(plutoniumTotal.text) < target) {
				UpdateScore();
			}
			else if(starsToSpawn) {
				canvas.endLevelCalled = true;
				handler.StartCoroutine(StarSpawning());
			}
		}

		private IEnumerator StarSpawning() {
			while(starsToSpawn) {
					yield return new WaitForSeconds(0.6f);
					SpawnNextStar();
			}
		}

		public void SpawnNextStar() {
			for(int i=0; i<3; i++)
				if (starsSpawned == i) { 
					if (int.Parse(penguinCounter.text) >= (int)canvas.GetType().GetField("penguinsRequiredFor" + (i + 1).ToString() + "Stars").GetValue(canvas)) {
						star[i].sprite = Resources.Load<Sprite>("Images/star");
						starsSpawned++;
					}
					else {
						starsToSpawn = false;
					}
					return;
				}
		}
		public void UpdateScore() {
			if (!PlayerPrefs.HasKey("Plutonium")) {
				PlayerPrefs.SetInt("Plutonium", 0);
				PlayerPrefs.Save();
			}
			if (int.Parse(plutoniumTotal.text) < target) {
				plutoniumTotal.text = (int.Parse(plutoniumTotal.text) + 1).ToString();
				plutoniumCounter.text = (int.Parse(plutoniumCounter.text) - 1).ToString();
			}

			if (int.Parse(plutoniumTotal.text) == target) {
				PlayerPrefs.SetInt("Plutonium", int.Parse(plutoniumTotal.text));
				PlayerPrefs.Save();
			}
		}
	}
}
