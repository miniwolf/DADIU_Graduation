using System;
using System.Collections.Generic;
using Assets.scripts.shop.item;
using Assets.scripts.sound;
using Assets.scripts.UI.inventory;
using Assets.scripts.UI.translations;
using UnityEngine;

namespace Assets.scripts.shop {
	public class Shop : MonoBehaviour, LanguageChangeListener {
		private readonly Dictionary<string, ShopItem> items = new Dictionary<string, ShopItem>();
		private Item<int> cash;

		public enum Items {
			Penguin, PenguinStock, RetryKey
		}

		[Serializable]
		public class Item {
			public Items item;
			public int price;
		}

		public Item[] storeItems;

		protected void Start() {
		    Debug.Log(gameObject);
		    cash = Inventory.cash;
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
					case Items.RetryKey:
						buy = new BuyRetryKey();
						items.Add("Key", buy);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				buy.SetPrice(storeItem.price);
			}
		}

		public void Purchase(string item) {
			AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.BUTTON_PRESS, gameObject);
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

	    public void OnLanguageChange(SupportedLanguage newLanguage) {


	    }
	}
}
