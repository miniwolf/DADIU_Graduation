using System.Collections.Generic;
using Asset.scripts.tools;
using Assets.scripts.controllers.actions.movement.sound;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
    public interface InGameMenu {
        /// <summary>
        /// Initialize inventory items you want to have on the screen
        /// </summary>
        /// <param name="items"></param>
        void InventoryItemsAvaliable(List<InventoryItem> items);
//        void InventoryItemEnable(InventoryItem item);
//        void InventoryItemDisable(InventoryItem item);

        void ToolsAvailable(List<Tool> tools);
//        void ToolDisable(Tool tool);
//        void ToolEnable(Tool tool);
    }
}