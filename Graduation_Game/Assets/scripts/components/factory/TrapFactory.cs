using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.traps;
using Assets.scripts.controllers.handlers;
using Assets.scripts.traps;
using UnityEngine;

namespace Assets.scripts.components.factory {
	public class TrapFactory {
		public static void BuildWire(Actionable<TrapActions> actionable, Wire wire, CouroutineDelegateHandler handler) {
			actionable.AddAction(TrapActions.PULSATE, CreatePulsate(wire, handler));
		}
		public static void BuildWeightBasedTrap(Actionable<TrapActions> actionable, GameObject weight) {
			actionable.AddAction(TrapActions.WEIGHTBASEDSINKING, CreateWeightBasedSinking(weight));
			actionable.AddAction(TrapActions.WEIGHTBASEDLIFTING, CreateWeightBasedLifting(weight));
		}

		private static Handler CreatePulsate(Wire wire, CouroutineDelegateHandler couroutine) {
			var handler = new ActionHandler();
			handler.AddAction(new Pulsate(wire, couroutine));
			return handler;
		}
		private static Handler CreateWeightBasedSinking(GameObject weight) {
			var handler = new ActionHandler();
			handler.AddAction(new SinkWeightBased(weight.GetComponent<WeightBasedInterface>()));
			return handler;
		}
		private static Handler CreateWeightBasedLifting(GameObject weight) {
			var handler = new ActionHandler();
			handler.AddAction(new LiftWeightBased(weight.GetComponent<WeightBasedInterface>()));
			return handler;
		}
	}
}
