using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.traps {
	public class Orca : MonoBehaviour {
		protected void OnTriggerEnter(Collider other) {
			if ( other.tag == TagConstants.PENGUIN && !other.GetComponent<Killable>().IsDead() ) {
				other.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinByOrca);
			}
		}
	}
}
