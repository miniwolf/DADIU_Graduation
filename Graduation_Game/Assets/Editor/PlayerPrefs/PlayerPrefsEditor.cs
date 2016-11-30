﻿using System;
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
		private readonly Item<int> level1stars = Inventory.level1stars;
		private readonly Item<int> level2stars = Inventory.level2stars;
		private readonly Item<int> level3stars = Inventory.level3stars;
		private readonly Item<int> level4stars = Inventory.level4stars;
		private readonly Item<int> level5stars = Inventory.level5stars;
		private readonly Item<int> level6stars = Inventory.level6stars;
		private readonly Item<int> level7stars = Inventory.level7stars;
		private readonly Item<int> level8stars = Inventory.level8stars;
		private readonly Item<int> level9stars = Inventory.level9stars;
		private readonly Item<int> level10stars = Inventory.level10stars;
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
			DrawEntry(InventoryConstants.LEVEL1STARS, level1stars);
			DrawEntry(InventoryConstants.LEVEL2STARS, level2stars);
			DrawEntry(InventoryConstants.LEVEL3STARS, level3stars);
			DrawEntry(InventoryConstants.LEVEL4STARS, level4stars);
			DrawEntry(InventoryConstants.LEVEL5STARS, level5stars);
			DrawEntry(InventoryConstants.LEVEL6STARS, level6stars);
			DrawEntry(InventoryConstants.LEVEL7STARS, level7stars);
			DrawEntry(InventoryConstants.LEVEL8STARS, level8stars);
			DrawEntry(InventoryConstants.LEVEL9STARS, level9stars);
			DrawEntry(InventoryConstants.LEVEL10STARS, level10stars);

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


			if (GUILayout.Button("Reset level progression values")) {
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVELPLAYED);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.COLLECTEDSTARS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.TOTALSTARS);

				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVELINDEX);

				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL1STARS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL2STARS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL3STARS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL4STARS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL5STARS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL6STARS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL7STARS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL8STARS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL9STARS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL10STARS);

				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL1STATUS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL2STATUS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL3STATUS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL4STATUS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL5STATUS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL6STATUS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL7STATUS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL8STATUS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL9STATUS);
				UnityEngine.PlayerPrefs.DeleteKey(InventoryConstants.LEVEL10STATUS);
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
