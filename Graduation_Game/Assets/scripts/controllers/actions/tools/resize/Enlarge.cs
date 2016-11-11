using Assets.scripts.character;
using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.tools.resize {
	public class Enlarge : Resize {
		public Penguin.CurveType GetCurveType() {
			return Penguin.CurveType.Enlarge;
		}

		public Vector3 GetScale(Directionable direction, float scalingFactor) {
			return direction.GetInitialScale() * scalingFactor;
		}
	}
}
