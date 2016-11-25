using System;
using Assets.scripts.UI.inventory;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.PlayerPrefs {
	public class PlayerPrefsEditor : EditorWindow {
		private string value;

		private readonly Item<string> levelPlayed = Inventory.levelPlayed;
		private readonly Item<int> collectedStars = Inventory.collectedStars;
		private readonly Item<int> levelIndex = Inventory.levelUnlockIndex;
		private readonly Item<int> penguinCount = Inventory.penguinCount;
		private readonly Item<int> hasInitialized = Inventory.hasInitialized;
		private readonly Item<int> penguinStorage  = Inventory.penguinStorage;
		private readonly Item<string> eggTime = Inventory.eggHatchTime;
		private readonly Item<int> cash = Inventory.cash;
		private readonly Item<int> key = Inventory.key;
		private readonly Item<string> loginDate = Inventory.loginDate;
		private readonly Item<int> level1 = Inventory.level1;
		private readonly Item<int> level2 = Inventory.level2;
		private readonly Item<int> level3 = Inventory.level3;
		private readonly Item<int> level4 = Inventory.level4;
		private readonly Item<int> level5 = Inventory.level5;
		private readonly Item<int> level6 = Inventory.level6;
		private readonly Item<int> level7 = Inventory.level7;
		private readonly Item<int> level8 = Inventory.level8;
		private readonly Item<int> level9 = Inventory.level9;
		private readonly Item<int> level10 = Inventory.level10;

		[MenuItem("Window/Player Prefs Editor")]
		private static void Init() {
			var editor = (PlayerPrefsEditor) GetWindow(typeof(PlayerPrefsEditor));

			var minSize = editor.minSize;
			minSize.x = 230;
			editor.minSize = minSize;
		}

		private void OnGUI() {
			levelPlayed.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVELPLAYED, levelPlayed.GetValue()));
			DrawEntry(InventoryConstants.COLLECTEDSTARS, collectedStars);
			DrawEntry(InventoryConstants.HASINITIALIZED, hasInitialized);
			DrawEntry(InventoryConstants.LEVELINDEX, levelIndex);
			DrawEntry(InventoryConstants.PENGUINCOUNT, penguinCount);
			DrawEntry(InventoryConstants.PENGUINSTORAGE, penguinStorage);
			eggTime.SetValue(EditorGUILayout.TextField(InventoryConstants.EGGTIME, eggTime.GetValue()));
			DrawEntry(InventoryConstants.CASH, cash);
			DrawEntry(InventoryConstants.KEY, key);
			loginDate.SetValue(EditorGUILayout.TextField(InventoryConstants.LASTLOGIN, loginDate.GetValue()));
			DrawEntry(InventoryConstants.LEVEL1, level1);
			DrawEntry(InventoryConstants.LEVEL2, level2);
			DrawEntry(InventoryConstants.LEVEL3, level3);
			DrawEntry(InventoryConstants.LEVEL4, level4);
			DrawEntry(InventoryConstants.LEVEL5, level5);
			DrawEntry(InventoryConstants.LEVEL6, level6);
			DrawEntry(InventoryConstants.LEVEL7, level7);
			DrawEntry(InventoryConstants.LEVEL8, level8);
			DrawEntry(InventoryConstants.LEVEL9, level9);
			DrawEntry(InventoryConstants.LEVEL10, level10);


			if (GUILayout.Button("Delete playerprefs for secretLevel")) {
				UnityEngine.PlayerPrefs.DeleteKey("hasVisited");
				UnityEngine.PlayerPrefs.DeleteKey("backFromSecret");
			}
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
