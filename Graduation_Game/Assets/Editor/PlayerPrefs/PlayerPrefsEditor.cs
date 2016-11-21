using System;
using Assets.scripts.UI.inventory;
using UnityEditor;

namespace Assets.Editor.PlayerPrefs {
	public class PlayerPrefsEditor : EditorWindow {
		private string value;
		private readonly Item<int> penguinCount = Inventory.penguinCount;
		private readonly Item<int> hasInitialized = Inventory.hasInitialized;
		private readonly Item<int> penguinStorage  = Inventory.penguinStorage;
		private readonly Item<string> eggTime = Inventory.eggHatchTime;
		private readonly Item<int> cash = Inventory.cash;
		private readonly Item<int> key = Inventory.key;
		private readonly Item<string> loginDate = Inventory.loginDate;

		[MenuItem("Window/Player Prefs Editor")]
		private static void Init() {
			var editor = (PlayerPrefsEditor) GetWindow(typeof(PlayerPrefsEditor));

			var minSize = editor.minSize;
			minSize.x = 230;
			editor.minSize = minSize;
		}

		private void OnGUI() {
			DrawEntry(InventoryConstants.HASINITIALIZED, hasInitialized);
			DrawEntry(InventoryConstants.PENGUINCOUNT, penguinCount);
			DrawEntry(InventoryConstants.PENGUINSTORAGE, penguinStorage);
			eggTime.SetValue(EditorGUILayout.TextField(InventoryConstants.EGGTIME, eggTime.GetValue()));
			DrawEntry(InventoryConstants.CASH, cash);
			DrawEntry(InventoryConstants.KEY, key);
			loginDate.SetValue(EditorGUILayout.TextField(InventoryConstants.LASTLOGIN, loginDate.GetValue()));
		}

		private void DrawEntry(string s, Item<int> item) {
			value = EditorGUILayout.TextField(s, item.GetValue().ToString());
			if ( "".Equals(value) ) {
				return;
			}
			item.SetValue(Convert.ToInt32(value));
		}
	}
}
