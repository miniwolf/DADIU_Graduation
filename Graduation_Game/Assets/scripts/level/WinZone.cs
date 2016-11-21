using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.controllers;
using Assets.scripts.components;
using Assets.scripts.UI;
using Assets.scripts.UI.inventory;
using System;

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
			levelName = Application.loadedLevelName;
			print(levelName);
			penguins = 0;
			canvas = GameObject.FindGameObjectWithTag(TagConstants.CANVAS).GetComponent<CanvasController>();
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			cutSceneController = GameObject.FindGameObjectWithTag(TagConstants.CUTSCENE).GetComponent<CutSceneController>();

		}

		void Update() {
			if ( !win ) {
				alivePenguins = int.Parse(penguinCounter.text);

				if (penguins != 0 && penguins == alivePenguins && levelName == "Level1") {
					SetPrefs(1);
					return;
				}

				if (penguins != 0 && penguins == alivePenguins && levelName == "Level2") {
					SetPrefs(2);
					return;
				}

				if (penguins != 0 && penguins == alivePenguins && levelName == "Level3") {
					SetPrefs(3);
					return;
					
				}

				if (penguins != 0 && penguins == alivePenguins && levelName == "Level4") {
					SetPrefs(4);
					return;
				}

				if (penguins != 0 && penguins == alivePenguins && levelName == "Level5") {
					SetPrefs(5);
					return;
				}

			}
		}

		private void SetPrefs(int level) {
			//canvas.EndLevel();			// this thing will be called inside the cutscene after the required time has passed (look in controllers/actions/game/CutScene.cs )
			PlayerPrefs.SetInt("LevelUnlockIndex", level);
			Inventory.UpdateCount();
			win = true;
			canvas.ExecuteAction(GameActions.EndLevel);
			//GameObject.FindGameObjectWithTag(TagConstants.CUTSCENE).GetComponent<CutSceneController>().ShowCutScene();
		}

		void OnTriggerEnter(Collider collider) {
			if ( collider.transform.tag == TagConstants.PENGUIN ) {
				penguins++;
			}
		}
	}
}
