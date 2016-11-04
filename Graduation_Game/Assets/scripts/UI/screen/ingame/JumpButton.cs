using System.Collections.Generic;
using System.Collections;
using Asset.scripts.tools;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
	public class JumpButton : MonoBehaviour {

		private GameObject jumpObjTool;
		private bool dragging;

		public void PlaceJump() {
			dragging = true;
			GameObject jumpObj = GameObject.FindGameObjectWithTag(TagConstants.JUMP);
			jumpObjTool = Instantiate(jumpObj); 
		}

		void Update() {
			if ( Input.GetMouseButton(0) ) {
				if ( dragging ) {
					Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if ( Physics.Raycast(ray, out hit) ) {
						if ( hit.transform.tag.Equals(TagConstants.LANE) ) {
							jumpObjTool.transform.position = hit.point;
						}
					}
				}
			}
			// release object on scene
			if ( Input.GetMouseButtonUp(0) ) {
				// TODO: SNAP in lane
				dragging = false;
			}
		}
	}
}
