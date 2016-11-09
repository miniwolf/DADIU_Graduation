using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class Speed : Action {
		private Directionable direction;

		public Speed(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			return;
		}

		public void Execute() {
			float initialSpeed = direction.GetWalkSpeed();
			float initialTime = direction.GetInitialTime(Penguin.CurveType.Speed);
			float actualTime = Time.timeSinceLevelLoad;
			AnimationCurve curve = direction.GetCurve(Penguin.CurveType.Speed);
			float newSpeed = initialSpeed * curve.Evaluate(actualTime - initialTime);
			direction.SetSpeed(newSpeed);
		}
	}
}
