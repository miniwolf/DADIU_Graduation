using System;
using System.Collections.Generic;
using Assets.scripts.UI.inventory;

namespace Assets.scripts.shop.item {
	public class BuyPenguin : ShopItemImpl {
		private readonly Item<int> penguinCount = Inventory.penguinCount;
		private readonly Item<int> hatchablePenguins = Inventory.hatchablePenguins;
		private readonly Item<int> penguinStock = Inventory.penguinStorage;
		private readonly Item<int> eggCount = Inventory.eggCount;
		private readonly Item<Dictionary<string, PenguinEgg>> eggs = Inventory.penguinEggs;

		public override bool Buy() {
			if ( penguinStock.GetValue() == penguinCount.GetValue() + eggCount.GetValue() ) {
				return false; // Cannot buy more penguins than there is stock
			}

			var penguinEgg = new PenguinEgg();
			var newGuid = Guid.NewGuid();
			penguinEgg.ID = newGuid.ToString();
			penguinEgg.HatchTime = DateTime.Now;
			eggCount.SetValue(Inventory.eggCount.GetValue() + 1);
			return true;
		}
	}
}
