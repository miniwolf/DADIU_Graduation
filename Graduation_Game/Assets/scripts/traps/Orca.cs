using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.traps {
	public class Orca : MonoBehaviour {
		private bool immune;

		protected void OnTriggerEnter(Collider other) {
			if ( other.tag == TagConstants.METALTEMPLATE ) {
				ToggleImmune();
			}
			if ( other.tag == TagConstants.PENGUIN && !other.GetComponent<Killable>().IsDead() && !immune ) {
				other.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinByOrca);
			}
		}

		protected void onTriggerExit(Collider other) {
			if ( other.tag == TagConstants.METALTEMPLATE ) {
				ToggleImmune();
			}
		}

		private void ToggleImmune() {
			immune = !immune;
		}
	}
}
