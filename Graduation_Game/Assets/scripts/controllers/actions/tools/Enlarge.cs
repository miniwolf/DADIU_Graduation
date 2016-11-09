using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class Enlarge : Action {
		private Directionable direction;

		public Enlarge(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			return;
		}

		public void Execute() {
			Vector3 initialScale = direction.GetInitialScale();
			float initialTime = direction.GetInitialTime(Penguin.CurveType.Enlarge);
			float actualTime = Time.timeSinceLevelLoad;
			AnimationCurve curve = direction.GetCurve(Penguin.CurveType.Enlarge);
			float scalingFactor = curve.Evaluate(actualTime - initialTime);
			Vector3 newScale = initialScale * scalingFactor;
			direction.SetScale(newScale);
		}
	}
}
