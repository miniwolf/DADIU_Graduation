using UnityEngine;

namespace Assets.scripts.UI.inventory {
	public class Inventory : MonoBehaviour {
		public readonly Item<int> plutonium = new InventoryItem();
		public static readonly Item<int> penguinStorage = new PreferenceItem<int>(InventoryConstants.PENGUINSTORAGE);
		public static readonly Item<int> penguinCount = new PreferenceItem<int>(InventoryConstants.PENGUINCOUNT);
		public static readonly Item<int> hasInitialized = new PreferenceItem<int>(InventoryConstants.HASINITIALIZED);
		public static readonly Item<int> cash = new PreferenceItem<int>(InventoryConstants.CASH);
		public static readonly Item<int> hatchablePenguins = new PreferenceItem<int>(InventoryConstants.HATCHABLEPENGUINS);

		public void Start() {
			// SanityCheck
			Debug.Assert(penguinCount.GetValue() <= penguinStorage.GetValue(), "It looks like you're trying to cheat "
																			   + "the system and get more penguins "
																			   + "than you can.");
			if ( hasInitialized.GetValue() == 0 ) {
				penguinStorage.SetValue(5);
				penguinCount.SetValue(5);
				hasInitialized.SetValue(1);
			}
		}
	}
}
