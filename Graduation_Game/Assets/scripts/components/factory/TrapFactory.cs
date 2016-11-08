using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.traps;
using Assets.scripts.controllers.handlers;
using Assets.scripts.traps;

namespace Assets.scripts.components.factory {
	public class TrapFactory {
		public static void BuildWire(Actionable<TrapActions> actionable, Wire wire, CouroutineDelegateHandler handler) {
			actionable.AddAction(TrapActions.PULSATE, CreatePulsate(wire, handler));
		}

		private static Handler CreatePulsate(Wire wire, CouroutineDelegateHandler couroutine) {
			var handler = new ActionHandler();
			handler.AddAction(new Pulsate(wire, couroutine));
			return handler;
		}
	}
}
