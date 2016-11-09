using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class StartSpeed : Action {
		private Directionable direction;

		public StartSpeed(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			return;
		}

		public void Execute() {
			direction.SetRunning(true);
			direction.SetInitialTime(Penguin.CurveType.Speed, Time.timeSinceLevelLoad);
		}
	}
}
