using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components;
using Assets.scripts.controllers;

namespace Assets.scripts.traps{
	public class WeightBasedTrap : ActionableGameEntityImpl<TrapActions>, WeightBasedInterface {

		GameObject[] gos = new GameObject[6]; //it is known that there are only 6 GO's at the time of creation which should be handled using this array.
		int iterator = 0;
		List<GameObject> penguins = new List<GameObject>();

		private float initialHeight;
		private float whenSunk;
		public float maxNegativeYMovement = 4f;
		public float movementFactor = 0.1f;
		private Coroutine sinking;

		void Start(){
			Init();
			DivideChildren();
		}

		private void Init() {
			initialHeight = transform.position.y;
			whenSunk = initialHeight - maxNegativeYMovement;
		}

		private void DivideChildren() {
			foreach (var t in GetComponentsInChildren<Transform>()) {
				if ( t.gameObject.GetComponent<MeshRenderer>() == null ) {
					continue;
				}

				gos[iterator] = t.gameObject;
				iterator++;
			}
		}

		private void Sinking(float sinkingSpeed) {
			sinking = StartCoroutine(SinkIt(sinkingSpeed));
		}

		private void Lifting() {
			StopCoroutine(sinking);
			ExecuteAction(TrapActions.WEIGHTBASEDLIFTING);
		}


		public override string GetTag() {
			return TagConstants.WEIGHTBASED;
		}

		protected void OnTriggerEnter(Collider other){
			if (other.tag == TagConstants.PENGUIN) {
				penguins.Add(other.gameObject);
				if (penguins.Count > 0) {
					Sinking(0.5f);
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

		IEnumerator SinkIt(float speed){
			while(gos[0].transform.position.y>whenSunk){
				ExecuteAction(TrapActions.WEIGHTBASEDSINKING);
				yield return new WaitForSeconds(speed);
			}
		}
	}
}