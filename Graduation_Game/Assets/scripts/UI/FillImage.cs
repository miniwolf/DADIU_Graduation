using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.scripts;
using Assets.scripts.UI.mainmenu;

namespace Assets.scripts.UI {
	public class FillImage : MonoBehaviour {
		public static float fillAmountTime = 1f;

		private float fillAmount;
		public static int numOfLvls = 5;
		public Image[] fillImages = new Image[numOfLvls - 1];
		public ParticleSystem[] particleSystems = new ParticleSystem[numOfLvls - 1];
		private string[] levelStatusNames = new string[numOfLvls];
		private int fillOverTimeIdx;
		private MainMenuScript.LvlData[] levels;

		void Start() {
			levels = GetComponent<MainMenuScript>().levels;
		}

		// Update is called once per frame
		void LateUpdate() {
			fillOverTimeIdx = GetLastLevelIndexToFill(levels);

			FillLevelLines(fillOverTimeIdx);

			if (fillOverTimeIdx > -1) {
				FillOverTime(fillImages[fillOverTimeIdx], particleSystems[fillOverTimeIdx], fillAmountTime);
			}
		}

		// Fills previously completed level lines instantly
		private void FillLevelLines(int fillOverTimeIdx) {
			for (int i = 0; i < fillOverTimeIdx; i++) {
				Fill(fillImages[i]);
			}
		}

		// Fills the last completed level line over time
		private void FillOverTime(Image image, ParticleSystem ps, float fillTime) {
			if (Time.timeSinceLevelLoad < fillTime) {
				float fillAmountChange = Time.deltaTime / fillTime;
				image.fillAmount += fillAmountChange;
			}
			else {
				ps.Play();

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

		// Instantly fills an image
		private void Fill(Image image) {
			image.fillAmount = 1f;
		}
	}
}
