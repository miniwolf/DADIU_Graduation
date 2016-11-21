using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.level;
using System;

namespace Assets.scripts.UI.inventory {
	public class Inventory : MonoBehaviour {
		public readonly Item<int> plutonium = new InventoryItem();
		public static readonly Item<int> penguinStorage = new PreferenceItem<int>(InventoryConstants.PENGUINSTORAGE);
		public static readonly Item<int> penguinCount = new PreferenceItem<int>(InventoryConstants.PENGUINCOUNT);
		public static readonly Item<int> hasInitialized = new PreferenceItem<int>(InventoryConstants.HASINITIALIZED);
		public static readonly Item<int> cash = new PreferenceItem<int>(InventoryConstants.CASH);
		public static readonly Item<int> hatchablePenguins = new PreferenceItem<int>(InventoryConstants.HATCHABLEPENGUINS);
		public static readonly Item<string> eggHatchTime = new PreferenceItem<string>(InventoryConstants.EGGTIME);
		public static readonly Item<int> levelUnlockIndex = new PreferenceItem<int>(InventoryConstants.LEVELINDEX);
		public static readonly Item<int> key = new PreferenceItem<int>(InventoryConstants.KEY);
		public static readonly Item<string> loginDate = new PreferenceItem<string>(InventoryConstants.LASTLOGIN);


		static Inventory() {
			// SanityCheck
			Debug.Assert(penguinCount.GetValue() <= penguinStorage.GetValue(), "It looks like you're trying to cheat "
																			   + "the system and get more penguins "
																			   + "than you can.");
			if ( hasInitialized.GetValue() == 0 ) {
				penguinStorage.SetValue(5);
				penguinCount.SetValue(5);
				hasInitialized.SetValue(1);
				key.SetValue(1);
				loginDate.SetValue(DateTime.Now.ToString());
			}

			var stringTime = loginDate.GetValue();
			loginDate.SetValue(DateTime.Now.ToString());

			if ( DateTime.Now.DayOfYear != Convert.ToDateTime(stringTime).DayOfYear ) {
				key.SetValue(key.GetValue() + 1);
			}

		}

		public static void UpdateCount() {
			// get penguins that are alive at the end of the level
			Text penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			int alivePenguinsLevel = int.Parse(penguinCounter.text);
			// get initial number of penguins at the beginning of the level
			GameObject[] penguinSpawners = GameObject.FindGameObjectsWithTag(TagConstants.PENGUIN_SPAWNER);
			int initialPenguinCount = 0;
			// we might have several penguin spawners (one in each lane)
			foreach ( GameObject go in penguinSpawners ) {
				initialPenguinCount += go.GetComponent<PenguinSpawner>().GetInitialPenguinCount();
			}

			// get number of dead penguins
			int deadPenguins = initialPenguinCount - alivePenguinsLevel;
			// update the penguin counter in the inventory
			int inventoryPenguins = Inventory.penguinCount.GetValue();
			Inventory.penguinCount.SetValue(inventoryPenguins - deadPenguins);
		}
	}
}
