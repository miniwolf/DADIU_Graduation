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
	class EndGameLoss : Action {
		private CanvasController canvas;
		private GameObject endScene, gameObject;
		private Text plutoniumCounter, penguinCounter, plutoniumThisLevel, plutoniumTotal;
		private GameObject[] star = new GameObject[3];
		private readonly CouroutineDelegateHandler handler;
		private SceneManager scenes;
		private Actionable<GameActions> actionable;
		public static bool isLevelWon = false;
		private int totalPlutonium = 0, plutoniumThisLevelint = 0;
		private int endedWithPenguins = 0;
		private int[] requiredPenguins = new int[3];


		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			canvas = gameObject.GetComponent<CanvasController>();
			endScene = canvas.failSceneObject;
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();

		}


		public EndGameLoss(CouroutineDelegateHandler handler, Actionable<GameActions> actionable) {
			this.handler = handler;
			this.actionable = actionable;
		}

		public void Execute() {
			EnableFail();
		}

		private void SetupEndScene()
		{
			AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.END_SCREEN_TRIGGER, Camera.main.gameObject);

			endScene.SetActive(true);

			string levelPlayedName = SceneManager.GetActiveScene().name;
			//PlayerPrefs.SetString("LevelPlayedName", levelPlayedName);

			Prefs.SetLevelLastPlayedName(levelPlayedName);

			isLevelWon = true;

			endedWithPenguins = int.Parse(penguinCounter.text);
			requiredPenguins = canvas.GetAmountOfPenguinsForStars();

			actionable.ExecuteAction(GameActions.FlowScore);
			handler.StartCoroutine(LoadMainMenu());
			actionable.ExecuteAction(GameActions.RetryButtonLoss);
		}

		void EnableFail(){
			canvas.SetActiveClickBlocker(true);
			canvas.failSceneObject.SetActive(true);
			handler.StartCoroutine(ShowFail());
		}

		private IEnumerator ShowFail(){
			Animator anim = canvas.failSceneObject.GetComponentInChildren<Animator>();
			anim.Play("PanelIn");
			yield return new WaitForSeconds(0.8f);
			canvas.SetActiveClickBlocker(false);
			SetupEndScene();
		}

		private IEnumerator LoadMainMenu(){
			yield return new WaitForSeconds(12);
			SceneManager.LoadSceneAsync("MainMenuScene");
		}


	}
}
