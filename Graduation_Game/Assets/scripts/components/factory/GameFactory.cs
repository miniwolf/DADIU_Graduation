using System;
using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.game;
using Assets.scripts.controllers.handlers;

namespace Assets.scripts.components.factory {
	public class GameFactory {
		private readonly Actionable<GameActions> actionable;

		public GameFactory(Actionable<GameActions> actionable) {
			this.actionable = actionable;
		}

		public void BuildCanvas(CouroutineDelegateHandler handler) {
			actionable.AddAction(GameActions.EndLevel, EndGame(handler));
		}

		public void BuildStar(CouroutineDelegateHandler handler) {
			actionable.AddAction(GameActions.TriggerStar, TriggerStar(handler));
		}

		public void BuildCutScene(CouroutineDelegateHandler handler) {
			actionable.AddAction(GameActions.TriggerStar, CutScene(handler));
		}

		private Handler CutScene(CouroutineDelegateHandler handler) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new CutScene(handler));
			return actionHandler;
		}

		private Handler TriggerStar(CouroutineDelegateHandler handler) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new TriggerStar(handler));
			return actionHandler;
		}

		private static Handler EndGame(CouroutineDelegateHandler handler) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new EndGame(handler));
			return actionHandler;
		}
	}
}
