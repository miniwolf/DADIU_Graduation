using UnityEngine;
using System.Collections;
using Assets.scripts;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using Assets.scripts.controllers;
using Assets.scripts.controllers.handlers;
using Assets.scripts.character;

namespace Assets.scripts.traps{
	public class SpikeTrap : ActionableGameEntityImpl<ControllableActions> {
		public override string GetTag(){
			return TagConstants.SPIKETRAP;
		}

		void OnTriggerEnter(Collider other){
			if (other.transform.tag == TagConstants.PLAYER) {
				other.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinBySpikes);
				other.gameObject.GetComponent<Penguin>().enabled = false;
			}
		}
	}
}