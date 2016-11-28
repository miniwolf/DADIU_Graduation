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
		private CanvasController canvas;
		private GameObject endScene, gameObject;
		private Text plutoniumCounter, penguinCounter, plutoniumThisLevel, plutoniumTotal;
		private GameObject[] star = new GameObject[3];
		private bool isSetUp = false;
		private int starsSpawned = 0, target;
		private bool scoreDoneUpdated = false, starsDoneSpawned = false;
		private readonly CouroutineDelegateHandler handler;
		private SceneManager scenes;
		private static int collectedStars;
		private Actionable<GameActions> actionable;
		public static bool isLevelWon = false;
		private int totalPlutonium = 0, plutoniumThisLevelint = 0;
		private int endedWithPenguins = 0;
		private int[] requiredPenguins = new int[3];


		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			canvas = gameObject.GetComponent<CanvasController>();
			plutoniumTotal = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_TOTAL).GetComponent<Text>();
			endScene = GameObject.FindGameObjectWithTag(TagConstants.ENDSCENE);
			star[0] = GameObject.FindGameObjectWithTag(TagConstants.STAR1);
			star[1] = GameObject.FindGameObjectWithTag(TagConstants.STAR2);
			star[2] = GameObject.FindGameObjectWithTag(TagConstants.STAR3);
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
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
			SetupEndScene();
			handler.StartCoroutine(FlowScore());
			handler.StartCoroutine(SpawnStars());
		}

		private void SetupEndScene()
		{
		    AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.END_SCREEN_TRIGGER, Camera.main.gameObject);
			plutoniumThisLevel.GetComponent<Text>().text = plutoniumCounter.text;
			plutoniumThisLevelint = int.Parse(plutoniumCounter.text);

			endScene.SetActive(true);
			totalPlutonium = PlayerPrefs.GetInt("Plutonium");
			plutoniumTotal.GetComponent<Text>().text = totalPlutonium.ToString();

			target = totalPlutonium + int.Parse(plutoniumThisLevel.text);

			string levelPlayedName = SceneManager.GetActiveScene().name;
			PlayerPrefs.SetString("LevelPlayedName", levelPlayedName);
			isLevelWon = true;

			endedWithPenguins = int.Parse(penguinCounter.text);
			requiredPenguins = canvas.GetAmountOfPenguinsForStars();

			handler.StartCoroutine(FlowScore());
		}

		private IEnumerator FlowScore() {
			yield return new WaitForSeconds(canvas.timeBeforeScoreFlow); //Seems strange atm

			while (plutoniumThisLevelint > 0) {
				yield return new WaitForSeconds(GetTimeFromCurve());
			}

			PlayerPrefs.SetInt("Plutonium", totalPlutonium);
			handler.StartCoroutine(LoadMainMenu());
			yield return null;
		}

		private float GetTimeFromCurve() {
			float t = 1 / (0.015f * (canvas.scoreFlowScalingFactor * plutoniumThisLevelint));
			if (plutoniumThisLevelint > 100) {
				int portion = Mathf.RoundToInt(plutoniumThisLevelint / 50);
				plutoniumThisLevelint -= portion;
				plutoniumThisLevel.text = plutoniumThisLevelint.ToString();
				plutoniumCounter.text = plutoniumThisLevelint.ToString();
				UpdateScore(portion);
			}
			else {
				int portion = 1;
				plutoniumThisLevelint -= portion;
				plutoniumThisLevel.text = plutoniumThisLevelint.ToString();
				plutoniumCounter.text = plutoniumThisLevelint.ToString();
				UpdateScore(portion);
			}
			return t;
		}

		private IEnumerator SpawnStars() {
			yield return new WaitForSeconds(canvas.timeBeforeStarSpawn);//Seems strange atm
			while (SpawnNextStar()) {
				yield return new WaitForSeconds(canvas.timeBewteenStarSpawn);
			}

			actionable.ExecuteAction(GameActions.TriggerCutScene);
			PlayerPrefs.DeleteKey("hasVisited");
			starsDoneSpawned = true;
			SaveStars();
			yield return null;
		}

		private void SaveStars(){
			int totalStars = 0;
			if (PlayerPrefs.HasKey("TotalStars")) {
				totalStars = PlayerPrefs.GetInt("TotalStars");
			}
			if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name)) {
				PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, collectedStars);
				PlayerPrefs.SetInt("TotalStars", totalStars + collectedStars);
			}
			else {
				int starsThisLevel = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name);
				if (collectedStars > starsThisLevel) {
					PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, collectedStars);
					PlayerPrefs.SetInt("TotalStars", totalStars - starsThisLevel + collectedStars);
				}
			}
		}

		private void UpdateScore(float portion) {
			totalPlutonium += (int)portion;
			plutoniumTotal.text = totalPlutonium.ToString();
		}

		private IEnumerator LoadMainMenu(){
			yield return new WaitForSeconds(12);
			SceneManager.LoadSceneAsync("MainMenuScene");
		}

		public bool SpawnNextStar() {
			if (starsSpawned == 3 || endedWithPenguins < requiredPenguins[starsSpawned]) {
				return false;
			}

			star[starsSpawned].GetComponent<Star>().FlyIn();
			AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.END_SCREEN_SPAWN_STAR, Camera.main.gameObject);

			starsSpawned++;
			collectedStars += starsSpawned;

			return true;
		}
	}
}
