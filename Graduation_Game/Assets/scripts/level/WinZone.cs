using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.controllers;
using Assets.scripts.components;
using Assets.scripts.UI;
using Assets.scripts.UI.inventory;
using System;
using UnityEngine.SceneManagement;

namespace Assets.scripts.level {
	public class WinZone : MonoBehaviour {
		private int penguins; //num of penguins in win zone
		private int alivePenguins;
		private CanvasController canvas; 
		private Text penguinCounter;

		private string levelName;
		private bool win;
		private CutSceneController cutSceneController;

		void Start() {
			levelName = SceneManager.GetActiveScene().name;
			print(levelName);
			penguins = 0;
			canvas = GameObject.FindGameObjectWithTag(TagConstants.CANVAS).GetComponent<CanvasController>();
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			cutSceneController = GameObject.FindGameObjectWithTag(TagConstants.CUTSCENE).GetComponent<CutSceneController>();

		}

		void Update() {
			if ( !win ) {
				alivePenguins = int.Parse(penguinCounter.text);

				if (penguins != 0 && penguins == alivePenguins && levelName == "W0Level0") {
					SetPrefs(1);
					return;
				}

				if (penguins != 0 && penguins == alivePenguins && levelName == "W0Level1") {
					SetPrefs(2);
					return;
				}

				if (penguins != 0 && penguins == alivePenguins && levelName == "W0Level2") {
					SetPrefs(3);
					return;
				}

				if (penguins != 0 && penguins == alivePenguins && levelName == "W1Level1") {
					SetPrefs(4);
					return;
				}

				if (penguins != 0 && penguins == alivePenguins && levelName == "W1Level2") {
					SetPrefs(5);
					return;
				}
				if (penguins != 0 && penguins == alivePenguins && levelName == "W1Level3") {
					SetPrefs(6);
					return;
				}
				if (penguins != 0 && penguins == alivePenguins && levelName == "W1Level4") {
					SetPrefs(7);
					return;
				}
				if (penguins != 0 && penguins == alivePenguins && levelName == "W1Level5") {
					SetPrefs(8);
					return;
				}
				if (penguins != 0 && penguins == alivePenguins && levelName == "W1Level6") {
					SetPrefs(9);
					return;
				}
				if (penguins != 0 && penguins == alivePenguins && levelName == "W1Level7") {
					SetPrefs(10);
					return;
				}


			}
		}

		private void SetPrefs(int level) {
			//canvas.EndLevel();			// this thing will be called inside the cutscene after the required time has passed (look in controllers/actions/game/CutScene.cs )
			Prefs.SetLevelUnlockIndex(level);
			Inventory.UpdateCount();
			win = true;
			canvas.EndLevel();
			//GameObject.FindGameObjectWithTag(TagConstants.CUTSCENE).GetComponent<CutSceneController>().ShowCutScene();
		}

		void OnTriggerEnter(Collider collider) {
			if ( collider.transform.tag == TagConstants.PENGUIN ) {
				penguins++;
			}
		}
	}
}
