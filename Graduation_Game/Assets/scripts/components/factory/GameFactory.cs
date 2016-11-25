using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.game;
using Assets.scripts.controllers.handlers;

namespace Assets.scripts.components.factory {
	public class GameFactory {
		private readonly CouroutineDelegateHandler handler;

		public GameFactory(CouroutineDelegateHandler handler) {
			this.handler = handler;
		}

		public void BuildCanvas(Actionable<GameActions> actionable) {
			actionable.AddAction(GameActions.EndLevel, EndGame(actionable));
			actionable.AddAction(GameActions.TriggerCutScene, CutScene());
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

		private Handler EndGame(Actionable<GameActions> actionable) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new EndGame(handler, actionable));
			return actionHandler;
		}
	}
}
