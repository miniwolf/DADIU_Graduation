using System;
using Assets.scripts.character;
using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.animation;
using Assets.scripts.controllers.actions.movement;
using Assets.scripts.controllers.actions.tools;
using Assets.scripts.controllers.actions.tools.lane;
using Assets.scripts.controllers.actions.tools.resize;
using Assets.scripts.controllers.actions.traps;
using Assets.scripts.controllers.handlers;
using Assets.scripts.gamestate;
using UnityEngine;
using Resize = Assets.scripts.controllers.actions.tools.Resize;
using AssemblyCSharp;
using Assets.scripts.components.registers;
using UnityEngine.Networking.Types;
using System.Reflection;
using System.Linq;
using Assets.scripts.controllers.actions.sound;
using Assets.scripts.sound;
using System.Collections;

namespace Assets.scripts.components.factory {
	public class PlayerFactory : Factory {
		private readonly Actionable<ControllableActions> actionable;
		private readonly GameObject levelSettings;
		private readonly Animator animator;
		private readonly Directionable directionable;
		private readonly GameStateManager gameStateManager;
	    private readonly NotifierSystem notifierSystem;

	    public PlayerFactory(Actionable<ControllableActions> actionable, GameObject penguin, GameObject levelSettings, GameStateManager stateManager, NotifierSystem notifierSystem){
			this.actionable = actionable;
			this.levelSettings = levelSettings;
			directionable = penguin.GetComponent<Directionable>();
			animator = penguin.GetComponentInChildren<Animator>();
			gameStateManager = stateManager;
		    this.notifierSystem = notifierSystem;
		    Penguin p = penguin.GetComponent<Penguin>();
		    p.SetGameStateManager(gameStateManager);
		    p.SetNotifyManager(this.notifierSystem);
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
			actionable.AddAction(ControllableActions.StartSliding, CreateSlideAction(true));
			actionable.AddAction(ControllableActions.StopSliding, CreateSlideAction(false));
			actionable.AddAction(ControllableActions.Freeze, CreateFreezeAction(true));
			actionable.AddAction(ControllableActions.UnFreeze, CreateFreezeAction(false));
			actionable.AddAction(ControllableActions.Stop, CreateStopAction());
			actionable.AddAction(ControllableActions.Start, CreateStartAction());
			actionable.AddAction(ControllableActions.OtherPenguinDied, CreateOtherPenguinDeath());
			actionable.AddAction(ControllableActions.Celebrate, CreateCelebrateAction());
		}

	    private Handler CreateOtherPenguinDeath()
	    {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new OtherPenguinDiedAction(animator));
			return actionHandler;
	    }

		private Handler CreateCelebrateAction() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SetTrigger(animator, AnimationConstants.CELEBRATE));
			//StartCoroutine(Wait());
			//actionHandler.AddAction(new StopMoving(actionable));
			return actionHandler;
		}

	    private Handler CreateFreezeAction(bool freeze) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new FreezeAction(animator, freeze));
			return actionHandler;
		}

		private Handler CreateSlideAction(bool slide) {
			var actionHandler = new ActionHandler();

			if ( slide ) {
				actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.SPEED));
			} else {
				actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.SPEED));
			}

			return actionHandler;
		}

		private Handler CreateMove() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new MoveForward((Directionable) actionable, actionable));
//		    actionHandler.AddAction(new PostSoundEvent(SoundConstants.PenguinSounds.START_MOVING));
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
			actionHandler.AddAction(new KillPenguin((Killable) actionable, notifierSystem));
			actionHandler.AddAction(new SetTrigger(animator, GetRandomAnimation(constant)));
			return actionHandler;
		}

		private string GetRandomAnimation(string type) {
			FieldInfo[] fields = typeof(AnimationConstants).GetFields().Where(f => f.GetRawConstantValue().ToString().StartsWith(type)).Cast<FieldInfo>().ToArray();
			return fields[UnityEngine.Random.Range(0, fields.Length)].GetRawConstantValue().ToString();
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
			actionHandler.AddAction(new StartSpeed(directionable));
			//actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.SPEED));
			return actionHandler;
		}

		private Handler CreateSpeed() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Speed(directionable));
			//actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.SPEED));
			return actionHandler;
		}

		private Handler CreateStopSpeed() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StopSpeed(directionable));
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
			actionHandler.AddAction(new Resize((Directionable) actionable, new Enlarge()));
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
			actionHandler.AddAction(new Resize((Directionable) actionable, new Minimize()));
			return actionHandler;
		}

		private Handler CreateStopMinimize() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StopMinimize((Directionable) actionable));
			actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.MINIMIZE));
			return actionHandler;
		}

		private Handler CreateStopAction() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StopMoving(actionable));
			return actionHandler;
		}

		private Handler CreateStartAction() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new StartMoving(actionable));
			return actionHandler;
		}
	}
}
