using Assets.scripts.components;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.pickups {
	public class TriggerStar : Action {
		private GameObject gameObject;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
		}

		public void Execute() {
		}
	}
}
