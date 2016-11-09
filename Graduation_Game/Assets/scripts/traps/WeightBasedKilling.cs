using UnityEngine;
using System.Collections;
using Assets.scripts;
using Assets.scripts.controllers;
using Assets.scripts.components;

public class WeightBasedKilling : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if (other.tag != TagConstants.PENGUIN) {
			return;
		}
		other.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguingByWeightBased);
		other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y - 4f, other.transform.position.z);
		//other.gameObject.SetActive(false);
		//other.gameObject.GetComponent<CharacterController>().center = new Vector3(other.transform.position.x, other.transform.position.y - 4f, other.transform.position.z);
		//other.gameObject.GetComponent<CharacterController>().enabled = false; //Will probably be replaced when death has been properly implemented
	}
}
