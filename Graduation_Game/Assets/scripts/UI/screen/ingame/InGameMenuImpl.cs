using System.Collections.Generic;
using Asset.scripts.tools;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
	public class InGameMenuImpl : UIController, InGameMenu {

		private List<Tool> toolsAvailable;
		private List<InventoryItem> inventoryItemsAvailable;

		private GameObject switchLaneToolObject;

		private bool dragging = false;
//		private Transform currentlyDragging;

		void Start() {
			switchLaneToolObject = GameObject.FindGameObjectWithTag("InGameMenuToolSwitchLane") as GameObject; 
			switchLaneToolObject.transform.LookAt(Camera.main.transform.position);
		}

		void Update() {
			if(Input.GetMouseButton(0)) {
				if(!dragging) { // check if user wants to drag somethign
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();

					if(Physics.Raycast(ray, out hit)) {
						Debug.Log(hit.transform.tag);
						if(hit.transform.tag.Equals("InGameMenuToolSwitchLane")) {
							dragging = true;
							switchLaneToolObject = GameObject.Instantiate(switchLaneToolObject);
//							currentlyDragging = hit.transform;
						} 
					} 
				} else { // we are in the drag mode
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();

					if(Physics.Raycast(ray, out hit)) {
						if(!hit.transform.tag.Equals("InGameMenuToolSwitchLane")) {
//							GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
							switchLaneToolObject.transform.position = hit.point;
//							g.transform.position = Camera.main.WorldToScreenPoint(hit.point);

//							float distance_to_screen = Camera.main.WorldToScreenPoint(g.transform.position).z * 10;
//							g.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
//							g.transform.position = hit.transform.position;
						}
					}
//					currentlyDragging.position = newPos;
				}
			} else {
				if(dragging) { // end of dragging
					dragging = false;	
				}
			}

//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit = new RaycastHit();
//			
//			if(Physics.Raycast(ray, out hit)) {
//				if(!hit.transform.tag.Equals("InGameMenuToolSwitchLane")) {
//					Debug.Log(hit.transform.tag);
//					switchLaneToolObject.transform.position = hit.point;
//				}
//			}
		}

		public override void RefreshText() {

		}

		public override void ResolveDependencies() {

		}

		public void InventoryItemsAvaliable(List<InventoryItem> items) {
			inventoryItemsAvailable = items;
		}

		public void ToolsAvailable(List<Tool> tools) {
			toolsAvailable = tools;
		}

		private void DrawTools() {
			HideAllTools();
			foreach(var tool in toolsAvailable) {
				switch(tool.GetToolType()) {
				case ToolType.SwitchLane:
					break;
				case ToolType.Jump:
					break;
				}
			}
		}

		private void HideAllTools() {

		}

		void SetLayerRecursively(GameObject o, int layer) {
			foreach (Transform t in o.GetComponentsInChildren<Transform>(true))
				t.gameObject.layer = layer;
		}
	}
}