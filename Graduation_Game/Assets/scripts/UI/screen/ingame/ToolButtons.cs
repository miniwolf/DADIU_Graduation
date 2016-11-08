using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.components.registers;

namespace Assets.scripts.UI.screen.ingame {
	public class ToolButtons : MonoBehaviour, GameEntity, Draggable, SetSnappingTool {

		private SnappingToolInterface snapping;

		public GameObject jumpPrefab;
		public GameObject switchLanePrefab;
		public GameObject speedPrefab;

		private GameObject[] jumpTools;
		private GameObject[] switchLaneTools;
		private GameObject[] speedTools;
		public int numberOfJumpTools = 8;
		public int numberOfSwitchLaneTools = 8;
		public int numberOfSpeedTools = 8;

		private bool dragging;
		private Vector3 mouseHitPosition;
		private bool jumpIsBeingPlaced = false;
		private bool switchIsBeingPlaced = false;
		private bool speedIsBeingPlaced = false;

		void Awake(){
			InjectionRegister.Register(this);
		}

		void Start() {
			jumpTools = new GameObject[numberOfJumpTools];
			switchLaneTools = new GameObject[numberOfSwitchLaneTools];
			speedTools = new GameObject[numberOfSpeedTools];

			PoolSystem(jumpTools, jumpPrefab, numberOfJumpTools);
			PoolSystem(switchLaneTools, switchLanePrefab, numberOfSwitchLaneTools);
			PoolSystem(speedTools, speedPrefab, numberOfSpeedTools);
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

		public void OnButtonClickPlaceSpeed() {
			if(numberOfSpeedTools > 0) {
				speedIsBeingPlaced = true;
				dragging = true;
				numberOfSpeedTools--;
				speedTools[numberOfSpeedTools].SetActive(true);
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

				// speed tool
				if ( touch.phase == TouchPhase.Moved && speedIsBeingPlaced ) {
					PlaceObject(speedTools[numberOfSpeedTools], touch.position);
				}
				if ( touch.phase == TouchPhase.Ended && speedIsBeingPlaced ) {
					speedIsBeingPlaced = false;
					speedTools[numberOfSpeedTools].GetComponentInChildren<SphereCollider>().enabled = true;
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

			// speed tool
			if ( Input.GetMouseButton(0) && speedIsBeingPlaced ) {
				PlaceObject(speedTools[numberOfSpeedTools], Input.mousePosition);
			}
			// Release jump to the scene
			if ( Input.GetMouseButtonUp(0) && speedIsBeingPlaced ) {
				speedTools[numberOfSpeedTools].GetComponentInChildren<SphereCollider>().enabled = true;
				speedIsBeingPlaced = false;
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

					snapping.Snap(hit.point, obj.transform);

				}
			}
		}

		private void SetDraggingFalse() {
			dragging = false;
		}

		public bool IsDragged() {
			return dragging;
		}

		public void SetSnap (SnappingToolInterface snapTool) {
			snapping = snapTool;
		}
		public string GetTag () {
			return TagConstants.SNAPPING;
		}
		public void SetupComponents () {
			return;
		}
		public GameObject GetGameObject () {
			return gameObject;
		}
		public Actionable<Assets.scripts.controllers.ControllableActions> GetActionable () {
			return null;
		}
	}
}
