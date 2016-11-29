using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.UI;
using Assets.scripts.components;
using System.Collections;
using Assets.scripts.sound;
using Assets.scripts.tools;
using UnityEngine.SceneManagement;
using Assets.scripts;
using Assets.scripts.UI.inventory;

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
		private static int collectedStars = 0;
		private Actionable<GameActions> actionable;
		public static bool isLevelWon = false;
		private int totalPlutonium = 0, plutoniumThisLevelint = 0;
		private int endedWithPenguins = 0;
		private int[] requiredPenguins = new int[3];


		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			canvas = gameObject.GetComponent<CanvasController>();
			endScene = GameObject.FindGameObjectWithTag(TagConstants.ENDSCENE);
			star[0] = GameObject.FindGameObjectWithTag(TagConstants.STAR1);
			star[1] = GameObject.FindGameObjectWithTag(TagConstants.STAR2);
			star[2] = GameObject.FindGameObjectWithTag(TagConstants.STAR3);
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			GameObject.FindGameObjectWithTag(TagConstants.ENDSCENE).SetActive(false);
			starsSpawned = 0;
		}


		public EndGame(CouroutineDelegateHandler handler, Actionable<GameActions> actionable) {
			this.handler = handler;
			this.actionable = actionable;
		}

		public void Execute() {
			SetupEndScene();
			handler.StartCoroutine(SpawnStars());
		}

		private void SetupEndScene()
		{
		    AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.END_SCREEN_TRIGGER, Camera.main.gameObject);

			endScene.SetActive(true);


			string levelPlayedName = SceneManager.GetActiveScene().name;
			//PlayerPrefs.SetString("LevelPlayedName", levelPlayedName);

			Prefs.SetLevelLastPlayedName(levelPlayedName);

			isLevelWon = true;

			Prefs.SetLevelWonStars(levelPlayedName, starsSpawned);

			endedWithPenguins = int.Parse(penguinCounter.text);
			requiredPenguins = canvas.GetAmountOfPenguinsForStars();

			actionable.ExecuteAction(GameActions.FlowScore);
			handler.StartCoroutine(LoadMainMenu());
		}




		private IEnumerator SpawnStars() {
			yield return new WaitForSeconds(canvas.timeBeforeStarSpawn);//Seems strange atm
			while (SpawnNextStar()) {
				yield return new WaitForSeconds(canvas.timeBewteenStarSpawn);
			}

			actionable.ExecuteAction(GameActions.TriggerCutScene);
			PlayerPrefs.DeleteKey("hasVisited");
			starsDoneSpawned = true;

			yield return null;
		}

		private void SaveStars(){
			int totalStars = 0;
			if (PlayerPrefs.HasKey("TotalStars")) {
				totalStars = Prefs.GetTotalStars();
			}
			if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name)) {
				PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, collectedStars);
			    Prefs.UpdateTotalStars(totalStars + collectedStars);
			}
			else {
				int starsThisLevel = Prefs.GetStarsForCurrentLevel();
				if (collectedStars > starsThisLevel) {
					PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, collectedStars);
				    Prefs.UpdateTotalStars(totalStars - starsThisLevel + collectedStars);
				}
			}
		}

		private IEnumerator LoadMainMenu(){
			yield return new WaitForSeconds(12);
			SaveStars();
			SceneManager.LoadSceneAsync("MainMenuScene");
		}

		public bool SpawnNextStar() {
			if (starsSpawned > 2 || endedWithPenguins < requiredPenguins[starsSpawned]) {
				Inventory.UpdateCount();
				return false;
			}

			star[starsSpawned].GetComponent<Star>().FlyIn();
			AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.END_SCREEN_SPAWN_STAR, Camera.main.gameObject);

			starsSpawned++;
			collectedStars += 1;

			return true;
		}
	}
}
