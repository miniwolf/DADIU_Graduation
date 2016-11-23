﻿using UnityEngine;
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
			plutoniumThisLevel.GetComponent<Text>().text = /*plutoniumCounter.text;*/ 5000.ToString();

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
			float startTime = Time.time;
			while (int.Parse(plutoniumThisLevel.text) > 0) {
				UpdateScore(1);/*
				timeIncrements += timeIncrements;
				Debug.Log(timeIncrements);*/
				yield return new WaitForSeconds(GetTimeFromCurve(Time.time - startTime));
			}

			PlayerPrefs.SetInt("Plutonium", target);
			PlayerPrefs.Save();
			yield return null;
		}

		private float GetTimeFromCurve(float time) {
			float t = 1 / ((canvas.scoreFlowTimeCurve.Evaluate(time/200) + 0.001f) * canvas.amplitude);
			Debug.Log(t);
			return t;
		}

		private IEnumerator SpawnStars() {
			yield return new WaitForSeconds(canvas.timeBeforeStarSpawn);
			while (SpawnNextStar()) {
				yield return new WaitForSeconds(canvas.timeBewteenStarSpawn);
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
			actionable.ExecuteAction(GameActions.TriggerCutScene);
			PlayerPrefs.DeleteKey("hasVisited");
			starsDoneSpawned = true;
			yield return null;
		}

		private void UpdateScore(int portion) {
			plutoniumTotal.text = (int.Parse(plutoniumTotal.text) + portion).ToString();
			plutoniumThisLevel.text = (int.Parse(plutoniumThisLevel.text) - portion).ToString();
		}

		public bool SpawnNextStar() {
			for (int i = 0; i < 3; i++) {
				if (starsSpawned == i) {
					if (int.Parse(plutoniumCounter.GetComponent<Text>().text) >= (int)canvas.GetType().GetField("penguinsRequiredFor" + (i + 1).ToString() + "Stars").GetValue(canvas)) {
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
