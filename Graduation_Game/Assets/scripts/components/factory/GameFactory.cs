using System;
using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.game;
using Assets.scripts.controllers.handlers;

namespace Assets.scripts.components.factory {
	public class GameFactory {
		private readonly Actionable<GameActions> actionable;
		private static CouroutineDelegateHandler handler;

		public GameFactory(Actionable<GameActions> actionable) {
			this.actionable = actionable;
		}

		public void BuildCanvas(CouroutineDelegateHandler handler) {
			actionable.AddAction(GameActions.EndLevel, EndGame(handler));
		}

		public void BuildStar() {
			actionable.AddAction(GameActions.TriggerStar, TriggerStar());
		}

		private Handler TriggerStar() {
			throw new NotImplementedException();
		}

		private static Handler EndGame(CouroutineDelegateHandler handler) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new EndGame(handler));
			return actionHandler;
		}
	}
}
