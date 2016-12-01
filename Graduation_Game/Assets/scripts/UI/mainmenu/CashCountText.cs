using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.mainmenu {
	public class CashCountText : MonoBehaviour {
		protected void Start() {
			var text = GetComponent<Text>();
			text.text = Inventory.cash.GetValue().ToString();
		}
	}
}
