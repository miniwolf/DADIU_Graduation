using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.pickups {
	public class Plutonium : ActionableGameEntityImpl<PickupActions> {
		public void OnTriggerEnter(Collider colider) {
			if (colider.tag == TagConstants.PENGUIN) {
				ExecuteAction(PickupActions.PickupPlutonium);
			}
		}

		public void TriggerFlow() {
			gameObject.layer = LayerMask.NameToLayer("UI");
			ExecuteAction(PickupActions.FlowScore);
		}

		public override string GetTag() {
			return TagConstants.PLUTONIUM_PICKUP;
		}
	}
}
