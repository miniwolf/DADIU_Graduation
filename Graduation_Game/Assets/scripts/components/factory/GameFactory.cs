using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.game;
using Assets.scripts.controllers.handlers;
using AssemblyCSharp;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.scripts.components.factory {
	public class GameFactory {
		private readonly CouroutineDelegateHandler handler;


		public GameFactory(CouroutineDelegateHandler handler) {
			this.handler = handler;
		}

		public void BuildCanvas(Actionable<GameActions> actionable) {
			actionable.AddAction(GameActions.EndLevel, EndGameWin(actionable));
			actionable.AddAction(GameActions.TriggerCutScene, CutScene());
			actionable.AddAction(GameActions.FlowScore, FlowScore());
			actionable.AddAction(GameActions.EndLevelLoss, EndGameLoss(actionable));
			actionable.AddAction(GameActions.RetryButtonLoss, RetryButtonLogicLoss(actionable));
			actionable.AddAction(GameActions.RetryButtonWin, RetryButtonLogicWin(actionable));
			actionable.AddAction(GameActions.DisableRetryWin, DisableRetryWin());
		}

		public void BuildStar(Actionable<GameActions> actionable) {
			actionable.AddAction(GameActions.TriggerStar, TriggerStar());
		}

		private Handler CutScene() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new CutScene(handler));
			return actionHandler;
		}

		private Handler TriggerStar() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new TriggerStar(handler));
			return actionHandler;
		}

		private Handler FlowScore() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new FlowScore(handler));
			return actionHandler;
		}

		private Handler EndGameWin(Actionable<GameActions> actionable) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new EndGame(handler, actionable));
			return actionHandler;
		}

		private Handler EndGameLoss(Actionable<GameActions> actionable) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new EndGameLoss(handler, actionable));
			return actionHandler;
		}

		private Handler RetryButtonLogicLoss(Actionable<GameActions> actionable){
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new RetryAction(handler, actionable, TagConstants.UI.FAILSCENEOBJECT));
			return actionHandler;

		}

		private Handler RetryButtonLogicWin(Actionable<GameActions> actionable){
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new RetryAction(handler, actionable, TagConstants.UI.ENDSCENEOBJECT));
			return actionHandler;
		}

		private Handler DisableRetryWin(){
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new DisableRetryAction(TagConstants.UI.ENDSCENEOBJECT));
			return actionHandler;
		}
	}
}
