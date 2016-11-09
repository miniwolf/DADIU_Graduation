using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class StartEnlarge : Action {
		private Directionable direction;

		public StartEnlarge(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			return;
		}

		public void Execute() {
			direction.SetEnlarging(true);
			direction.SetInitialTime(Penguin.CurveType.Enlarge, Time.timeSinceLevelLoad);
		}
	}
}
