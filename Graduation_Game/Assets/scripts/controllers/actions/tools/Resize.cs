using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class Resize : Action {
		private readonly Directionable direction;
		private readonly resize.Resize resize;

		public Resize(Directionable direction, resize.Resize resize) {
			this.direction = direction;
			this.resize = resize;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			var curve = direction.GetCurve(resize.GetCurveType());
			var scalingFactor = curve.Evaluate(Time.timeSinceLevelLoad - direction.GetInitialTime(Penguin.CurveType.Enlarge));
			direction.SetScale(resize.GetScale(direction, scalingFactor));
		}
	}
}
