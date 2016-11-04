using System.Collections.Generic;
using System.Collections;
using Asset.scripts.tools;
using UnityEditor;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
	public class SwitchLaneButton : MonoBehaviour {

		// TODO maybe take z position of the penguin as a ref and add it to the offsets
		public float leftLaneOffset = 1f;
		public float rightLaneOffset = -1f;


		private GameObject switchLaneTool;
		private GameObject player;
		private bool dragging;
		private Vector3 mouseHitPosition;

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
							mouseHitPosition = hit.point;
							switchLaneTool.transform.position = hit.point;
						}
					}
				}
			}
			// Release object to the scene
			if ( Input.GetMouseButtonUp(0) ) {
				dragging = false;

				// Handles snapping on the left lane
				if (Mathf.Abs(leftLaneOffset - mouseHitPosition.z) < Mathf.Abs(rightLaneOffset - mouseHitPosition.z)) {
					switchLaneTool.transform.position = new Vector3(mouseHitPosition.x, mouseHitPosition.y, leftLaneOffset);
				}
				// Handles snapping on the right lane
				else {
					switchLaneTool.transform.position = new Vector3(mouseHitPosition.x, mouseHitPosition.y, rightLaneOffset);
				}
			}
		}
	}
}
