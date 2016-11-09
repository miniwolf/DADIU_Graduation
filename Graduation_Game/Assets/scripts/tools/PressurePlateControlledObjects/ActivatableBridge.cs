using UnityEngine;
using System.Collections;
using Assets.scripts.components;

namespace Assets.scripts.tools.PressurePlateControlledObjects {
	public class ActivatableBridge : ObjectControlledByPressurePlate {

		public override void Trigger() {
            transform.RotateAround(transform.position - (transform.lossyScale / 2), Vector3.forward, -90);
			tag = TagConstants.LANE;
        }
	}
}
