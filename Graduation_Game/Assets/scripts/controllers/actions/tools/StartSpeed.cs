using UnityEngine;
using Assets.scripts.character;
using Assets.scripts.components;

namespace Assets.scripts.controllers.actions.tools {
	public class StartSpeed : Action {
		private readonly Penguin penguin;
		private readonly Directionable directionable;

		public StartSpeed(Penguin penguin, Directionable directionable) {
			this.penguin = penguin;
			this.directionable = directionable;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			directionable.SetRunning(true);
			penguin.SetInitialRunTime(Time.timeSinceLevelLoad);
		}
	}
}
