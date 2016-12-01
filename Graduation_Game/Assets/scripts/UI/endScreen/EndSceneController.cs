using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using Assets.scripts;
using Assets.scripts.UI.inventory;
using Assets.scripts.level;
using System.Collections;

public class EndSceneController: MonoBehaviour {

	private Image popup;

	void Start(){
		popup = GameObject.FindGameObjectWithTag(TagConstants.UI.POPUP_PENGUIN_REQUIRED).GetComponent<Image>();
		DisablePopup();
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			DisablePopup();
		}
	}
		
	public void GoToShop() {
		SceneManager.LoadScene("Store");
	}

	public void Replay() {
		GameObject[] penguinSpawners = GameObject.FindGameObjectsWithTag(TagConstants.PENGUIN_SPAWNER);
		int initialPenguinCount = 0;
		// we might have several penguin spawners (one in each lane)
		foreach ( GameObject go in penguinSpawners ) {
			initialPenguinCount += go.GetComponent<PenguinSpawner>().GetInitialPenguinCount();
		}

		if(Inventory.penguinCount.GetValue() >= initialPenguinCount) {
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
		} else {
			EnablePopup();
		}

	}
	public void BackToMenu() {
		SceneManager.LoadScene("MainMenuScene");
	}
	public void GoNextLevel() {
		string nextLevel = Regex.Replace(SceneManager.GetActiveScene().name, @"[\d-]", string.Empty) + (int.Parse(Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value) + 1).ToString();
		try {
			SceneManager.LoadScene(nextLevel);
		}
		catch {
			SceneManager.LoadScene("MainMenuScene");
		}
	}

	private void DisablePopup() {
		popup.transform.localScale = Vector3.zero;
		popup.enabled = false;
	}

	private void EnablePopup() {
		popup.transform.localScale = Vector3.one;
		popup.enabled = true;
	}
}
