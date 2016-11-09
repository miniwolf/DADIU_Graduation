using Assets.scripts.controllers.handlers;
using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.tools.pressurePlate;

namespace Assets.scripts.components.factory{
	public class PressurePlateFactory  {
		private readonly Actionable<PressurePlateActions> actionable;

		public PressurePlateFactory(Actionable<PressurePlateActions> actionable){
			this.actionable = actionable;
		}

		public void BuildActionOnLinkingObject(LinkingComponent linkingObject) {
			actionable.AddAction (PressurePlateActions.Excute, CreateAction(linkingObject));
		}

		private static Handler CreateAction(LinkingComponent linkingObject) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new TriggerLinkingComponent(linkingObject));
			return actionHandler;
		}
	}
}
