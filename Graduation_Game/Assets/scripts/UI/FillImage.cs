using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.scripts;
using Assets.scripts.UI.mainmenu;
using Assets.scripts.controllers.actions.game;

// TODO rename this file to represent sliders and particle systems, not images
namespace Assets.scripts.UI {
	public class FillImage : MonoBehaviour {
		public float fillAmountTime = 5f;
		private static int numOfLvls;
	    public Slider[] sliders;
		public ParticleSystem[] particleSystems;

		private float fillAmount;
		private string[] levelStatusNames ;
		private int fillOverTimeIdx;
		private MainMenuScript.LvlData[] levels;

		void Start() {
			levels = GetComponent<MainMenuScript>().levels;
		    numOfLvls = levels.Length;
		    levelStatusNames = new string[numOfLvls];
			fillOverTimeIdx = GetLastLevelIndexToFill(levels);

			LoadPreviouslyFilledSliders();
		}
		
		// Update is called once per frame
		void LateUpdate() {			
			if (fillOverTimeIdx > -1 && EndGame.isNewLevelWon) {
				FillOverTime(sliders[fillOverTimeIdx], particleSystems[fillOverTimeIdx], fillAmountTime);
			}
		}

		// Time it takes to fill the slider
		// used in MainMenuScript
		public float GetFillAmountTime() {
			return fillAmountTime;
		}

		private void LoadPreviouslyFilledSliders() {
			int fillIndex = fillOverTimeIdx;

			// We want to use the last index to load the slider over time
			// whenever a fresh level is won
			if (EndGame.isNewLevelWon) fillIndex = fillIndex - 1; 

			for (int i = 0; i <= fillIndex; i++) {
				Fill(sliders[i]);
			}
		}

		// Fills the last completed level line over time
		private void FillOverTime(Slider slider, ParticleSystem ps, float fillTime) {
			if (Time.timeSinceLevelLoad < fillTime) {
				float fillAmountChange = Time.deltaTime / fillTime;
				slider.value += fillAmountChange;
			}
			else {
				ps.Play(); // Particle system played when slider has been filled
			}
		}

		// Returns the index of the last level that was completed,
		// and -1 if no level was completed
		private int GetLastLevelIndexToFill(MainMenuScript.LvlData[] levels) {
			for (int i = levels.Length - 1; i > -1; i--) {
				if(Prefs.IsLevelStatusComplete(levels[i].sceneFileName)) {
					fillOverTimeIdx = i;
					return fillOverTimeIdx;
				}
			}
			return -1;
		}

		// Instantly fills a slider
		private void Fill(Slider slider) {
			slider.value = 1f;
		}
	}
}
