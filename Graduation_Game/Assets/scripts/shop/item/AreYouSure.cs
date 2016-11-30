using UnityEngine;

namespace Assets.scripts.shop.item {
	public class AreYouSure : MonoBehaviour {
		private ShopItem item;

		public void Cancel() {
			Destroy(gameObject);
		}

		public void IAmSure() {
			item.Buy();
			DestroyThis();
		}

		private void DestroyThis() {
			Destroy(gameObject);
		}

		public void SetShopItem(ShopItem item) {
			this.item = item;
		}
	}
}
