using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.UI;
using Assets.scripts.components;
using System.Collections;
using Assets.scripts.sound;
using Assets.scripts.tools;
using UnityEngine.SceneManagement;

namespace Assets.scripts.controllers.actions.game {
	class EndGame : Action {
		private GameObject gameObject;
		private CanvasController canvas;
		private GameObject endScene;
		private Text plutoniumCounter;
		private Text plutoniumThisLevel;
		private Text plutoniumTotal;
		private GameObject penguinCounter;
		private GameObject[] star = new GameObject[3];
		private bool isSetUp = false;
		private int target;
		private int starsSpawned;
		private bool scoreDoneUpdated = false;
		private bool starsDoneSpawned = false;
		private readonly CouroutineDelegateHandler handler;
		private SceneManager scenes;
		private static int collectedStars;
		private Actionable<GameActions> actionable;


		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			canvas = gameObject.GetComponent<CanvasController>();
			plutoniumTotal = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_TOTAL).GetComponent<Text>();
			endScene = GameObject.FindGameObjectWithTag(TagConstants.ENDSCENE);
			star[0] = GameObject.FindGameObjectWithTag(TagConstants.STAR1);
			star[1] = GameObject.FindGameObjectWithTag(TagConstants.STAR2);
			star[2] = GameObject.FindGameObjectWithTag(TagConstants.STAR3);
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT);
			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
			plutoniumThisLevel = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_THIS_LEVEL).GetComponent<Text>();
			GameObject.FindGameObjectWithTag(TagConstants.ENDSCENE).SetActive(false);
			starsSpawned = 0;
		}


		public EndGame(CouroutineDelegateHandler handler, Actionable<GameActions> actionable) {
			this.handler = handler;
			this.actionable = actionable;
		}

		public void Execute() {
			
			if (!isSetUp) {
				SetupEndScene();
			}
			if (!scoreDoneUpdated) {
				handler.StartCoroutine(FlowScore());
			}
			if (!starsDoneSpawned) {
				handler.StartCoroutine(SpawnStars());
			}

		}

		private void SetupEndScene()
		{
		    AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.END_SCREEN_TRIGGER, Camera.main.gameObject);
			plutoniumThisLevel.GetComponent<Text>().text = plutoniumCounter.text;;

			endScene.SetActive(true);
			plutoniumTotal.GetComponent<Text>().text = PlayerPrefs.GetInt("Plutonium").ToString();

			target = PlayerPrefs.GetInt("Plutonium") + int.Parse(plutoniumThisLevel.text);
			isSetUp = true;
		}

		private IEnumerator FlowScore() {
			yield return new WaitForSeconds(canvas.timeBeforeScoreFlow);
			if (!PlayerPrefs.HasKey("Plutonium")) {
				PlayerPrefs.SetInt("Plutonium", 0);
				PlayerPrefs.Save();
			}
			
			Debug.Log(plutoniumThisLevel.text);
			while (int.Parse(plutoniumThisLevel.text) > 0) {
				yield return new WaitForSeconds(GetTimeFromCurve());
			}

			PlayerPrefs.SetInt("Plutonium", target);
			PlayerPrefs.Save();
			handler.StartCoroutine(LoadMainMenu());
			yield return null;
		}

		private float GetTimeFromCurve() {
			float t = 1 / (0.015f * (canvas.scoreFlowScalingFactor * int.Parse(plutoniumThisLevel.text)));
			if (int.Parse(plutoniumThisLevel.text) > 100) {
				plutoniumThisLevel.text = (int.Parse(plutoniumThisLevel.text) - Mathf.Round(int.Parse(plutoniumThisLevel.text) / 50)).ToString();
				UpdateScore(Mathf.Round(int.Parse(plutoniumThisLevel.text) / 50));
			}
			else {
				plutoniumThisLevel.text = (int.Parse(plutoniumThisLevel.text)-1).ToString();
				UpdateScore(1);
			}
			return t;
		}

		private IEnumerator SpawnStars() {
			yield return new WaitForSeconds(canvas.timeBeforeStarSpawn);
			while (SpawnNextStar()) {
				yield return new WaitForSeconds(canvas.timeBewteenStarSpawn);
			}

			Debug.Log(PlayerPrefs.GetInt("TotalStars"));
			actionable.ExecuteAction(GameActions.TriggerCutScene);
			PlayerPrefs.DeleteKey("hasVisited");
			starsDoneSpawned = true;
			yield return null;
		}

		private void SaveStars(){
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
		}

		private void UpdateScore(float portion) {
			plutoniumTotal.text = (int.Parse(plutoniumTotal.text) + portion).ToString();
		}

		private IEnumerator LoadMainMenu(){
			yield return new WaitForSeconds(12);
			SceneManager.LoadSceneAsync("MainMenuScene");
		}

		public bool SpawnNextStar() {
			for (int i = 0; i < 3; i++) {
				if (starsSpawned == i) {
					if (int.Parse(penguinCounter.GetComponent<Text>().text) >= (int)canvas.GetType().GetField("penguinsRequiredFor" + (i + 1).ToString() + "Stars").GetValue(canvas)) {
						star[i].GetComponent<Star>().FlyIn();
						starsSpawned++;
					    AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.END_SCREEN_SPAWN_STAR, Camera.main.gameObject);
					    return true;
					}
				}
			}
			collectedStars += starsSpawned;
			PlayerPrefs.SetInt("CollectedStars", collectedStars);
			return false;
		}
	}
}
