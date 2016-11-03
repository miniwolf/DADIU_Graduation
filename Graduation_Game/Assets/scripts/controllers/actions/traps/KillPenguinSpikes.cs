using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.traps {
	public class KillPenguinSpikes : Action {
		private GameObject gameObject;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
		}

		public void Execute() {
			gameObject.GetComponentInChildren<Animator>().SetTrigger(AnimationConstants.SPIKEDEATH);
			//Play sound
			//Add all the global things here, e.g. remove a penguin from the amount you have, etc.
		}
	}
}
