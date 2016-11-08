using UnityEngine;
using System.Collections;
using Assets.scripts.components;


namespace Assets.scripts.controllers.actions.traps{
	public class SinkWeightBased : Action {
		private GameObject iceParent;
		private GameObject[] toManipulate = new GameObject[6];
		private float moveFactor;
		private float minHeight;
		private WeightBasedInterface weightInt;

		public void Setup(GameObject obj){
			iceParent = obj;
			weightInt = obj.GetComponent<WeightBasedInterface>();
			moveFactor = weightInt.GetMovementFactor();
			minHeight = weightInt.GetWhenSunk();
			toManipulate = weightInt.GetChildrenToManipulate();
		}

		public void Execute(){
			for (int i = 0; i < toManipulate.Length; i++) {
				toManipulate[i].transform.position -= new Vector3(0, moveFactor, 0);
			}
		}
	}
}

