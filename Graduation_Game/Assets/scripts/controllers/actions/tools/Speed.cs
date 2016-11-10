using UnityEngine;
using Assets.scripts.character;
using Assets.scripts.components;

namespace Assets.scripts.controllers.actions.tools {
	public class Speed : Action {
		private readonly Directionable direction;

		public Speed(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			var curve = direction.GetCurve(Penguin.CurveType.Speed);
			var newSpeed = direction.GetWalkSpeed() *
			               curve.Evaluate(Time.timeSinceLevelLoad - direction.GetInitialTime(Penguin.CurveType.Speed));
			direction.SetSpeed(newSpeed);
		}
	}
}
