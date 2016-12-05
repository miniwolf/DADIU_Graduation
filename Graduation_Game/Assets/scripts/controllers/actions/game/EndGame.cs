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
using System.Collections.Generic;
using Assets.scripts.level;
using System;

namespace Assets.scripts.controllers.actions.game {
	class EndGame : Action {
		private CanvasController canvas;
		private GameObject endScene, gameObject;
		private Text plutoniumCounter, penguinCounter, plutoniumThisLevel, plutoniumTotal;
		private GameObject[] star = new GameObject[3];
		private bool isSetUp = false;
		private int starsSpawned = 0;
		private bool scoreDoneUpdated = false, starsDoneSpawned = false;
		private readonly CouroutineDelegateHandler handler;
		private SceneManager scenes;
		private Actionable<GameActions> actionable;
		private int totalPlutonium = 0, plutoniumThisLevelint = 0;
		private int endedWithPenguins = 0, reqPenguins = 0;
		private int[] requiredPenguins = new int[3];
		public static bool isNewLevelWon = false;

		private bool shouldShowRetry = true;
		private GameObject[] penguinIcons;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			canvas = gameObject.GetComponent<CanvasController>();
			endScene = GameObject.FindGameObjectWithTag(TagConstants.ENDSCENE);
			star[0] = GameObject.FindGameObjectWithTag(TagConstants.STAR1);
			star[1] = GameObject.FindGameObjectWithTag(TagConstants.STAR2);
			star[2] = GameObject.FindGameObjectWithTag(TagConstants.STAR3);
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			penguinIcons = GameObject.FindGameObjectsWithTag(TagConstants.UI.PENGUINICON);
			Array.Sort(penguinIcons, CompareObNames);
			isNewLevelWon = false;
			starsSpawned = 0;
			reqPenguins = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_SPAWNER).GetComponent<PenguinSpawner>().GetInitialPenguinCount();
		}

		public EndGame(CouroutineDelegateHandler handler, Actionable<GameActions> actionable) {
			this.handler = handler;
			this.actionable = actionable;
		}

		private int CompareObNames(GameObject x, GameObject y) {
			return x.name.CompareTo(y.name);
		}

		public void Execute() {
			endedWithPenguins = int.Parse(penguinCounter.text);
			requiredPenguins = canvas.GetAmountOfPenguinsForStars();

			if (endedWithPenguins >= reqPenguins) {
				actionable.ExecuteAction(GameActions.DisableRetryWin);
				shouldShowRetry = false;
			}
			PenguinIcons();
			EnableWin();
		}

		private void SetupEndScene() {

			Prefs.SetLevelLastPlayedName(SceneManager.GetActiveScene().name);

			if (shouldShowRetry) {
				actionable.ExecuteAction(GameActions.RetryButtonWin);
			}

			AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.END_SCREEN_TRIGGER, Camera.main.gameObject);
			endScene.SetActive(true);
			endedWithPenguins = int.Parse(penguinCounter.text);
			requiredPenguins = canvas.GetAmountOfPenguinsForStars();

			actionable.ExecuteAction(GameActions.FlowScore);
			handler.StartCoroutine(LoadMainMenu());
			handler.StartCoroutine(SpawnStars());
		}


		private void EnableWin(){
			string currentLevel = SceneManager.GetActiveScene().name;

			// Set prefs only if the level has not been beat before
			if (!Prefs.IsLevelStatusComplete(currentLevel)) {
				isNewLevelWon = true;
				Prefs.SetCurrentLevelToWon();
				Prefs.SetLevelStatus(currentLevel, Prefs.COMPLETED);
			}

			canvas.SetActiveClickBlocker(true);
			//canvas.failSceneObject.SetActive(true);
			handler.StartCoroutine(ShowWin());
		}

		private IEnumerator ShowWin(){
			Animator anim = canvas.endSceneObject.GetComponentInChildren<Animator>();
			anim.Play("PanelIn");
			yield return new WaitForSeconds(1f);
			canvas.SetActiveClickBlocker(false);
			SetupEndScene();
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

		private void GetAllStars() {
			
		}

		private IEnumerator LoadMainMenu(){
			yield return new WaitForSeconds(12);
			SceneManager.LoadSceneAsync("MainMenuScene");
		}

		public bool SpawnNextStar() {
			if (starsSpawned > 2 || endedWithPenguins < requiredPenguins[starsSpawned]) {
				//Inventory.UpdateCount();
				return false;
			}

			star[starsSpawned].GetComponent<Star>().FlyIn();
			AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.END_SCREEN_SPAWN_STAR, Camera.main.gameObject);
			starsSpawned++;
			if(Prefs.GetStarsForCurrentLevel() < starsSpawned) Prefs.SetStarsForCurrentLevel(starsSpawned);

			return true;
		}

		private void PenguinIcons(){
			DeactivatePenguinIcons();
			for (int i = 0; i < reqPenguins; i++) {
				if (endedWithPenguins > i) {
				} else {
					penguinIcons[i].GetComponent<Image>().sprite = canvas.penguinIsDead;
				}
			}
		}

		private void DeactivatePenguinIcons(){
			for (int i = 0; i < penguinIcons.Length; i++) {
				penguinIcons[i].SetActive(false);
			}
			for (int i = 0; i < reqPenguins && i<penguinIcons.Length; i++) {
				penguinIcons[i].SetActive(true);
			}
		}
	}
}
