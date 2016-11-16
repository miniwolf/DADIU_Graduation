using System;
using Assets.scripts.UI.inventory;

namespace Assets.scripts.shop.item {
	public class BuyPenguin : ShopItemImpl {
		private readonly Item<int> penguinCount = Inventory.penguinCount;
		private readonly Item<int> penguinStock = Inventory.penguinStorage;

		public override bool Buy() {
			if ( penguinStock.GetValue() == penguinCount.GetValue() ) {
				return false; // Cannot buy more penguins than there is stock
			}

			var penguin = new HatchablePenguin();
			PenguinCollection.penguinsReady.Add(penguin);
			return true;
		}
	}
}
