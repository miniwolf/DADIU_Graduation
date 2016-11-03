using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using Assets.scripts.controllers;
using Assets.scripts.controllers.handlers;

public class Plutonium : ActionableGameEntityImpl<PickupActions> {
	public void OnTriggerEnter(Collider colider) {
		if (colider.tag == TagConstants.PLAYER) {
			ExecuteAction(PickupActions.PickupPlutonium);
		}		
	}

	override public string GetTag() {
		return TagConstants.PLUTONIUM_PICKUP;
	}
}
