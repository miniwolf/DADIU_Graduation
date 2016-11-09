using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class StopSpeed : Action {
		private Directionable direction;

		public StopSpeed(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			return;
		}

		public void Execute() {
			direction.SetRunning(false);
			float initialSpeed = direction.GetWalkSpeed();
			direction.SetSpeed(initialSpeed);
			direction.removeCurve(Penguin.CurveType.Speed);
		}
	}
}
