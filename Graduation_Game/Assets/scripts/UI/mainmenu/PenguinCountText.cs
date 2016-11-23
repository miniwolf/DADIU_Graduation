using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.mainmenu {
	public class PenguinCountText : MonoBehaviour {
		Text text;
		protected void Start() {
			text = GetComponent<Text>();
			text.text = Inventory.penguinCount.GetValue() + "/" + Inventory.penguinStorage.GetValue();
		}
		void Update(){
			text.text = Inventory.penguinCount.GetValue() + "/" + Inventory.penguinStorage.GetValue();
		}
	}
}
