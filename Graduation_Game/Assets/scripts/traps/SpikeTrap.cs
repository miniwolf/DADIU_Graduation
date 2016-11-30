using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.controllers;

namespace Assets.scripts.traps {
	public class SpikeTrap : MonoBehaviour {
		protected void OnTriggerEnter(Collider other) {

		    if ( other.transform.tag != TagConstants.PENGUIN && other.transform.tag != TagConstants.SEAL ) {
				return;
			}

		    var actionable = other.gameObject.GetComponent<Actionable<ControllableActions>>();
		    var directionable = other.gameObject.GetComponent<Directionable>();

		    if ( other.tag == TagConstants.SEAL ) {
		        actionable.ExecuteAction(ControllableActions.SealDeath);
		        return;
		    }

		    if ( directionable.IsEnlarging() ) {
				gameObject.SetActive(false);
				return;
			}

		    switch (tag) {
                case TagConstants.SPIKETRAPGROUND:
                    actionable.ExecuteAction(ControllableActions.KillPenguinByGroundSpikes);
                    break;
                case TagConstants.SPIKETRAPWALL:
                    actionable.ExecuteAction(ControllableActions.KillPenguinByWallSpikes);
                    break;
		    }
		}
	}
}
