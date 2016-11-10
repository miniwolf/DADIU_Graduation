﻿using Assets.scripts.character;
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
		private readonly Penguin penguin;
		private readonly Directionable directionable;

		public PlayerFactory(Actionable<ControllableActions> actionable, GameObject penguin, GameObject levelSettings){
			this.actionable = actionable;
			this.levelSettings = levelSettings;
			this.penguin = penguin.GetComponent<Penguin>();
			directionable = penguin.GetComponent<Directionable>();
			animator = penguin.GetComponentInChildren<Animator>();
		}

		public void Build() {
			actionable.AddAction(ControllableActions.Move, CreateMove());
			actionable.AddAction(ControllableActions.SwitchLeft, CreateSwitchLeft());
			actionable.AddAction(ControllableActions.SwitchRight, CreateSwitchRight());
			actionable.AddAction(ControllableActions.KillPenguinBySpikes, KillPenguinBy(AnimationConstants.SPIKEDEATH));
			actionable.AddAction(ControllableActions.KillPenguinByPit, KillPenguinBy(AnimationConstants.PITDEATH));
			actionable.AddAction(ControllableActions.KillPenguinByExcavator, KillPenguinBy(AnimationConstants.SPIKEDEATH)); // TODO: There should be another
			actionable.AddAction(ControllableActions.KillPenguingByWeightBased, KillPenguinBy(AnimationConstants.DROWNING));
			actionable.AddAction(ControllableActions.KillPenguinByElectricution, KillPenguinBy(AnimationConstants.ELECTRICUTION));
			actionable.AddAction(ControllableActions.KillPenguinByOrca, KillPenguinBy(AnimationConstants.ORCADEATH));
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

		private Handler KillPenguinBy(string constant) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable) actionable));
			actionHandler.AddAction(new SetTrigger(animator, constant));
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

		private Handler CreateStartSpeed() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StartSpeed(penguin, directionable));
			//actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.SPEED));
			return actionHandler;
		}

		private Handler CreateSpeed() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Speed(penguin, directionable));
			//actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.SPEED));
			return actionHandler;
		}
		private Handler CreateStopSpeed() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StopSpeed(penguin, directionable));
			//actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.SPEED));
			actionHandler.AddAction(new StopSpeed((Directionable) actionable));
			actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.SPEED));
			return actionHandler;
		}

		private Handler CreateStartEnlarge() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StartEnlarge((Directionable) actionable));
			actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.ENLARGE));
			return actionHandler;
		}

		private Handler CreateEnlarge() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Enlarge((Directionable) actionable));
			return actionHandler;
		}
		private Handler CreateStopEnlarge() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StopEnlarge((Directionable) actionable));
			// TODO there is an offset from when the shrinking animation should be played and when it is actually played
			actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.ENLARGE));
			return actionHandler;
		}

		private Handler CreateStartMinimize() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StartMinimize((Directionable) actionable));
			actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.MINIMIZE));
			return actionHandler;
		}

		private Handler CreateMinimize() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Minimize((Directionable) actionable));
			return actionHandler;
		}
		private Handler CreateStopMinimize() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StopMinimize((Directionable) actionable));
			actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.MINIMIZE));
			return actionHandler;
		}
	}
}
