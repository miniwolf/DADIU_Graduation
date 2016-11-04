using System.Collections.Generic;
using System.Collections;
using Asset.scripts.tools;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
	public class SwitchLaneButton : MonoBehaviour {

		private GameObject switchLaneTool;
		private bool dragging;

		public void PlaceSwitchLane() {
			dragging = true;
			GameObject switchLaneObj = GameObject.FindGameObjectWithTag(TagConstants.SWITCHLANE);
			switchLaneTool = Instantiate(switchLaneObj); 
		}

		void Update() {
			if ( Input.GetMouseButton(0) ) {
				if ( dragging ) {
					Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if ( Physics.Raycast(ray, out hit) ) {
						if ( hit.transform.tag.Equals(TagConstants.LANE) ) {
							switchLaneTool.transform.position = hit.point;
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
