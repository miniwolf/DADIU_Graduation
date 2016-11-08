using System;
using UnityEngine;

namespace Assets.scripts.controllers.actions.tools.lane {
	public class Right : LaneSwitch {
		private const double ZERO_TOLERANCE = 0.0001;

		public Quaternion GetNewRotation(Quaternion oldRotation) {
			return Quaternion.Euler(0, Math.Abs(oldRotation.y) < ZERO_TOLERANCE ? 45 : 90, 0);
		}

		public bool LaneSwitchCondition(float positionZ, float f) {
			return positionZ > f;
		}
	}
}
