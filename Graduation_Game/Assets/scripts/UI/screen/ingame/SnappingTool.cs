using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {

	public class SnappingTool : SnappingToolInterface {

		public float leftLaneOffset;
		public float rightLaneOffset;
		private RaycastHit hit;
		int layerMask = 1 << 8;
		private Vector3 pos;

		// Handles snapping on left or right lane
		public void Snap(Vector3 hitPos, Transform tool) {
			pos = 	Mathf.Abs(leftLaneOffset - hitPos.z) < Mathf.Abs(rightLaneOffset - hitPos.z)
					? new Vector3(hitPos.x, tool.position.y, leftLaneOffset)
					: new Vector3(hitPos.x, tool.position.y, rightLaneOffset);
			Debug.DrawRay(new Vector3(pos.x, 50f, pos.z), -Vector3.up * 50f);
			if (Physics.Raycast(new Vector3(pos.x, 50f, pos.z), -Vector3.up, out hit, 80f, layerMask)) {
				tool.position = new Vector3(pos.x, hit.point.y, pos.z);
			}
		}

		public void SetCenter (GameObject obj) {
			leftLaneOffset = obj.transform.position.z + 1f;
			rightLaneOffset = obj.transform.position.z - 1f;
		}
	}
}
