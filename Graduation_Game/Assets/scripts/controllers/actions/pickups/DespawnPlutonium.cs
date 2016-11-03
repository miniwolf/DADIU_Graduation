using Assets.scripts.components;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.pickups
{
	public class DespawnPlutonium : Action {
		private GameObject gameObject;
		private Text plutoniumCounter;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
		}

		public void Execute() {
			gameObject.transform.parent.gameObject.SetActive (false);
			plutoniumCounter.text = (int.Parse(plutoniumCounter.text) + 1).ToString();
		}
	}
}