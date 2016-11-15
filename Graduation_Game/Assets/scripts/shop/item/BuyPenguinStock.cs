using Assets.scripts.UI.inventory;

namespace Assets.scripts.shop.item {
	public class BuyPenguinStock : ShopItemImpl {
		private readonly Item<int> stockCount = Inventory.penguinStorage;

		public override bool Buy() {
			stockCount.SetValue(stockCount.GetValue() + 1);
			return true;
		}
	}
}
