using Assets.scripts.controllers;
using Assets.scripts.controllers.actions;
using Assets.scripts.controllers.actions.movement;
using Assets.scripts.controllers.actions.pickups;
using Assets.scripts.controllers.handlers;
using UnityEngine;

namespace Assets.scripts.components.factory {
	public class PickupFactory : Factory {
		private readonly Actionable<PickupActions> actionable;
		private GameObject levelSettings;

		public PickupFactory(Actionable<PickupActions> actionable, GameObject levelSettings){
			this.actionable = actionable;
			this.levelSettings = levelSettings;
		}

		public void Build() {
			actionable.AddAction(PickupActions.PickupPlutonium, PickupPlutonium());
		}

		private Handler PickupPlutonium() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new DespawnPlutonium());
			return actionHandler;
		}
	}
}
