using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class StopSpeed : Action {
		private Penguin penguin;
		private float initialSpeed;

		public void Setup(GameObject gameObject) {
			penguin = gameObject.GetComponent<Penguin>();
			initialSpeed = penguin.GetWalkSpeed();
		}

		public void Execute() {
			penguin.SetRunning(false);
			penguin.SetSpeed(initialSpeed);
		}
	}
}
