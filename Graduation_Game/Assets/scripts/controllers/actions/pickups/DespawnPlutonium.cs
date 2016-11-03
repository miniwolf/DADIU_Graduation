using Assets.scripts.components;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.movement
{
	public class DespawnPlutonium : Action {
		private GameObject gameObject;
		private Text inventory;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			inventory = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
		}

		public void Execute() {
			gameObject.transform.parent.gameObject.SetActive (false);
			inventory.text = (int.Parse(inventory.text) + 1).ToString();
		}
	}
}