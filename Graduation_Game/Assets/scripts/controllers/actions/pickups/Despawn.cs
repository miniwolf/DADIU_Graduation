using UnityEngine;

namespace Assets.scripts.controllers.actions.pickups {
	public class Despawn : Action {
		private GameObject gameObject;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
		}

		public void Execute() {
			gameObject.SetActive (false);
		}
	}
}
