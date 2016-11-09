using UnityEngine;

namespace Assets.scripts.controllers.actions.traps{
	public class LiftWeightBased : Action {
		private readonly GameObject[] toManipulate;
		private readonly float moveFactor;

		public LiftWeightBased(WeightBasedInterface weight) {
			toManipulate = weight.GetChildrenToManipulate();
			moveFactor = weight.GetMovementFactor();
		}

		public void Setup( GameObject obj ) {
		}

		public void Execute() {
			foreach ( var go in toManipulate ) {
				go.transform.position += new Vector3(0, moveFactor, 0);
			}
		}
	}
}
