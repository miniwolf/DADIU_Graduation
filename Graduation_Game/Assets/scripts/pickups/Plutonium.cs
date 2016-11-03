﻿using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.pickups {
	public class Plutonium : ActionableGameEntityImpl<PickupActions> {
		public void OnTriggerEnter(Collider colider) {
			if (colider.tag == TagConstants.PLAYER) {
				ExecuteAction(PickupActions.PickupPlutonium);
			}
		}

		public override string GetTag() {
			return TagConstants.PLUTONIUM_PICKUP;
		}
	}
}
