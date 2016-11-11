using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class Enlarge : Action {
		private readonly Directionable direction;

		public Enlarge(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			var curve = direction.GetCurve(Penguin.CurveType.Enlarge);
			var scalingFactor = curve.Evaluate(Time.timeSinceLevelLoad - direction.GetInitialTime(Penguin.CurveType.Enlarge));
			direction.SetScale(direction.GetInitialScale() * scalingFactor);
		}
	}
}
