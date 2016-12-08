using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.mainmenu {
	public class CashCountText : MonoBehaviour {
		Text text;
		protected void Start() {
			text = GetComponent<Text>();
			text.text = Inventory.cash.GetValue().ToString();
		}
		void Update(){
			text.text = Inventory.cash.GetValue().ToString();
		}
	}
}
