using UnityEngine;
using System.Collections;
using Assets.scripts.character;
using Assets.scripts.components;

namespace Assets.scripts.controllers.actions.tools {
	public class SwitchRight : Action {
		private readonly Directionable direction;
		private Penguin penguin;

		public SwitchRight(Directionable direction){
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			penguin = gameObject.GetComponent<Penguin>();
		}

		public void Execute() {			
			// change direction of the penguin 45 degrees to the right
			penguin.SetDirection(Quaternion.Euler(0, 0, 45) * direction.GetDirection());
		}
	}
}