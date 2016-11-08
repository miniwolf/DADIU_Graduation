using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
	public class ToolButtons : SnappingTool, Draggable {

		public GameObject jumpPrefab;
		public GameObject switchLanePrefab;

		public float leftLaneOffset = 1f;
		public float rightLaneOffset = -1f;

		private GameObject[] jumpTools;
		private GameObject[] switchLaneTools;
		public int numberOfJumpTools = 8;
		public int numberOfSwitchLaneTools = 8;

		private bool dragging;
		private Vector3 mouseHitPosition;
		private bool jumpIsBeingPlaced = false;
		private bool switchIsBeingPlaced = false;

		void Start() {
			jumpTools = new GameObject[numberOfJumpTools];
			switchLaneTools = new GameObject[numberOfSwitchLaneTools];

			PoolSystem(jumpTools, jumpPrefab, numberOfJumpTools);
			PoolSystem(switchLaneTools, switchLanePrefab, numberOfSwitchLaneTools);
			
		}

		// Instantiates prefabs of length n, stores them in an array objArray
		// and sets them to all to false.
		void PoolSystem(GameObject[] objArray, GameObject prefab, int n) {
			for(int i = 0; i < n; i++) {
				GameObject objTool = Instantiate(prefab);
				objArray[i] = objTool;
				objArray[i].SetActive(false);
			}
		}

		public void OnButtonClickPlaceJump() {
			if(numberOfJumpTools > 0) {
				jumpIsBeingPlaced = true;
				dragging = true;
				numberOfJumpTools--;
				jumpTools[numberOfJumpTools].SetActive(true);
			}
		}

		public void OnButtonClickPlaceSwitchLane() {
			if(numberOfSwitchLaneTools > 0) {
				switchIsBeingPlaced = true;
				dragging = true;
				numberOfSwitchLaneTools--;
				switchLaneTools[numberOfSwitchLaneTools].SetActive(true);
			}
		}


		void Update() {
			foreach ( var touch in Input.touches) {
				// switch lane tool
				if ( touch.phase == TouchPhase.Moved && switchIsBeingPlaced ) {
					PlaceObject(switchLaneTools[numberOfSwitchLaneTools], touch.position);
				}
				if ( touch.phase == TouchPhase.Ended && switchIsBeingPlaced ) {
					switchIsBeingPlaced = false;
					//activate collider when we place it on the scene
					switchLaneTools[numberOfSwitchLaneTools].GetComponentInChildren<SphereCollider>().enabled = true;
					SetDraggingFalse();
				}

				// jump tool
				if ( touch.phase == TouchPhase.Moved && jumpIsBeingPlaced ) {
					PlaceObject(jumpTools[numberOfJumpTools], touch.position);
				}
				if ( touch.phase == TouchPhase.Ended && jumpIsBeingPlaced ) {
					jumpIsBeingPlaced = false;
					jumpTools[numberOfJumpTools].GetComponentInChildren<SphereCollider>().enabled = true;
					SetDraggingFalse();
				}
			}

			// switch lane tool
			if ( Input.GetMouseButton(0) && switchIsBeingPlaced ) {
				PlaceObject(switchLaneTools[numberOfSwitchLaneTools], Input.mousePosition);
			}
			// Release switch lane to the scene
			if ( Input.GetMouseButtonUp(0) && switchIsBeingPlaced ) {
				switchIsBeingPlaced = false;
				switchLaneTools[numberOfSwitchLaneTools].GetComponentInChildren<SphereCollider>().enabled = true;
				SetDraggingFalse();
			}

			// jump tool
			if ( Input.GetMouseButton(0) && jumpIsBeingPlaced ) {
				PlaceObject(jumpTools[numberOfJumpTools], Input.mousePosition);
			}
			// Release jump to the scene
			if ( Input.GetMouseButtonUp(0) && jumpIsBeingPlaced ) {
				jumpTools[numberOfJumpTools].GetComponentInChildren<SphereCollider>().enabled = true;
				jumpIsBeingPlaced = false;
				SetDraggingFalse();
			}
		}

		private void PlaceObject(GameObject obj, Vector3 position) {
			if ( !dragging ) {
				return;
			}

			Ray ray =  Camera.main.ScreenPointToRay(position);
			RaycastHit hit;

			if ( Physics.Raycast(ray, out hit) ) {
				if ( hit.transform.tag.Equals(TagConstants.LANE)) {
					obj.transform.position = hit.point;

					Snap(hit.point, obj.transform, leftLaneOffset, rightLaneOffset);

				}
			}
		}

		private void SetDraggingFalse() {
			dragging = false;
		}

		public bool IsDragged() {
			return dragging;
		}
	}
}
