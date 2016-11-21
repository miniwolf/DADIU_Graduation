using Assets.scripts.UI.inventory;

namespace Assets.scripts.shop.item {
	public class BuyRetryKey : ShopItemImpl {
		private readonly Item<int> keys = Inventory.key;

		public override bool Buy() {
			keys.SetValue(keys.GetValue() + 1);
			return true;
		}
	}
}
