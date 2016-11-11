using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class StartMinimize : Action {
		private readonly Directionable direction;

		public StartMinimize(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			direction.SetWeight(Penguin.Weight.Small);
			direction.SetMinimizing(true);
			direction.SetInitialTime(Penguin.CurveType.Minimize, Time.timeSinceLevelLoad);
		}
	}
}
