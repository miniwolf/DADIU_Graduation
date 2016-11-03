using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.traps
{
	public class KillPenguinPit : Action {
		private GameObject gameObject;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
		}

		public void Execute() {
			gameObject.GetComponentInChildren<Animator>().SetTrigger(AnimationConstants.PITDEATH);
			//gameObject.SetActive (false);
		}
	}
}