using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts.character;
using UnityEngine.UI;

namespace Assets.scripts.traps{
	public class PitDeath {
		void OnTriggerEnter(Collider other){
			if (other.transform.tag == TagConstants.PLAYER) {
				other.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinByPit);
				other.gameObject.GetComponent<Penguin>().enabled = false;
				Text penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
				penguinCounter.text = (int.Parse(penguinCounter.text) - 1).ToString();
			}
		}
	}
}
