using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class StopMinimize : Action {
		private Directionable direction;

		public StopMinimize(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			return;
		}

		public void Execute() {
			// stop the penguin from executing enlarge
			direction.SetWeight(Penguin.Weight.Normal);
			direction.SetMinimizing(false);
			// set back the original scale of the penguin
			Vector3 initialScale = direction.GetInitialScale();
			direction.SetScale(initialScale);
			//remove curve for enlarging
			direction.removeCurve(Penguin.CurveType.Minimize);
		}
	}
}
