using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class Speed : Action {
		private Penguin penguin;

		public void Setup(GameObject gameObject) {
			penguin = gameObject.GetComponent<Penguin>();

		}

		public void Execute() {
			float initialSpeed = penguin.GetWalkSpeed();
			float initialTime = penguin.GetInitialRunTime();
			float actualTime = Time.timeSinceLevelLoad;
			float newSpeed = initialSpeed * penguin.GetCurve().Evaluate(actualTime - initialTime);
			penguin.SetSpeed(newSpeed);
		}
	}
}
