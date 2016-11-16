using Assets.scripts.UI.inventory;
using UnityEngine;

namespace Assets.scripts.controllers.actions.pickups {
	public class HatchEgg : Action {
		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			Inventory.penguinCount.SetValue(Inventory.penguinCount.GetValue() + 1);
			Inventory.eggCount.SetValue(Inventory.eggCount.GetValue() - 1);
		}
	}
}
