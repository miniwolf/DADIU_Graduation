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
		private readonly Item<string> level1status = Inventory.level1status;
		private readonly Item<string> level2status = Inventory.level2status;
		private readonly Item<string> level3status = Inventory.level3status;
		private readonly Item<string> level4status = Inventory.level4status;
		private readonly Item<string> level5status = Inventory.level5status;
		private readonly Item<string> level6status = Inventory.level6status;
		private readonly Item<string> level7status = Inventory.level7status;
		private readonly Item<string> level8status = Inventory.level8status;
		private readonly Item<string> level9status = Inventory.level9status;
		private readonly Item<string> level10status = Inventory.level10status;

		private readonly Item<int> totalStars = Inventory.totalStars;

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
			DrawEntry(InventoryConstants.LEVEL10, level10);

			DrawEntry(InventoryConstants.TOTALSTARS, totalStars);

			level1status.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVEL1STATUS, level1status.GetValue()));
			level2status.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVEL2STATUS, level2status.GetValue()));
			level3status.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVEL3STATUS, level3status.GetValue()));
			level4status.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVEL4STATUS, level4status.GetValue()));
			level5status.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVEL5STATUS, level5status.GetValue()));
			level6status.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVEL6STATUS, level6status.GetValue()));
			level7status.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVEL7STATUS, level7status.GetValue()));
			level8status.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVEL8STATUS, level8status.GetValue()));
			level9status.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVEL9STATUS, level9status.GetValue()));
			level10status.SetValue(EditorGUILayout.TextField(InventoryConstants.LEVEL10STATUS, level10status.GetValue()));

		
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
