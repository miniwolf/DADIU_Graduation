using Assets.scripts.character;
using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.tools.resize {
	public interface Resize {
		Penguin.CurveType GetCurveType();
		Vector3 GetScale(Directionable direction, float scalingFactor);
	}
}