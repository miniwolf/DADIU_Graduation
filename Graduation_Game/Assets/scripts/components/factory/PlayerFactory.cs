using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.animation;
using Assets.scripts.controllers.actions.movement;
using Assets.scripts.controllers.actions.tools;
using Assets.scripts.controllers.actions.traps;
using Assets.scripts.controllers.handlers;
using UnityEngine;

namespace Assets.scripts.components.factory {
	public class PlayerFactory : Factory {
	    private readonly Actionable<ControllableActions> actionable;
		private readonly GameObject levelSettings;
		private readonly Animator animator;

		public PlayerFactory(Actionable<ControllableActions> actionable, GameObject penguin, GameObject levelSettings){
			this.actionable = actionable;
			this.levelSettings = levelSettings;

			animator = penguin.GetComponentInChildren<Animator>();
		}

		public void Build() {
			actionable.AddAction(ControllableActions.Move, CreateMove());
			actionable.AddAction(ControllableActions.SwitchLeft, CreateSwitchLeft());
			actionable.AddAction(ControllableActions.SwitchRight, CreateSwitchRight());
			actionable.AddAction(ControllableActions.KillPenguinBySpikes, CreateKillPenguinBySpikes());
			actionable.AddAction(ControllableActions.KillPenguinByPit, CreateKillPenguinByPit());
			actionable.AddAction(ControllableActions.Jump, CreateJump());
		}

		private Handler CreateMove() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new MoveForward((Directionable) actionable));
			return actionHandler;
		}

		private Handler CreateSwitchLeft() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SwitchLeft((Directionable) actionable, levelSettings));
			return actionHandler;
		}

		private Handler CreateSwitchRight() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SwitchRight((Directionable) actionable, levelSettings));
			return actionHandler;
		}

		private Handler CreateKillPenguinBySpikes() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable) actionable));
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.SPIKEDEATH));
			return actionHandler;
		}

		private Handler CreateKillPenguinByPit() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable) actionable));
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.PITDEATH));
			return actionHandler;
		}

		private Handler CreateJump() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Jump((Directionable) actionable, levelSettings));
			return actionHandler;
		}
	}
}
