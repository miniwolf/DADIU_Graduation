using System;
using Assets.scripts.shop.item;
using Assets.scripts.UI.inventory;
using UnityEngine;

namespace Assets.scripts.controllers.actions.pickups {
	public class HatchEgg : Action {
		private GameObject go;

		public void Setup(GameObject gameObject) {
			go = gameObject;
		}

		public void Execute() {
			Inventory.penguinCount.SetValue(Inventory.penguinCount.GetValue() + 1);
			var penguinEgg = go.GetComponent<PenguinEgg>();
			penguinEgg.HatchTime = DateTime.Now.AddMinutes(30);
			penguinEgg.hatchable = false;
			penguinEgg.HideButton();
		}
	}
}
