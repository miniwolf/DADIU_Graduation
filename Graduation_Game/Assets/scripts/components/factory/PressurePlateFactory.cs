using System;
using Assets.scripts.components;
using Assets.scripts.controllers.handlers;
using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.tools.pressurePlate;

namespace Assets.scripts.components.factory{
	public class PressurePlateFactory  {
		private readonly Actionable<PressurePlateActions> actionable;

		public PressurePlateFactory(Actionable<PressurePlateActions> actionable){
			this.actionable = actionable;
		}

		public void BuildFallDownBridge() {
			actionable.AddAction (PressurePlateActions.Excute, CreateFallDown());
		}

		private static Handler CreateFallDown() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new FallDownBridge());
			Console.Write ("It works, mon!!!");
			return actionHandler;
		}
	}
}

