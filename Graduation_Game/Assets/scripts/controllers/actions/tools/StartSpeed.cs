using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class StartSpeed : Action {
		private Penguin penguin;
		private float initialSpeed;

		public void Setup(GameObject gameObject) {
			penguin = gameObject.GetComponent<Penguin>();
		}

		public void Execute() {
			penguin.SetRunning(true);
			penguin.SetInitialRunTime(Time.timeSinceLevelLoad);
		}
	}
}
