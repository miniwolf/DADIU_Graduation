using UnityEngine;
using System.Collections;


namespace Assets.scripts.controllers.actions.traps{
	public class SinkWeightBased : Action {
		private GameObject iceParent;
		private GameObject[] toManipulate = new GameObject[6];
		private float moveFactor;
		private float minHeight;

		public void Setup(GameObject obj){
			iceParent = obj;
			moveFactor = obj.GetComponent<WeightBasedInterface>().GetMovementFactor();
			minHeight = obj.GetComponent<WeightBasedInterface>().GetWhenSunk();
			toManipulate = obj.GetComponent<WeightBasedInterface>().GetChildrenToManipulate();
		}



		public void Execute(){
			if (toManipulate[0].transform.position.y > minHeight) {
				for (int i = 0; i < toManipulate.Length; i++) {
					toManipulate[i].transform.position -= new Vector3(0, moveFactor, 0);
				}
			}
		}
	}
}

