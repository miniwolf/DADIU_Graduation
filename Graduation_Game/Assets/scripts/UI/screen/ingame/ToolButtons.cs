using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.components.registers;

namespace Assets.scripts.UI.screen.ingame {
	public class ToolButtons : MonoBehaviour, GameEntity, Draggable, SetSnappingTool {

		private SnappingToolInterface snapping;
		private InputManager inputManager;

		public GameObject jumpPrefab;
		public GameObject switchLanePrefab;
		public GameObject speedPrefab;
		public GameObject enlargePrefab;
		public GameObject minimizePrefab;
		public GameObject bridgePrefab;

		private GameObject[] jumpTools;
		private GameObject[] switchLaneTools;
		private GameObject[] speedTools;
		private GameObject[] enlargeTools;
		private GameObject[] minimizeTools;
		private GameObject[] bridgeTools;
		public int numberOfJumpTools = 8;
		public int numberOfSwitchLaneTools = 8;
		public int numberOfSpeedTools = 8;
		public int numberOfEnlargeTools = 8;
		public int numberOfMinimizeTools = 8;
		public int numberOfBridgeTools = 8;

		private bool dragging;
		private Vector3 mouseHitPosition;
		private bool jumpIsBeingPlaced = false;
		private bool switchIsBeingPlaced = false;
		private bool speedIsBeingPlaced = false;
		private bool enlargeIsBeingPlaced = false;
		private bool minimizeIsBeingPlaced = false;
		private bool bridgeIsBeingPlaced = false;

		void Awake(){
			InjectionRegister.Register(this);
		}

		void Start() {
			
			jumpTools = new GameObject[numberOfJumpTools];
			switchLaneTools = new GameObject[numberOfSwitchLaneTools];
			speedTools = new GameObject[numberOfSpeedTools];
			enlargeTools = new GameObject[numberOfEnlargeTools];
			minimizeTools = new GameObject[numberOfMinimizeTools];
			bridgeTools = new GameObject[numberOfBridgeTools];

			PoolSystem(jumpTools, jumpPrefab, numberOfJumpTools);
			PoolSystem(switchLaneTools, switchLanePrefab, numberOfSwitchLaneTools);
			PoolSystem(speedTools, speedPrefab, numberOfSpeedTools);
			PoolSystem(enlargeTools, enlargePrefab, numberOfEnlargeTools);
			PoolSystem(minimizeTools, minimizePrefab, numberOfMinimizeTools);
			PoolSystem(bridgeTools, bridgePrefab, numberOfBridgeTools);
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

		public void OnButtonClickPlaceEnlarge() {
			if(numberOfEnlargeTools > 0) {
				enlargeIsBeingPlaced = true;
				dragging = true;
				numberOfEnlargeTools--;
				enlargeTools[numberOfEnlargeTools].SetActive(true);
			}
		}

		public void OnButtonClickPlaceMinimize() {
			if(numberOfMinimizeTools > 0) {
				minimizeIsBeingPlaced = true;
				dragging = true;
				numberOfMinimizeTools--;
				minimizeTools[numberOfMinimizeTools].SetActive(true);
			}
		}

		public void OnButtonClickPlaceBridge() {
			if (numberOfBridgeTools > 0) {
				bridgeIsBeingPlaced = true;
				dragging = true;
				numberOfBridgeTools--;
				bridgeTools[numberOfBridgeTools].SetActive(true);
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
					switchLaneTools[numberOfSwitchLaneTools].GetComponentInChildren<Collider>().enabled = true;
					SetDraggingFalse();
				}

				// jump tool
				if ( touch.phase == TouchPhase.Moved && jumpIsBeingPlaced ) {
					PlaceObject(jumpTools[numberOfJumpTools], touch.position);
				}
				if ( touch.phase == TouchPhase.Ended && jumpIsBeingPlaced ) {
					jumpIsBeingPlaced = false;
					jumpTools[numberOfJumpTools].GetComponentInChildren<Collider>().enabled = true;
					SetDraggingFalse();
				}

				// speed tool
				if ( touch.phase == TouchPhase.Moved && speedIsBeingPlaced ) {
					PlaceObject(speedTools[numberOfSpeedTools], touch.position);
				}
				if ( touch.phase == TouchPhase.Ended && speedIsBeingPlaced ) {
					speedIsBeingPlaced = false;
					speedTools[numberOfSpeedTools].GetComponentInChildren<Collider>().enabled = true;
					SetDraggingFalse();
				}

				// enlarge tool
				if ( touch.phase == TouchPhase.Moved && enlargeIsBeingPlaced ) {
					PlaceObject(enlargeTools[numberOfEnlargeTools], touch.position);
				}
				if ( touch.phase == TouchPhase.Ended && enlargeIsBeingPlaced ) {
					enlargeIsBeingPlaced = false;
					enlargeTools[numberOfEnlargeTools].GetComponentInChildren<Collider>().enabled = true;
					SetDraggingFalse();
				}

				// minimize tool
				if ( touch.phase == TouchPhase.Moved && minimizeIsBeingPlaced ) {
					PlaceObject(minimizeTools[numberOfMinimizeTools], touch.position);
				}
				if ( touch.phase == TouchPhase.Ended && minimizeIsBeingPlaced ) {
					minimizeIsBeingPlaced = false;
					minimizeTools[numberOfMinimizeTools].GetComponentInChildren<Collider>().enabled = true;
					SetDraggingFalse();
				}

				// bridge tool
				if (touch.phase == TouchPhase.Moved && bridgeIsBeingPlaced) {
					PlaceObject(bridgeTools[numberOfBridgeTools], touch.position);
				}
				if (touch.phase == TouchPhase.Ended && bridgeIsBeingPlaced) {
					bridgeIsBeingPlaced = false;
					bridgeTools[numberOfBridgeTools].GetComponentInChildren<Collider>().enabled = true;
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
				switchLaneTools[numberOfSwitchLaneTools].GetComponentInChildren<Collider>().enabled = true;
				SetDraggingFalse();
			}

			// jump tool
			if ( Input.GetMouseButton(0) && jumpIsBeingPlaced ) {
				PlaceObject(jumpTools[numberOfJumpTools], Input.mousePosition);
			}
			// Release jump to the scene
			if ( Input.GetMouseButtonUp(0) && jumpIsBeingPlaced ) {
				jumpTools[numberOfJumpTools].GetComponentInChildren<Collider>().enabled = true;
				jumpIsBeingPlaced = false;
				SetDraggingFalse();
			}

			// speed tool
			if ( Input.GetMouseButton(0) && speedIsBeingPlaced ) {
				
				PlaceObject(speedTools[numberOfSpeedTools], Input.mousePosition);
			}
			// Release speed to the scene
			if ( Input.GetMouseButtonUp(0) && speedIsBeingPlaced ) {
				speedTools[numberOfSpeedTools].GetComponentInChildren<Collider>().enabled = true;
				speedIsBeingPlaced = false;
				SetDraggingFalse();
			}

			// enlarge tool
			if ( Input.GetMouseButton(0) && enlargeIsBeingPlaced ) {
				PlaceObject(enlargeTools[numberOfEnlargeTools], Input.mousePosition);
			}
			// Release enlarge to the scene
			if ( Input.GetMouseButtonUp(0) && enlargeIsBeingPlaced ) {
				enlargeTools[numberOfEnlargeTools].GetComponentInChildren<Collider>().enabled = true;
				enlargeIsBeingPlaced = false;
				SetDraggingFalse();
			}

			// minimize tool
			if ( Input.GetMouseButton(0) && minimizeIsBeingPlaced ) {
				PlaceObject(minimizeTools[numberOfMinimizeTools], Input.mousePosition);
			}
			// Release minimize to the scene
			if ( Input.GetMouseButtonUp(0) && minimizeIsBeingPlaced ) {
				minimizeTools[numberOfMinimizeTools].GetComponentInChildren<Collider>().enabled = true;
				minimizeIsBeingPlaced = false;
				SetDraggingFalse();
			}

			// bridge tool
			if (Input.GetMouseButton(0) && bridgeIsBeingPlaced) {
				PlaceObject(bridgeTools[numberOfBridgeTools], Input.mousePosition);
			}
			// Release bridge to the scene
			if (Input.GetMouseButtonUp(0) && bridgeIsBeingPlaced) {
				bridgeTools[numberOfBridgeTools].GetComponentInChildren<Collider>().enabled = true;
				bridgeIsBeingPlaced = false;
				SetDraggingFalse();
			}
		}

		private void PlaceObject(GameObject obj, Vector3 position) {
			inputManager.BlockCameraMovement();
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
			inputManager.UnblockCameraMovement();
		}

		public bool IsDragged() {
			return dragging;
		}

		public void SetSnap (SnappingToolInterface snapTool) {
			snapping = snapTool;
		}

		public void SetInputManager(InputManager inputManage){
			this.inputManager = inputManage;
		}

		public string GetTag () {
			return TagConstants.TOOLBUTTON;
		}
		public void SetupComponents () {
			return;
		}
		public GameObject GetGameObject () {
			return gameObject;
		}
		public Actionable<T> GetActionable<T>() {
			return null;
		}
	}
}
