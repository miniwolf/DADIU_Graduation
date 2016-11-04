using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.pickups;
using Assets.scripts.controllers.handlers;

namespace Assets.scripts.components.factory {
	public class PickupFactory : Factory {
		private readonly Actionable<PickupActions> actionable;

		public PickupFactory(Actionable<PickupActions> actionable){
			this.actionable = actionable;
		}

		public void Build() {
			actionable.AddAction(PickupActions.PickupPlutonium, PickupPlutonium());
		}

		private static Handler PickupPlutonium() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new DespawnPlutonium());
			return actionHandler;
		}
	}
}
