using Assets.scripts.character;
using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.tools.resize {
	public class Minimize : Resize {
		public Penguin.CurveType GetCurveType() {
			return Penguin.CurveType.Minimize;
		}

		public Vector3 GetScale(Directionable direction, float scalingFactor) {
			return direction.GetInitialScale() / scalingFactor;
		}
	}
}
