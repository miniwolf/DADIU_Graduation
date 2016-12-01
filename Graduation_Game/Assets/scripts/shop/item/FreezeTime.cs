using Assets.scripts.UI.inventory;

namespace Assets.scripts.shop.item {
	public class FreezeTime : ShopItemImpl {
		private readonly Item<int> freezeTime = Inventory.numberOfFreezeTime;

		public override bool Buy() {
			freezeTime.SetValue(freezeTime.GetValue() + 1);
			return true;
		}
	}
}
