using UnityEngine;

namespace Assets.scripts.controllers.actions.pickups {
	public class ShakeEgg : Action {
		private Material material;
		private bool toggle;

		public void Setup(GameObject gameObject) {
			material = gameObject.GetComponent<MeshRenderer>().material;
		}

		public void Execute() {
			material.color = toggle ? Color.black : Color.blue;
			toggle = !toggle;
		}
	}
}
