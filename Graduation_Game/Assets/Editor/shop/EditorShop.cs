using System;
using System.Collections.Generic;
using Assets.scripts.shop;
using Assets.scripts.shop.item;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.shop {
	[CustomEditor(typeof(Shop))]
	public class EditorShop : UnityEditor.Editor {
		private readonly Dictionary<string, ShopItem> newItems = new Dictionary<string, ShopItem>();
		private readonly List<string> toBeRemoved = new List<string>();

		public override void OnInspectorGUI() {
			var shop = target as Shop;
			var items = shop.GetItems();
			foreach (var key in items.Keys) {
				var newName = CreateEntry(0, key, items[key]);
				if ( newName == key ) {
					continue;
				}
				toBeRemoved.Add(key);
				newItems.Add(newName, items[key]);
			}

			if ( toBeRemoved.Count == 0 ) {
				return;
			}
			UpdateItems(items);
		}

		private void UpdateItems(IDictionary<string, ShopItem> items) {
			foreach ( var s in toBeRemoved ) {
				items.Remove(s);
			}
			foreach (var keyValuePair in newItems) {
				items.Add(keyValuePair);
			}
		}

		private static string CreateEntry(int i, string name, ShopItem item) {
			EditorGUILayout.LabelField(i.ToString());
			var newName = EditorGUILayout.TextField("Name", name);
			var newPrice = EditorGUILayout.TextField("Price", item.GetPrice().ToString());
			item.SetPrice(Convert.ToInt32(newPrice));
			return newName;
		}
	}
}
