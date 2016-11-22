using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.pickups {
	public class ShakeEgg : Action {
		private GameObject go;
		private bool toggle;

		public void Setup(GameObject gameObject) {
			go = gameObject;
		}

		public void Execute() {
			go.GetComponentInChildren<Button>().GetComponent<Image>().color = toggle ? Color.black : Color.blue;
			toggle = !toggle;
		}
	}
}
