using UnityEngine;
using System.Collections;
namespace Assets.scripts.UI.screen.ingame {

	public class SnappingTool {

		public float leftLaneOffset;
		public float rightLaneOffset;

		public void DefineOffSet(GameObject obj){
			leftLaneOffset = obj.transform.position.z + 1f;
			rightLaneOffset = obj.transform.position.z - 1f;
		}

		// Handles snapping on left or right lane
		public void Snap(Vector3 hitPos, Transform tool) {
			tool.position =
					Mathf.Abs(leftLaneOffset - hitPos.z) < Mathf.Abs(rightLaneOffset - hitPos.z)
					? new Vector3(hitPos.x, tool.position.y, leftLaneOffset)
					: new Vector3(hitPos.x, tool.position.y, rightLaneOffset);
		}
	}
}
