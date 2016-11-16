using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.pickups;
using Assets.scripts.controllers.handlers;

namespace Assets.scripts.components.factory {
	public class PickupFactory {
	    private static CouroutineDelegateHandler coroutineDelegator;

	    public PickupFactory(CouroutineDelegateHandler handler) {
	        coroutineDelegator = handler;
	    }

		public static void BuildPlutonium(Actionable<PickupActions> actionable) {
			actionable.AddAction(PickupActions.PickupPlutonium, PickupPlutonium());
		}

		private static Handler PickupPlutonium() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new DespawnPlutonium(coroutineDelegator));
			return actionHandler;
		}

		public static void BuildEgg(Actionable<PickupActions> actionable) {
			actionable.AddAction(PickupActions.HatchEgg, HatchEgg());
			actionable.AddAction(PickupActions.ShakeEgg, ShakeEgg());
		}

		private static Handler ShakeEgg() {
			var actionHandler = new ActionHandler();
			//actionHandler.AddAction(new ShakeEgg()); TODO: Probably some animation
			return actionHandler;
		}

		private static Handler HatchEgg() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new HatchEgg());
			return actionHandler;
		}
	}
}
