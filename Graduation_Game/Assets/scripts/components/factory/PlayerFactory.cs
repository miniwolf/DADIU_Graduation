using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.movement;
using Assets.scripts.controllers.actions.movement.sound;
using Assets.scripts.controllers.handlers;

namespace Assets.scripts.components.factory {
	public class PlayerFactory : Factory {
	    private readonly Actionable<ControllableActions> actionable;

	    public PlayerFactory(Actionable<ControllableActions> actionable)
	    {
	        this.actionable = actionable;
	    }

	    public void Build() {
	        actionable.AddAction(ControllableActions.Move, CreateMove());
	    }

	    private static Handler CreateMove() {
	        var actionHandler = new ActionHandler();
	        actionHandler.AddAction(new MoveForward());
	        actionHandler.AddAction(new StartMovingSound());
	        return actionHandler;
	    }
	}
}