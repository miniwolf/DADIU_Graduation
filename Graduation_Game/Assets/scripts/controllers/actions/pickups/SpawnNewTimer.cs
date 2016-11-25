using System;
using System.Globalization;
using Assets.scripts.eggHatching;
using Assets.scripts.shop.item;
using Assets.scripts.UI.inventory;
using UnityEngine;

namespace Assets.scripts.controllers.actions.pickups {
	public class SpawnNewTimer : Action {
		private EggCollection collection;
		private PenguinEgg penguinEgg;

		public void Setup(GameObject gameObject) {
			penguinEgg = gameObject.GetComponent<PenguinEgg>();
			collection = GameObject.FindGameObjectWithTag(TagConstants.EGG_COLLECTION).GetComponent<EggCollection>();
		}

		public void Execute() {
			var penguins = Inventory.penguinCount.GetValue();
			var hatching = Inventory.eggHatchTime.GetValue().Split(',').Length - 1;
			if ( penguins + hatching == Inventory.penguinStorage.GetValue() ) {
				return;
			}
			if ( penguins + hatching > Inventory.penguinStorage.GetValue() ) {
				Debug.LogError("Looks like you're trying to cheat the system by getting more penguins than you have storage for.");
				Inventory.penguinCount.SetValue(0);
			}
			var newTime = DateTime.Now.AddMinutes(30).ToString(CultureInfo.InvariantCulture);
			collection.AddEgg(newTime);
			var splitText = Inventory.eggHatchTime.GetValue().Split(',');
			splitText[penguinEgg.Idx] = newTime;
			Inventory.eggHatchTime.SetValue(string.Join(",", splitText));
		}
	}
}
