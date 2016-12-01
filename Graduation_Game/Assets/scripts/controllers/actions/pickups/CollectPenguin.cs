using Assets.scripts.UI.inventory;
using UnityEngine;

namespace Assets.scripts.controllers.actions.pickups {
	public class CollectPenguin : Action {
		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			Inventory.penguinCount.SetValue(Inventory.penguinCount.GetValue() + 1);
		}
	}
}
