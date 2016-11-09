using UnityEngine;
using Assets.scripts.character;
using Assets.scripts.components;

namespace Assets.scripts.controllers.actions.tools {
	public class StopSpeed : Action {
		private Penguin penguin;
		private float initialSpeed;
		private readonly Directionable directionable;

		public StopSpeed(Penguin penguin, Directionable directionable) {
			this.penguin = penguin;
			this.directionable = directionable;
		}

		public void Setup(GameObject gameObject) {
			penguin = gameObject.GetComponent<Penguin>();
		}

		public void Execute() {
			directionable.SetRunning(false);
			penguin.SetSpeed(directionable.GetWalkSpeed());
		}
	}
}
