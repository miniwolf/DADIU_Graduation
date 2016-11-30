using Assets.scripts.UI.inventory;

namespace Assets.scripts.shop.item {
	public class BuyExtraEgg : ShopItemImpl {
		private readonly Item<int> eggCount = Inventory.eggCount;

		public override bool Buy() {
			eggCount.SetValue(eggCount.GetValue() + 1);
			return true;
		}
	}
}
