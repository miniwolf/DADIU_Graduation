using UnityEngine;
using System.Collections;
using Assets.scripts.components;
using System.Collections.Generic;


namespace Assets.scripts.controllers.actions.traps{
	public class SinkWeightBased : Action {
		private GameObject iceParent;
		private List<GameObject> toManipulate = new List<GameObject>();
		private float moveFactor;
		private float heavinessFactor;
		private WeightBasedInterface weightInt;

		public void Setup(GameObject obj){
			iceParent = obj;
			weightInt = obj.GetComponent<WeightBasedInterface>();
			moveFactor = weightInt.GetMovementFactor();
			heavinessFactor = weightInt.GetHeavinessFactor();
			toManipulate = weightInt.GetChildrenToManipulate();
		}

		public void Execute(){
			for (int i = 0; i < toManipulate.Count; i++) {
				toManipulate[i].transform.position -= new Vector3(0, moveFactor*heavinessFactor, 0);
			}
		}
	}
}

