using UnityEngine;
using System.Collections;
using Assets.scripts.components;

namespace Assets.scripts.controllers.actions.tools {
	public class SwitchLeft : Action {
		private readonly Directionable direction;
		private Penguin penguin;

		public SwitchLeft(Directionable direction){
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			penguin = gameObject.GetComponent<Penguin>();
		}

		public void Execute() {			
			// change direction of the penguin 45 degrees to the right
			// create coroutine that computes the number of seconds it takes the penguin
			// to change to the middle of the other lane, given its speed, and using 45 degrees
			// then start coroutine that waits for those seconds and changes back the direction to forward

			// rotates -45 degrees around z axis
			penguin.SetDirection(Quaternion.Euler(0, 0, -45) * direction.GetDirection());
		}
	}
}