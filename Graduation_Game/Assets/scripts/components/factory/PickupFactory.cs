using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.pickups;
using Assets.scripts.controllers.handlers;
using UnityEngine;

namespace Assets.scripts.components.factory {
	public class PickupFactory : Factory {
		private readonly Actionable<PickupActions> actionable;
	    private readonly CouroutineDelegateHandler coroutineDelegator;

	    public PickupFactory(Actionable<PickupActions> actionable, CouroutineDelegateHandler handler){
			this.actionable = actionable;
	        coroutineDelegator = handler;
	    }

		public void Build() {
			actionable.AddAction(PickupActions.PickupPlutonium, PickupPlutonium());
		}

		private Handler PickupPlutonium() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new DespawnPlutonium(coroutineDelegator));
			return actionHandler;
		}
	}
}
