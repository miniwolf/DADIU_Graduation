using UnityEngine;

namespace Assets.scripts.controllers.actions.traps{
	public class SinkWeightBased : Action {
		private readonly GameObject[] toManipulate;
		private readonly float moveFactor;

		public SinkWeightBased(WeightBasedInterface weight) {
			moveFactor = weight.GetMovementFactor();
			toManipulate = weight.GetChildrenToManipulate();
		}

		public void Setup(GameObject obj){
		}

		public void Execute() {
			foreach (var go in toManipulate) {
				go.transform.position -= new Vector3(0, moveFactor, 0);
			}
		}
	}
}
