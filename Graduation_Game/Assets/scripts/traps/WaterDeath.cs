using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.controllers;

namespace Assets.scripts.traps{
	public class WaterDeath : MonoBehaviour {
		protected void OnTriggerEnter(Collider other){
			if ( other.transform.tag != TagConstants.PENGUIN && other.transform.tag != TagConstants.SEAL ) {
				return;
			}else if(other.tag == TagConstants.SEAL){
				other.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.SealDeath);
				return;
			}

			other.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinByWater); 
		}
	}
}
