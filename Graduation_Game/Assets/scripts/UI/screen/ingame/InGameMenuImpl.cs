using System.Collections.Generic;
using Asset.scripts.tools;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
    public class InGameMenuImpl : UIController, InGameMenu {

        private List<Tool> toolsAvailable;
        private List<InventoryItem> inventoryItemsAvailable;

        void Start() {

        }

        void OnDestroy() {

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
            foreach (var tool in toolsAvailable) {
                switch (tool.GetToolType()) {
                     case ToolType.SwitchLane:
                        break;
                     case ToolType.Jump:
                        break;
                }
            }
        }

        private void HideAllTools() {

        }
    }
}