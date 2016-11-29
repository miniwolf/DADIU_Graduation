using UnityEngine;
using Assets.scripts.controllers.actions;
using Assets.scripts.UI;
using Assets.scripts.components;
using System.Collections;
using Assets.scripts;
using UnityEngine.UI;

namespace AssemblyCSharp {
	public class FlowScore : Action{
		private GameObject canvas;
		private CanvasController canvasController;
		private CouroutineDelegateHandler handler;
		private int plutoniumThisLevelint, totalPlutonium, target;
		private Text plutoniumCounter, plutoniumThisLevel, plutoniumTotal;


		public FlowScore (CouroutineDelegateHandler handler) {
			this.handler = handler;
		}

		public void Setup (GameObject gameObject) {
			canvas = gameObject;
			canvasController = canvas.GetComponent<CanvasController>();
			plutoniumCounter = canvasController.GetPlutoniumCounter();
			plutoniumTotal = canvasController.GetPlutoniumTotal();
			plutoniumThisLevel = canvasController.GetPlutoniumThisLevel();
			totalPlutonium = PlayerPrefs.GetInt("Plutonium");
		}

		public void Execute () {
			target = totalPlutonium + int.Parse(plutoniumThisLevel.text);
			plutoniumTotal.GetComponent<Text>().text = totalPlutonium.ToString();
			plutoniumThisLevel.GetComponent<Text>().text = plutoniumCounter.text;
			plutoniumThisLevelint = int.Parse(plutoniumCounter.text);

			handler.StartCoroutine(FlowTheScore());
		}

		private IEnumerator FlowTheScore() {
			yield return new WaitForSeconds(canvasController.timeBeforeScoreFlow); //Seems strange atm

			while (plutoniumThisLevelint > 0) {
				yield return new WaitForSeconds(GetTimeFromCurve());
			}

			PlayerPrefs.SetInt("Plutonium", totalPlutonium);
			//handler.StartCoroutine(LoadMainMenu());
			yield return null;
		}

		private float GetTimeFromCurve() {
			float t = 1 / (0.015f * (canvasController.scoreFlowScalingFactor * plutoniumThisLevelint));
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
		private void UpdateScore(float portion) {
			totalPlutonium += (int)portion;
			plutoniumTotal.text = totalPlutonium.ToString();
		}
	}
}

