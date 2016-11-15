﻿using Assets.scripts.UI.inventory;

namespace Assets.scripts.shop.item {
	public class BuyPenguin : ShopItemImpl {
		private readonly Item<int> penguinCount = Inventory.penguinCount;
		private readonly Item<int> hatchablePenguins = Inventory.hatchablePenguins;
		private readonly Item<int> penguinStock = Inventory.penguinStorage;

		public override bool Buy() {
			if ( penguinStock.GetValue() == penguinCount.GetValue() ) {
				return false; // Cannot buy more penguins than there is stock
			}

			penguinCount.SetValue(Inventory.penguinCount.GetValue() + 1);
			return true;
		}
	}
}