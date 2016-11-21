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
		private bool win;

		void Start() {
			penguins = 0;
			canvas = GameObject.FindGameObjectWithTag(TagConstants.CANVAS).GetComponent<CanvasController>();
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
		}

		void Update() {
			if ( !win ) {
				alivePenguins = int.Parse(penguinCounter.text);
				if ( penguins != 0 && penguins == alivePenguins ) {
					//canvas.EndLevel();			// this thing will be called inside the cutscene after the required time has passed (look in controllers/actions/game/CutScene.cs )
					Inventory.UpdateCount();
					win = true;
					GameObject.FindGameObjectWithTag(TagConstants.CUTSCENE).GetComponent<CutSceneController>().ShowCutScene();
				}
			}
		}

		void OnTriggerEnter(Collider collider) {
			if ( collider.transform.tag == TagConstants.PENGUIN ) {
				penguins++;
			}
		}
	}
}
