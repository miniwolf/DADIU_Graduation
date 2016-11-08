using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.controllers;

namespace Assets.scripts.traps{
	public class PitDeath : MonoBehaviour {
		protected void OnTriggerEnter(Collider other){
			if ( other.transform.tag != TagConstants.PENGUIN ) {
				return;
			}

			other.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinByPit); 
		}
	}
}
