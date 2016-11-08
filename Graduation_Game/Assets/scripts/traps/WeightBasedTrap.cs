using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components;

namespace Assets.scripts.traps{
	public class WeightBasedTrap : ActionableGameEntityImpl<TrapActions>, WeightBasedInterface {

		GameObject[] gos = new GameObject[6]; //it is known that there are only 6 GO's at the time of creation which should be handled using this array.
		int iterator = 0;
		List<GameObject> penguins = new List<GameObject>();

		private float initialHeight;
		private float whenSunk;
		public float maxNegativeYMovement = 4f;
		public float movementFactor = 0.5f;


		void Init(){
			initialHeight = transform.position.y;
			whenSunk = initialHeight - maxNegativeYMovement;
		}


		void DivideChildren(){
			foreach (Transform t in GetComponentsInChildren<Transform>()) {
				if (t.gameObject.GetComponent<MeshRenderer>() != null) {
					gos[iterator] = t.gameObject;
					iterator++;
				}
			}
		}

		void Sinking(){
			ExecuteAction(TrapActions.WeightBasedSinking);
		}
		void Lifting(){
			ExecuteAction(TrapActions.WeightBasedLifting);
		}


		public override string GetTag () {
			return TagConstants.WEIGHTBASED;
		}

		protected void OnTriggerEnter(Collider other){
			if (other.tag == TagConstants.PENGUIN) {
				penguins.Add(other.gameObject);
				if (penguins.Count > 0) {
					Sinking();
				}
			}
		}
		protected void OnTriggerExit(Collider other){
			if (other.tag == TagConstants.PENGUIN) {
				penguins.Remove(other.gameObject);
				if (penguins.Count == 0) {
					Lifting();
				}
			}
		}

		public float GetInitialHeight(){
			return initialHeight;
		}

		public float GetWhenSunk(){
			return whenSunk;
		}

		public GameObject[] GetChildrenToManipulate(){
			return gos;
		}

		public float GetMovementFactor(){
			return movementFactor;
		}
	}
}