using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.traps;
using Assets.scripts.controllers.handlers;
using Assets.scripts.traps;

namespace Assets.scripts.components.factory {
	public class TrapFactory {
		public static void BuildWire(Actionable<TrapActions> actionable, Wire wire, CouroutineDelegateHandler handler) {
			actionable.AddAction(TrapActions.PULSATE, CreatePulsate(wire, handler));
		}
		public static void BuildWeightBasedTrap(Actionable<TrapActions> actionable){
			actionable.AddAction(TrapActions.WEIGHTBASEDSINKING, CreateWeightBasedSinking());
			actionable.AddAction(TrapActions.WEIGHTBASEDLIFTING, CreateWeightBasedLifting());
		}

		private static Handler CreatePulsate(Wire wire, CouroutineDelegateHandler couroutine) {
			var handler = new ActionHandler();
			handler.AddAction(new Pulsate(wire, couroutine));
			return handler;
		}
		private static Handler CreateWeightBasedSinking() {
			var handler = new ActionHandler();
			handler.AddAction(new SinkWeightBased());
			return handler;
		}
		private static Handler CreateWeightBasedLifting() {
			var handler = new ActionHandler();
			handler.AddAction(new LiftWeightBased());
			return handler;
		}
	}
}
