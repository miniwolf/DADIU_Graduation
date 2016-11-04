using System.Collections.Generic;
using System.Collections;
using Asset.scripts.tools;
using UnityEngine;


namespace Assets.scripts.UI.screen.ingame {
	public class InGameMenuImpl : UIController, InGameMenu {

		private List<Tool> toolsAvailable;
		private List<InventoryItem> inventoryItemsAvailable;

		private GameObject switchLaneToolObjectOriginal, // template from which we create switchLaneToolObjectCurrentlyDragging
			switchLaneToolObjectCurrentlyDragging; // actually dragged object

		private bool dragging = false;
	
		public override void ResolveDependencies() {
			switchLaneToolObjectOriginal = GameObject.FindGameObjectWithTag(TagConstants.UI.IN_GAME_TOOL_SWITCH_LANE) as GameObject; 

			// code below is just for internal UI testing. Call this from somewhere else
			Tool t = switchLaneToolObjectOriginal.GetComponent<Tool>();
			List<Tool> tls = new List<Tool>();
			tls.Add(t);
			// especially this part
			ToolsAvailable(tls);
			DrawTools();
		}

		public override void RefreshText() {

		}

		void Update() {
			if(Input.GetMouseButton(0)) {
				if(!dragging) { // if not dragging, check if user wants to drag something
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();

					if(Physics.Raycast(ray, out hit)) { // button click raycast hits object from main menu
						if(hit.transform.tag.Equals(TagConstants.UI.IN_GAME_TOOL_SWITCH_LANE)) {
							dragging = true;
							// create new object which will be placed on the scene
							switchLaneToolObjectCurrentlyDragging = GameObject.Instantiate(switchLaneToolObjectOriginal); 
							switchLaneToolObjectOriginal.SetActive(false);
						} 
					} 
				} else { // we are in the drag mode (an active object is dragged)
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();

					if(Physics.Raycast(ray, out hit)) {
						if(!hit.transform.tag.Equals(TagConstants.UI.IN_GAME_TOOL_SWITCH_LANE)) {
							switchLaneToolObjectCurrentlyDragging.transform.position = hit.point;
						}
					}
				}
			} else {
				if(dragging) { // end of dragging
					dragging = false;
					switchLaneToolObjectCurrentlyDragging = null;
					switchLaneToolObjectOriginal.SetActive(true);
				}
			}
		}

		public void InventoryItemsAvaliable(List<InventoryItem> items) {
			inventoryItemsAvailable = items;
			DrawInventoryItems();
		}

		public void ToolsAvailable(List<Tool> tools) {
			toolsAvailable = tools;
			DrawTools();
		}

		void DrawInventoryItems() {
			// todo 
		}

		/// <summary>
		/// Draw tools that are present, disable others
		/// </summary>
		private void DrawTools() {
			HideAllTools();
			foreach(var tool in toolsAvailable) {
				switch(tool.GetToolType()) {
				case ToolType.SwitchLane:
					switchLaneToolObjectOriginal.SetActive(true);
					break;
				case ToolType.Jump:
					// todo
					break;
				}
			}
		}

		private void HideAllTools() {
			switchLaneToolObjectOriginal.SetActive(false);
		}
	}
}