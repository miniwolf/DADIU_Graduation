using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine.UI;

namespace Assets.scripts.traps{
	public class SpikeTrap : MonoBehaviour {
		protected void OnTriggerEnter(Collider other){
			if ( other.transform.tag != TagConstants.PENGUIN ) {
				return;
			}

			other.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinBySpikes);
			var penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			penguinCounter.text = (int.Parse(penguinCounter.text) - 1).ToString();
		}
	}
}