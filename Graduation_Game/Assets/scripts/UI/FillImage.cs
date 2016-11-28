using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.scripts.UI.mainmenu;

public class FillImage : MonoBehaviour {
	public float fillAmountTime = 1f;
	
	private float fillAmount;
	public static int numOfLvls = 5;
	public Image[] fillImages = new Image[numOfLvls-1];
	public ParticleSystem[] particleSystems = new ParticleSystem[numOfLvls - 1];
	private string[] levelStatusNames = new string[numOfLvls];
	private string status = "status";
	private string completed = "completed";
	private int fillOverTimeIdx;
	
	void Start() {
		levelStatusNames = InitilizeLevelStatusNames(levelStatusNames);
	}

	// Update is called once per frame
	void LateUpdate() {

		fillOverTimeIdx = GetLastLevelIndexToFill(levelStatusNames);

		FillLevelLines(fillOverTimeIdx);

		if (fillOverTimeIdx > -1) {
			FillOverTime(fillImages[fillOverTimeIdx], particleSystems[fillOverTimeIdx], fillAmountTime);
		}
	}

	// levelStatusNames will include the PlayerPrefs level status names
	private string[] InitilizeLevelStatusNames(string[] levelStatusNames) {
		for (int i = 0; i < levelStatusNames.Length; i++) {
			levelStatusNames[i] = "Level" + (i + 1).ToString() + status;
		}
		return levelStatusNames;
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
		} else {
			ps.Play();
		}
	}

	// Returns the index of the last level that was completed,
	// and -1 if no level was completed
	private int GetLastLevelIndexToFill(string[] levelStatusNames) {
		for (int i = levelStatusNames.Length-1; i > -1; i--) {
			if (PlayerPrefs.GetString(levelStatusNames[i]) == completed) {
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
