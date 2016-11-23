using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.mainmenu {
	public class PenguinCountText : MonoBehaviour {
		protected void Start() {
			var text = GetComponent<Text>();
			text.text = Inventory.penguinCount.GetValue() + "/" + Inventory.penguinStorage.GetValue();
		}
	}
}
