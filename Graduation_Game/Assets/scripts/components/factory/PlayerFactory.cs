using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.movement;
using Assets.scripts.controllers.actions.tools;
using Assets.scripts.controllers.actions.movement.sound;
using Assets.scripts.controllers.handlers;

namespace Assets.scripts.components.factory {
	public class PlayerFactory : Factory {
	    private readonly Actionable<ControllableActions> actionable;

	    public PlayerFactory(Actionable<ControllableActions> actionable){
	        this.actionable = actionable;
	    }

	    public void Build() {
			actionable.AddAction(ControllableActions.Move, CreateMove());
			actionable.AddAction(ControllableActions.SwitchLeft, CreateSwitchLeft());
			actionable.AddAction(ControllableActions.SwitchRight, CreateSwitchRight());
	    }

	    private Handler CreateMove() {
	        var actionHandler = new ActionHandler();
			actionHandler.AddAction(new MoveForward((Directionable) actionable));
	        actionHandler.AddAction(new StartMovingSound());
	        return actionHandler;
	    }

		private Handler CreateSwitchLeft() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SwitchLeft((Directionable) actionable));
			return actionHandler;
		}

		private Handler CreateSwitchRight() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SwitchRight((Directionable) actionable));
			return actionHandler;
		}
	}
}