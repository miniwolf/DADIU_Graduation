using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.scripts;
using Assets.scripts.UI.mainmenu;
using Assets.scripts.controllers.actions.game;

// TODO MainMenuCanvas.prefab card: add rest of the particle systems
// And rename this file to represent sliders and particle systems, not images
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
		}

		// Time it takes to fill the slider
		// used in MainMenuScript
		public float GetFillAmountTime() {
			return fillAmountTime;
		}

		// Update is called once per frame
		void LateUpdate() {
			fillOverTimeIdx = GetLastLevelIndexToFill(levels);
			
			if (fillOverTimeIdx > -1 && EndGame.isNewLevelWon) {
				FillOverTime(sliders[fillOverTimeIdx], particleSystems[fillOverTimeIdx], fillAmountTime);
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
	}
}
