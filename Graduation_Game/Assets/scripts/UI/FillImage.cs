using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.scripts.UI.mainmenu;

public class FillImage : MonoBehaviour {
	public float fillAmountTime = 1f;
	public ParticleSystem particleSystem;
	private float fillAmount;
	public static int numOfLvls = 5;
	public Image[] fillImages = new Image[numOfLvls-1];
	private string[] levelStatusNames = new string[numOfLvls-1];
	private string status = "status";
	private string completed = "completed";
	private int fillOverTimeIdx = -1;

	void Start() {
		levelStatusNames = InitilizeLevelStatusNames(levelStatusNames);

		fillOverTimeIdx = GetLastLevelIndexToFill(levelStatusNames);

		FillLevelLines(fillOverTimeIdx);
	}

	// Update is called once per frame
	void Update() {
		if(fillOverTimeIdx > -1) {
			FillOverTime(fillImages[fillOverTimeIdx], fillAmountTime);
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
	private void FillOverTime(Image image, float fillTime) {
		if (Time.timeSinceLevelLoad < fillTime) {
			float fillAmountChange = Time.deltaTime / fillTime;
			image.fillAmount += fillAmountChange;
		} else {
			particleSystem.Play();
		}
	}

	// Returns the index of the last level that was completed,
	// and -1 if no level was completed
	private int GetLastLevelIndexToFill(string[] levelStatusNames) {
		for (int i = levelStatusNames.Length - 1; i >= 0; i--) {
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
