using System.Linq;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
	public class ToolButtons : MonoBehaviour, Draggable {

		// TODO maybe take z position of the penguin as a ref and add it to the offsets
		public float leftLaneOffset = 1f;
		public float rightLaneOffset = -1f;
		private GameObject jumpObjTool;
		private GameObject switchLaneTool;
		private bool dragging;
		private Vector3 mouseHitPosition;
		private bool jumpIsBeingPlaced = false;
		private bool switchIsBeingPlaced = false;

		public void PlaceJump(GameObject toPlace) {
			jumpIsBeingPlaced = true;
			dragging = true;
			jumpObjTool = Instantiate(toPlace);
			jumpObjTool.SetActive(true);
		}

		public void PlaceSwitchLane(GameObject toPlace) {
			dragging = true;
			switchIsBeingPlaced = true;
			switchLaneTool = Instantiate(toPlace);
			switchLaneTool.SetActive(true);
		}

		void Update() {
			foreach ( var touch in Input.touches) {
				// switch lane tool
				if ( touch.phase == TouchPhase.Moved && switchIsBeingPlaced ) {
					PlaceObject(switchLaneTool, touch.position);
				}
				if ( touch.phase == TouchPhase.Ended && switchIsBeingPlaced ) {
					switchIsBeingPlaced = false;
					//activate collider when we place it on the scene
					switchLaneTool.GetComponentInChildren<SphereCollider>().enabled = true;
					Snap();
				}

				// jump tool
				if ( touch.phase == TouchPhase.Moved && jumpIsBeingPlaced ) {
					PlaceObject(jumpObjTool, touch.position);
				}
				if ( touch.phase == TouchPhase.Ended && jumpIsBeingPlaced ) {
					jumpIsBeingPlaced = false;
					jumpObjTool.GetComponentInChildren<SphereCollider>().enabled = true;
					Snap();
				}
			}

			// switch lane tool
			if ( Input.GetMouseButton(0) && switchIsBeingPlaced ) {
				PlaceObject(switchLaneTool, Input.mousePosition);
			}
			// Release switch lane to the scene
			if ( Input.GetMouseButtonUp(0) && switchIsBeingPlaced ) {
				switchIsBeingPlaced = false;
				switchLaneTool.GetComponentInChildren<SphereCollider>().enabled = true;
				Snap();
			}

			// jump tool
			if ( Input.GetMouseButton(0) && jumpIsBeingPlaced ) {
				PlaceObject(jumpObjTool, Input.mousePosition);
			}
			// Release jump to the scene
			if ( Input.GetMouseButtonUp(0) && jumpIsBeingPlaced ) {
				jumpObjTool.GetComponentInChildren<SphereCollider>().enabled = true;
				jumpIsBeingPlaced = false;
				Snap();
			}
		}

		private void PlaceObject(GameObject obj, Vector3 position) {
			if ( !dragging ) {
				return;
			}

			Ray ray =  Camera.main.ScreenPointToRay(position);
			RaycastHit hit;

			if ( Physics.Raycast(ray, out hit) ) {
				if ( hit.transform.tag.Equals(TagConstants.LANE) ) {
					obj.transform.position = hit.point;
				}
			}
		}

		private void Snap() {
			dragging = false;

			// TODO snaping
		}

		public bool IsDragged() {
			return dragging;
		}
	}
}
