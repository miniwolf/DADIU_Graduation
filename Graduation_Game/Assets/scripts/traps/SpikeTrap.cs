using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts.character;

namespace Assets.scripts.traps{
	public class SpikeTrap : MonoBehaviour {
		protected void OnTriggerEnter(Collider other){
			if ( other.transform.tag != TagConstants.PENGUIN ) {
				return;
			}
			
			other.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinBySpikes);
		}
	}
}