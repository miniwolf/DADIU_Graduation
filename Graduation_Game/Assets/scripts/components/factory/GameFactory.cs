using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.game;
using Assets.scripts.controllers.handlers;

namespace Assets.scripts.components.factory {
	public class GameFactory : Factory {
		private readonly Actionable<GameActions> actionable;

		public GameFactory(Actionable<GameActions> actionable) {
			this.actionable = actionable;
		}

		public void Build() {
			actionable.AddAction(GameActions.EndLevel, EndGame());
		}

		private static Handler EndGame() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new EndGame());
			return actionHandler;
		}
	}
}
