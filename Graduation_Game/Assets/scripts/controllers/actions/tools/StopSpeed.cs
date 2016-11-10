using UnityEngine;
using Assets.scripts.character;
using Assets.scripts.components;

namespace Assets.scripts.controllers.actions.tools {
	public class StopSpeed : Action {
		private readonly Directionable direction;

		public StopSpeed(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			direction.SetRunning(false);
			direction.SetSpeed(direction.GetWalkSpeed());
			direction.removeCurve(Penguin.CurveType.Speed);
		}
	}
}
