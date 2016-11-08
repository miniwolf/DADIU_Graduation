using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.animation;
using Assets.scripts.controllers.actions.movement;
using Assets.scripts.controllers.actions.tools;
using Assets.scripts.controllers.actions.tools.lane;
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
			actionable.AddAction(ControllableActions.KillPenguinByElectricution, CreateKillPenguinByElectricution());
			actionable.AddAction(ControllableActions.StartJump, CreateStartJump());
			actionable.AddAction(ControllableActions.StopJump, CreateStopJump());
		}

		private Handler CreateMove() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new MoveForward((Directionable) actionable, actionable));
			return actionHandler;
		}

		private Handler CreateSwitchLeft() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Switch((Directionable) actionable, levelSettings, new Left()));
			return actionHandler;
		}

		private Handler CreateSwitchRight() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Switch((Directionable) actionable, levelSettings, new Right()));
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

		private Handler CreateKillPenguinByElectricution() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable) actionable));
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.ELECTRICUTION));
			return actionHandler;
		}

		private Handler CreateStartJump() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Jump((Directionable) actionable, levelSettings));
			actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.JUMP));
			return actionHandler;
		}

		private Handler CreateStopJump() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.JUMP));
			return actionHandler;
		}
	}
}
