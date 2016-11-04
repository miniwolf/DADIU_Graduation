using Assets.scripts.components;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.scripts.controllers.actions.tools.pressurePlate{
	public class FallDownBridge : Action {
		private GameObject gameObject;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
		}

		public void Execute() {
			Debug.Log ("Works, mon!!!!!!");
		}
	}
}