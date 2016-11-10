using UnityEngine;
using Assets.scripts.character;
using Assets.scripts.components;

namespace Assets.scripts.controllers.actions.tools {
	public class StartSpeed : Action {
		private readonly Directionable direction;

		public StartSpeed(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			direction.SetRunning(true);
			direction.SetInitialTime(Penguin.CurveType.Speed, Time.timeSinceLevelLoad);
		}
	}
}
