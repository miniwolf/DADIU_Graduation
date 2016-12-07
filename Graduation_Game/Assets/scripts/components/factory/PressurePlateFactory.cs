using Assets.scripts.controllers.handlers;
using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.sound;
using Assets.scripts.controllers.actions.tools.pressurePlate;
using Assets.scripts.sound;

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
			actionHandler.AddAction(new PostSoundEvent(SoundConstants.ToolSounds.PRESSURE_PLATE));
			actionHandler.AddAction(new PostSoundEvent(SoundConstants.ToolSounds.BRIDGE_FALLING));
			return actionHandler;
		}
	}
}
