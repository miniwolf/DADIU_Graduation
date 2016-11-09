using UnityEngine;
using Assets.scripts.level;

namespace Assets.scripts.controllers.actions.tools.lane {
	public interface LaneSwitch {
		Quaternion GetNewRotation(Quaternion oldRotation);
		bool LaneSwitchCondition(float positionZ, float f);
		float GetDirection(float zPos, LevelSettings levelSettings);
	}
}