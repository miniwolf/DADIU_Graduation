using System;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {

	public class SnappingTool : SnappingToolInterface {
		public float leftLaneOffset;
		public float rightLaneOffset;
		public int xSnapValue = 1;

		private RaycastHit hit;
		private const int layerMask = 1 << 8;
		private Vector3 pos;

		// Handles snapping on left or right lane
		public void Snap(Vector3 hitPos, Transform tool) {
			/*pos = Mathf.Abs(leftLaneOffset - hitPos.z) < Mathf.Abs(rightLaneOffset - hitPos.z)
					? new Vector3(hitPos.x, tool.position.y, leftLaneOffset)
					: new Vector3(hitPos.x, tool.position.y, rightLaneOffset);
			pos.x = Round(pos.x);*/

			if ( Mathf.Abs(leftLaneOffset - hitPos.z) < Mathf.Abs(rightLaneOffset - hitPos.z) ) {
				pos = new Vector3(hitPos.x, tool.position.y, leftLaneOffset);
				if ( tool.tag == TagConstants.SWITCHTEMPLATE ) {
					Material mat = tool.gameObject.GetComponentInChildren<MeshRenderer>().material;
					if ( mat.name != "LeftArrow" ) {
						tool.gameObject.GetComponentInChildren<MeshRenderer>().material = Resources.Load("LeftArrow", typeof(Material)) as Material;
						Transform[] trans;
						trans = tool.gameObject.GetComponentsInChildren<Transform>();
						for (int i = 0; i < trans.Length; i++) {
							if (trans[i].tag == TagConstants.LANECHANGEARROW) {
								trans[i].rotation = Quaternion.Euler(new Vector3(180f, 180f, 0f));
								break;
							}
						}
					}
					/*Vector3 scale = tool.transform.localScale;
					if ( scale.x < 0 ) {
						tool.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
					}*/
				}
			} else {
				pos = new Vector3(hitPos.x, tool.position.y, rightLaneOffset);
				if ( tool.tag == TagConstants.SWITCHTEMPLATE ) {
					Material mat = tool.gameObject.GetComponentInChildren<MeshRenderer>().material;
					if ( mat.name != "RightArrow" ) {
						Transform[] trans;
						trans = tool.gameObject.GetComponentsInChildren<Transform>();
						tool.gameObject.GetComponentInChildren<MeshRenderer>().material = Resources.Load("RightArrow", typeof(Material)) as Material;
						for (int i = 0; i < trans.Length; i++) {
							if (trans[i].tag == TagConstants.LANECHANGEARROW) {
								trans[i].rotation = Quaternion.Euler(new Vector3(0, 180f, 0f));
								break;
							}
						}
					}
					/*Vector3 scale = tool.transform.localScale;
					if ( scale.x > 0 ) {
						tool.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
					}*/
				}
			}
			pos.x = Round(pos.x);

			Debug.DrawRay(new Vector3(pos.x, hitPos.y+2f, pos.z), -Vector3.up * 1f);
			if (Physics.Raycast(new Vector3(pos.x, hitPos.y+2f, pos.z), -Vector3.up, out hit, 2f, layerMask)) {
				tool.position = new Vector3(pos.x, hit.point.y, pos.z);
			}
		}

		private float Round(float input) {
			return xSnapValue * Mathf.Round(input / xSnapValue);
		}

		public void SetCenter (GameObject obj) {
			leftLaneOffset = obj.transform.position.z + 1f;
			rightLaneOffset = obj.transform.position.z - 1f;
			xSnapValue = obj.GetComponent<Settings>().GetXSnapValue();
		}
	}
}
