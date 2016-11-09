using UnityEngine;
using System.Collections;
using Assets.scripts.components;

namespace Assets.scripts.controllers.actions.traps{
	public class LiftWeightBased : Action {
		private GameObject iceParent;
		private GameObject[] toManipulate = new GameObject[6];
		private float moveFactor;
		private float maxHeight;
		private WeightBasedInterface weightInt;

		public void Setup(GameObject obj){
			iceParent = obj;
			weightInt = obj.GetComponent<WeightBasedInterface>();
			moveFactor = weightInt.GetMovementFactor();
			maxHeight = weightInt.GetInitialHeight();
			toManipulate = weightInt.GetChildrenToManipulate();
		}

		public void Execute(){
			if (toManipulate[0].transform.position.y < maxHeight) {
				for (int i = 0; i < toManipulate.Length; i++) {
					toManipulate[i].transform.position += new Vector3(0, 0.5f, 0);
				}
			}
		}
	}
}
