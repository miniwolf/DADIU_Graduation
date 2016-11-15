﻿using System;
using System.Collections.Generic;
using Assets.scripts.shop.item;
using Assets.scripts.UI.inventory;
using UnityEngine;

namespace Assets.scripts.shop {
	public class Shop : MonoBehaviour {
		private readonly Dictionary<string, ShopItem> items = new Dictionary<string, ShopItem>();
		private readonly Item<int> cash = Inventory.cash;

		public enum Items {
			Penguin, PenguinStock
		}

		[Serializable]
		public class Item {
			public Items item;
			public int price;
		}

		public Item[] storeItems;

		protected void Start() {
			foreach ( var storeItem in storeItems ) {
				ShopItem buy;
				switch (storeItem.item) {
					case Items.Penguin:
						buy = new BuyPenguin();
						items.Add("Penguin", buy);
						break;
					case Items.PenguinStock:
						buy = new BuyPenguinStock();
						items.Add("Stock", buy);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				buy.SetPrice(storeItem.price);
			}
		}

		public void Purchase(string item) {
			var shopItem = items[item];
			if ( shopItem.GetPrice() > cash.GetValue()) {
				Debug.Log("You do not have enough cash");
				return;
			}

			if ( shopItem.Buy() ) {
				cash.SetValue(cash.GetValue() - shopItem.GetPrice());
			} else {
				Debug.Log("could not buy");
			}
		}

		public Dictionary<string, ShopItem> GetItems() {
			return items;
		}
	}
}
