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

		public PlayerFactory(Actionable<ControllableActions> actionable, GameObject penguin, GameObject levelSettings) {
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
			actionable.AddAction(ControllableActions.KillPenguingByWeightBased, CreateKillPenguinByWeightBased());
			actionable.AddAction(ControllableActions.KillPenguinByExcavator, CreateKillPenguinByExcavator());
			actionable.AddAction(ControllableActions.KillPenguinByElectricution, CreateKillPenguinByElectricution());
			actionable.AddAction(ControllableActions.KillPenguinByOrca, CreateKillPenguinByOrca());
			actionable.AddAction(ControllableActions.StartJump, CreateStartJump());
			actionable.AddAction(ControllableActions.StopJump, CreateStopJump());
			actionable.AddAction(ControllableActions.StartSpeed, CreateStartSpeed());
			actionable.AddAction(ControllableActions.Speed, CreateSpeed());
			actionable.AddAction(ControllableActions.StopSpeed, CreateStopSpeed());
			actionable.AddAction(ControllableActions.StartEnlarge, CreateStartEnlarge());
			actionable.AddAction(ControllableActions.Enlarge, CreateEnlarge());
			actionable.AddAction(ControllableActions.StopEnlarge, CreateStopEnlarge());
			actionable.AddAction(ControllableActions.StartMinimize, CreateStartMinimize());
			actionable.AddAction(ControllableActions.Minimize, CreateMinimize());
			actionable.AddAction(ControllableActions.StopMinimize, CreateStopMinimize());
			actionable.AddAction(ControllableActions.StartSliding, CreateSlideAction(true));
			actionable.AddAction(ControllableActions.StopSliding, CreateSlideAction(false));
		}

		private Handler CreateSlideAction(bool slide) {
			var actionHandler = new ActionHandler();

			if(slide)
				actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.SPEED));
			else
				actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.SPEED));

			return actionHandler;
		}

		private Handler CreateMove() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new MoveForward((Directionable)actionable, actionable));
			return actionHandler;
		}

		private Handler CreateSwitchLeft() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Switch((Directionable)actionable, levelSettings, new Left()));
			return actionHandler;
		}

		private Handler CreateSwitchRight() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Switch((Directionable)actionable, levelSettings, new Right()));
			return actionHandler;
		}

		private Handler CreateKillPenguinBySpikes() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable)actionable));
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.SPIKEDEATH));
			return actionHandler;
		}

		private Handler CreateKillPenguinByPit() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable)actionable));
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.PITDEATH));
			return actionHandler;
		}

		private Handler CreateKillPenguinByWeightBased() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable)actionable));
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.SPIKEDEATH)); // Should be another anim, it does not exists right now
			return actionHandler;
		}

		private Handler CreateKillPenguinByExcavator() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable)actionable));
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.SPIKEDEATH)); // Should be another anim, it does not exists right now
			return actionHandler;
		}

		private Handler CreateKillPenguinByElectricution() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable)actionable));
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.ELECTRICUTION));
			return actionHandler;
		}

		private Handler CreateKillPenguinByOrca() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable)actionable));
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.ORCADEATH));
			return actionHandler;
		}

		private Handler CreateStartJump() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Jump((Directionable)actionable, levelSettings));
			actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.JUMP));
			return actionHandler;
		}

		private Handler CreateStopJump() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.JUMP));
			return actionHandler;
		}

		private Handler CreateStartSpeed() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StartSpeed((Directionable)actionable));
			actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.SPEED));
			return actionHandler;
		}

		private Handler CreateSpeed() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Speed((Directionable)actionable));
			return actionHandler;
		}

		private Handler CreateStopSpeed() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StopSpeed((Directionable)actionable));
			actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.SPEED));
			return actionHandler;
		}

		private Handler CreateStartEnlarge() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StartEnlarge((Directionable)actionable));
			actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.ENLARGE));
			return actionHandler;
		}

		private Handler CreateEnlarge() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Enlarge((Directionable)actionable));
			return actionHandler;
		}

		private Handler CreateStopEnlarge() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StopEnlarge((Directionable)actionable));
			// TODO there is an offset from when the shrinking animation should be played and when it is actually played
			actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.ENLARGE));
			return actionHandler;
		}

		private Handler CreateStartMinimize() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StartMinimize((Directionable)actionable));
			actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.MINIMIZE));
			return actionHandler;
		}

		private Handler CreateMinimize() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Minimize((Directionable)actionable));
			return actionHandler;
		}

		private Handler CreateStopMinimize() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StopMinimize((Directionable)actionable));
			actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.MINIMIZE));
			return actionHandler;
		}
	}
}
