using UnityEngine;
using Assets.scripts.character;
using Assets.scripts.components;

namespace Assets.scripts.controllers.actions.tools {
	public class Speed : Action {
		private readonly Penguin penguin;
		private readonly Directionable diretionable;

		public Speed(Penguin penguin, Directionable directionable) {
			this.penguin = penguin;
			this.diretionable = directionable;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			var newSpeed = diretionable.GetWalkSpeed() *
						   penguin.GetCurve().Evaluate(Time.timeSinceLevelLoad - penguin.GetInitialRunTime());
			diretionable.SetSpeed(newSpeed);
		}
	}
}
