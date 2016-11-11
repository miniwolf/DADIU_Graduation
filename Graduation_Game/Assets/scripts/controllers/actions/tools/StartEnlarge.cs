using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class StartEnlarge : Action {
		private readonly Directionable direction;

		public StartEnlarge(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			direction.SetWeight(Penguin.Weight.Big);
			direction.SetEnlarging(true);
			direction.SetInitialTime(Penguin.CurveType.Enlarge, Time.timeSinceLevelLoad);
		}
	}
}
