using UnityEngine;
using System.Collections;
namespace Assets.scripts.UI.screen.ingame {

	public class SnappingTool : MonoBehaviour {


		// Handles snapping on left or right lane
		public void Snap(Vector3 hitPos, Transform tool, float leftLaneOffset, float rightLaneOffset) {
			tool.position =
					Mathf.Abs(leftLaneOffset - hitPos.z) < Mathf.Abs(rightLaneOffset - hitPos.z)
					? new Vector3(hitPos.x, tool.position.y, leftLaneOffset)
					: new Vector3(hitPos.x, tool.position.y, rightLaneOffset);
		}
	}
}
