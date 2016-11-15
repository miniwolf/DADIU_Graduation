namespace Assets.scripts.shop.item {
	public abstract class ShopItemImpl : ShopItem {
		private int price;

		public int GetPrice() {
			return price;
		}

		public void SetPrice(int price) {
			this.price = price;
		}

		public abstract bool Buy();
	}
}
