namespace Assets.scripts.shop.item {
	public interface ShopItem {
		int GetPrice();
		bool Buy();
		void SetPrice(int newPrice);
	}
}