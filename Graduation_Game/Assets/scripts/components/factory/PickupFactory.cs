using Assets.scripts.controllers;
using Assets.scripts.controllers.actions;
using Assets.scripts.controllers.actions.movement;
using Assets.scripts.controllers.handlers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.components.factory {
	public class PickupFactory : Factory {
		private readonly Actionable<PickupActions> actionable;
		private Text inventory;

		public PickupFactory(Actionable<PickupActions> actionable){
			this.actionable = actionable;
			inventory = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
		}

		public void Build() {
			actionable.AddAction(PickupActions.PickupPlutonium, PickupPlutonium());
		}

		private Handler PickupPlutonium() {
			inventory.text = int.Parse (inventory.text) + 1;
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Despawn());
			return actionHandler;
		}
	}
}