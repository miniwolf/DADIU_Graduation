using System;
using UnityEngine;
using Assets.scripts.controllers;
using Assets.scripts.UI;
using Assets.scripts.controllers.handlers;
using AssemblyCSharp;
using Assets.scripts.controllers.actions.animation;


namespace Assets.scripts.components.factory {
	public class SealFactory : Factory {
		private readonly Actionable<ControllableActions> actionable;
		//private static CouroutineDelegateHandler handler;
		private GameObject seal;
		private Animator animator;

		public SealFactory ( Actionable<ControllableActions> actionable, GameObject seal) {
			this.actionable = actionable;
			this.seal = seal;
			animator = seal.GetComponentInChildren<Animator>();
		}

		public void Build(){
			actionable.AddAction(ControllableActions.SealJump, CreateSealJump());
			actionable.AddAction(ControllableActions.SealLand, CreateSealLand());
			actionable.AddAction(ControllableActions.SealMove, CreateSealMove());
			actionable.AddAction(ControllableActions.SealDeath, CreateSealDeath());
			actionable.AddAction(ControllableActions.SealFall, CreateSealFall());
		}



		private Handler CreateSealJump(){
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SealJumpAction());
			actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.SEAL_JUMP));
			return actionHandler;
		}

		private Handler CreateSealLand(){
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SealLandAction());
			actionHandler.AddAction(new SetBoolFalse(animator, AnimationConstants.SEAL_JUMP));
			return actionHandler;
		}

		private Handler CreateSealMove(){
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SealMoveForward(actionable));
			return actionHandler;
		}

		private Handler CreateSealDeath(){
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SealDeathAction());
			return actionHandler;
		}

		private Handler CreateSealFall(){
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new SealFallAction());
			actionHandler.AddAction(new SetBoolTrue(animator, AnimationConstants.SEAL_FALL));
			return actionHandler;
		}

	}
}

