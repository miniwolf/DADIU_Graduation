using System.Collections.Generic;
using Assets.scripts.shop.item;
using Assets.scripts.UI.inventory;
using UnityEngine;

namespace Assets.scripts.shop {
	public class Shop : MonoBehaviour {
		private static readonly Dictionary<string, ShopItem> items = new Dictionary<string, ShopItem>();

		static Shop() {
			items.Add("Penguin", new BuyPenguin());
			items.Add("Stock", new BuyPenguinStock());
		}

		private readonly Item<int> cash = Inventory.cash;

		public void Purchase(string item) {
			var shopItem = items[item];

			if ( shopItem.GetPrice() > cash.GetValue()) {
				// Cannot purchase
				return;
			}

			if ( shopItem.Buy() ) {
				cash.SetValue(cash.GetValue() - shopItem.GetPrice());
			}
		}

		public Dictionary<string, ShopItem> GetItems() {
			return items;
		}
	}
}
