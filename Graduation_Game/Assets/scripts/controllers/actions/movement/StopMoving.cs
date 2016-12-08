using Assets.scripts.character;
using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement {
	public class StopMoving : Action {
		private readonly Actionable<ControllableActions> actionable;
		private Penguin penguin;

		public StopMoving(Actionable<ControllableActions> actionable) {
			this.actionable = actionable;
		}

		public void Setup(GameObject go) {
			penguin = go.GetComponent<Penguin>();
		}

		public void Execute() {
			penguin.SetStop(true);
			actionable.ExecuteAction(ControllableActions.Freeze);
		}
	}
}
