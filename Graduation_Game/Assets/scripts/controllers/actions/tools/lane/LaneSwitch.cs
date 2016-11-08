using UnityEngine;

namespace Assets.scripts.controllers.actions.tools.lane {
	public interface LaneSwitch {
		Quaternion GetNewRotation(Quaternion oldRotation);
		bool LaneSwitchCondition(float positionZ, float f);
	}
}