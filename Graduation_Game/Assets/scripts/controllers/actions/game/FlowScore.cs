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
		private Text plutoniumCounter; 
		private Text[] plutoniumThisLevel, plutoniumTotal;


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
			AssignTotalPlutonium();
			AssignThisLevelPlutonium();
			plutoniumThisLevelint = int.Parse(plutoniumCounter.text);
			target = totalPlutonium + plutoniumThisLevelint;

			handler.StartCoroutine(FlowTheScore());
		}

		private void AssignTotalPlutonium(){
			for (int i = 0; i < plutoniumTotal.Length; i++) {
				plutoniumTotal[i].text = totalPlutonium.ToString();
			}
		}
		
		private void AssignThisLevelPlutonium(){
			for (int i = 0; i < plutoniumThisLevel.Length; i++) {
				plutoniumThisLevel[i].text = plutoniumThisLevelint.ToString();
			}
		}


		private IEnumerator FlowTheScore() {
			yield return new WaitForSeconds(canvasController.timeBeforeScoreFlow); //Seems strange atm

			while (plutoniumThisLevelint > 0) {
				// place sound for score flow tick	
				float score = 100 - (plutoniumThisLevelint / float.Parse(plutoniumCounter.text) * 100);
				AkSoundEngine.SetRTPCValue("count_up_pitch", score);
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
				AssignThisLevelPlutonium();
				//plutoniumCounter.text = plutoniumThisLevelint.ToString();
				UpdateScore(portion);
			}
			else {
				int portion = 1;
				plutoniumThisLevelint -= portion;
				AssignThisLevelPlutonium();
				//plutoniumCounter.text = plutoniumThisLevelint.ToString();
				UpdateScore(portion);
			}
			return t;
		}
		private void UpdateScore(float portion) {
			totalPlutonium += (int)portion;
			AssignTotalPlutonium();
		}
	}
}

