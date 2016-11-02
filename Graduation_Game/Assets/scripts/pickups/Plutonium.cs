using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using Assets.scripts.controllers;
using Assets.scripts.controllers.handlers;

public class Plutonium : MonoBehaviour {

	Inventory inventory;
	Dictionary<PickupActions, Handler> actions = new Dictionary<PickupActions, Handler>();

	void Awake(){
		inventory = GetComponent<Inventory> ();
	}

	public void OnTriggerEnter(Collider colider) {
		if (colider.tag == TagConstants.PLAYER) {
			inventory.plutonium.IncreaseAmount ();
			//ExecuteAction(PickupActions.Despawn);
		}
		
	}
}
