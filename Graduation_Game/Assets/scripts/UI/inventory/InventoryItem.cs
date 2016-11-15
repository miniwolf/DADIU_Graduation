namespace Assets.scripts.UI.inventory {
	public class InventoryItem : Item<int> {
		public int amount;

		/// <summary>
		/// Increases the amount by a given value. If value not specified it will be increased by 1.
		/// </summary>
		/// <param name="value">(Optional) Value for increase amount.</param>
		public void IncreaseAmount(int value = 1) {
			amount += value;
		}

		/// <summary>
		/// Decreases the amount by a given value. If value not specified it will be decreased by 1.
		/// </summary>
		/// <param name="value">(Optional) Value for decrease amount.</param>
		public void DecreaseAmount(int value = 1) {
			amount -= value;
		}

		/// <summary>
		/// Gets the amount.
		/// </summary>
		/// <returns>The amount.</returns>
		public int GetValue() {
			return amount;
		}

		public void SetValue(int value) {
			amount = value;
		}
	}
}
