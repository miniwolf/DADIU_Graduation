using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.controllers;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.UI;
using Assets.scripts.UI.inventory;

namespace Assets.scripts.UI {
	public class CanvasController : ActionableGameEntityImpl<GameActions> {

		// Use this for initialization
		public int penguinsRequiredFor3Stars;
		public int penguinsRequiredFor2Stars;
		public int penguinsRequiredFor1Stars;
		private Text penguinCounter;
		private GameObject endScene;
		private bool endLevel;
		private bool over;

		void Start() {
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			foreach (Transform g in gameObject.GetComponentsInChildren<Transform>(true))
				if (g.tag == TagConstants.ENDSCENE)
					endScene = g.gameObject;
		}

		void Update () {
			// update inventory when game over
			if(int.Parse(penguinCounter.text) < 1 && !over) {
				Inventory.UpdateCount();
				over = true;
			}

			// TODO fix and refactor this part
			if (int.Parse(penguinCounter.text) < 1 || endLevel) {
				ExecuteAction(GameActions.EndLevel);
			}
		}		

		public override string GetTag() {
			return TagConstants.CANVAS;
		}

		public void EndLevel() {
			endLevel = true;
		}

	}
}
