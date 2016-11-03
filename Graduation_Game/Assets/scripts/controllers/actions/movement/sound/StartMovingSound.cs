using UnityEngine;

namespace Assets.scripts.controllers.actions.movement.sound {
	public class StartMovingSound : Action {
		private GameObject go;

		public void Setup(GameObject gameObject) {
			go = gameObject;
		}

		public void Execute() {
			// Play sound
		}
	}
}
