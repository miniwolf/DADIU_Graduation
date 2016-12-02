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
using System.Reflection;
using System.Linq;
using Assets.scripts.controllers.actions.sound;
using Assets.scripts.sound;

namespace Assets.scripts.components.factory {

	public class PlayerFactory : Factory {
		private readonly Actionable<ControllableActions> actionable;
		private readonly GameObject levelSettings;
		private readonly Animator animator;
		private readonly Directionable directionable;
		private readonly GameStateManager gameStateManager;
	    private readonly NotifierSystem notifierSystem;
		private GameObject splat;
		AnimationSet animationSet = new AnimationSet();
	    private CouroutineDelegateHandler delegator;

		public PlayerFactory(Actionable<ControllableActions> actionable, GameObject penguin, GameObject levelSettings,
		    GameStateManager stateManager, NotifierSystem notifierSystem, GameObject splat, CouroutineDelegateHandler delegator){
			this.actionable = actionable;
			this.levelSettings = levelSettings;
			directionable = penguin.GetComponent<Directionable>();
			animator = penguin.GetComponentInChildren<Animator>();
			animator.SetFloat("WalkBlend", Random.Range(0.0f, 1.0f));
			animator.SetTime(Random.Range(0.0f, 1.0f));
			gameStateManager = stateManager;
		    this.notifierSystem = notifierSystem;
		    Penguin p = penguin.GetComponent<Penguin>();
		    p.SetGameStateManager(gameStateManager);
		    p.SetNotifyManager(this.notifierSystem);
			this.splat = splat;
		    this.delegator = delegator;
		}

		public void Build() {
			actionable.AddAction(ControllableActions.Move, CreateMove());
		    CreateSwitchLane();
			actionable.AddAction(ControllableActions.KillPenguinByWallSpikes, KillPenguinBy(animationSet.deathSpikeWallAnimation));
			actionable.AddAction(ControllableActions.KillPenguinByGroundSpikes, KillPenguinBy(animationSet.deathSpikeGroundAnimation));
			actionable.AddAction(ControllableActions.KillPenguinByPit, KillPenguinBy(animationSet.deathPitAnimation));
			actionable.AddAction(ControllableActions.KillPenguinByExcavator, KillPenguinBy(animationSet.deathSpikeGroundAnimation)); // TODO: There should be another
			actionable.AddAction(ControllableActions.KillPenguinByWater, KillPenguinBy(animationSet.deathDrownAnimation));
			actionable.AddAction(ControllableActions.KillPenguinByElectricution, KillPenguinBy(animationSet.deathElectricAnimation));
		//	actionable.AddAction(ControllableActions.KillPenguinByOrca, KillPenguinBy(deathSpikeAnimation != null ? deathSpikeAnimation() : null));			// inexistent
			actionable.AddAction(ControllableActions.StartJump, CreateStartJump());
			actionable.AddAction(ControllableActions.StopJump, CreateStopJump());
		//	actionable.AddAction(ControllableActions.StartSpeed, CreateStartSpeed());
		//	actionable.AddAction(ControllableActions.Speed, CreateSpeed());
		//	actionable.AddAction(ControllableActions.StopSpeed, CreateStopSpeed());
		//	actionable.AddAction(ControllableActions.StartEnlarge, CreateStartEnlarge());
		//	actionable.AddAction(ControllableActions.Enlarge, CreateEnlarge());
		//	actionable.AddAction(ControllableActions.StopEnlarge, CreateStopEnlarge());
		//	actionable.AddAction(ControllableActions.StartMinimize, CreateStartMinimize());
		//	actionable.AddAction(ControllableActions.Minimize, CreateMinimize());
		//	actionable.AddAction(ControllableActions.StopMinimize, CreateStopMinimize());
			actionable.AddAction(ControllableActions.StartSliding, CreateSlideAction(true));
			actionable.AddAction(ControllableActions.StopSliding, CreateSlideAction(false));
			actionable.AddAction(ControllableActions.Freeze, CreateFreezeAction(true));
			actionable.AddAction(ControllableActions.UnFreeze, CreateFreezeAction(false));
			actionable.AddAction(ControllableActions.Stop, CreateStopAction());
			actionable.AddAction(ControllableActions.Start, CreateStartAction());
			actionable.AddAction(ControllableActions.OtherPenguinDied, CreateOtherPenguinDeath());
			actionable.AddAction(ControllableActions.Celebrate, CreateCelebrateAction());
			actionable.AddAction(ControllableActions.Win, CreateWinAction());
			actionable.AddAction(ControllableActions.PenguinFall, CreateFall());
			actionable.AddAction(ControllableActions.PenguinStopFall, CreateStopFall());
		}

		private Handler CreateStopFall() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SetBoolFalse(animator, animationSet.fallAnimation));
			return actionHandler;
		}

		private Handler CreateFall() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SetBoolTrue(animator, animationSet.fallAnimation));
			return actionHandler;
		}

	    private Handler CreateOtherPenguinDeath() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new OtherPenguinDiedAction(animator, animationSet.reactionToDeath));
			return actionHandler;
	    }

		private Handler CreateCelebrateAction() {
			var actionHandler = new ActionHandler();
			if(animationSet.celebrateAnimation != "")
				actionHandler.AddAction(new SetTrigger(animator, animationSet.celebrateAnimation));
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
				actionHandler.AddAction(new StartSlidingAction(delegator, animator, animationSet.slidingAnimation, directionable));
			} else {
			    actionHandler.AddAction(new StopSlidingAction(directionable));
			}

			return actionHandler;
		}

		private Handler CreateMove() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new MoveForward((Directionable) actionable, actionable, delegator));
//		    actionHandler.AddAction(new PostSoundEvent(SoundConstants.PenguinSounds.START_MOVING));
			return actionHandler;
		}

		private void CreateSwitchLane() {
		    var leftSwitch = new Switch((Directionable) actionable, levelSettings, new Left());
		    var rightSwitch = new Switch((Directionable) actionable, levelSettings, new Right());

		    leftSwitch.SetOther(rightSwitch);
		    rightSwitch.SetOther(leftSwitch);

		    var actionHandler = new ActionHandler();
			actionHandler.AddAction(leftSwitch);
		    actionHandler.AddAction(new PostSoundEvent(SoundConstants.ToolSounds.CHANGE_LANE_TRIGGERED));
		    actionable.AddAction(ControllableActions.SwitchLeft, actionHandler);

		    actionHandler = new ActionHandler();
		    actionHandler.AddAction(rightSwitch);
		    actionHandler.AddAction(new PostSoundEvent(SoundConstants.ToolSounds.CHANGE_LANE_TRIGGERED));
		    actionable.AddAction(ControllableActions.SwitchRight, actionHandler);
		}

		private Handler KillPenguinBy(string constant) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new KillPenguin((Killable) actionable, notifierSystem));
			if (constant != "")
				actionHandler.AddAction(new SetTrigger(animator, constant));
			if ( !constant.Equals(animationSet.deathDrownAnimation) ) {
				actionHandler.AddAction(new DefaultBloodSplatterAction(splat));
			}
			return actionHandler;
		}
		
		private Handler CreateStartJump() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Jump((Directionable) actionable, levelSettings));
			if (animationSet.jumpAnimation != "")
				actionHandler.AddAction(new SetBoolTrue(animator, animationSet.jumpAnimation));
			actionHandler.AddAction(new PostSoundEvent(SoundConstants.ToolSounds.JUMP_TRIGGERED));
			return actionHandler;
		}

		private Handler CreateStopJump() {
			var actionHandler = new ActionHandler();
			if (animationSet.jumpAnimation != "")
				actionHandler.AddAction(new SetBoolFalse(animator, animationSet.jumpAnimation));
			return actionHandler;
		}
		/*
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
			if (jumpAnimation != null)
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
			if (jumpAnimation != null)
				actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.MINIMIZE));
			return actionHandler;
		}
		*/
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

		private Handler CreateWinAction() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new Win(directionable, actionable));
			return actionHandler;
		}
	}
}
