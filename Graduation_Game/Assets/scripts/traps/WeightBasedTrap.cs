using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts.character;

namespace Assets.scripts.traps{
	public class WeightBasedTrap : ActionableGameEntityImpl<TrapActions>, WeightBasedInterface {
		private readonly List<GameObject> gos = new List<GameObject>(); //it is known that there are only 6 GO's at the time of creation which should be handled using this array.
		private int iterator = 0;
		private readonly List<GameObject> penguins = new List<GameObject>();

		private float initialHeight;
		private float whenSunk;
		public float maxNegativeYMovement = 1.5f;
		public float movementFactor = 0.0000001f;
		private Coroutine sinking, lifting;
		public bool isBothLanes = false, isOnlyRight = true;
		private BoxCollider col; 
		private Penguin.Weight weight;
		public float sinksFasterFactor = 2f; 
		private bool sinkRunning = false, liftRunning = false;

		public int amountOfPenguinsThatMakeItSink = 1;

		protected void Start(){
			Init();
			DivideChildren();
		}

		protected void Init(){
			col = GetComponent<BoxCollider>();
			col.center = new Vector3(col.center.x, 1.3f, isBothLanes ? 0 : isOnlyRight ? -1f : 1f);
			col.size = isBothLanes ? new Vector3(col.size.x, col.size.y, 4f) : new Vector3(col.size.x, col.size.y, 2f);
		}


		private void DivideChildren(){
			foreach (var t in GetComponentsInChildren<Transform>()) {
				if (t.gameObject.GetComponent<MeshRenderer>() != null) {
					gos.Add(t.gameObject);
				}
			}
			initialHeight = gos[0].transform.position.y;
			whenSunk = initialHeight - maxNegativeYMovement;
		}

		private void Sinking() {
			if (liftRunning) {
				StopCoroutine(lifting);
				liftRunning = false;
			}

			if (!sinkRunning) {
				sinking = StartCoroutine(SinkIt());
			}
		}

		private void Lifting(){
			if (sinkRunning) {
				StopCoroutine(sinking);
				sinkRunning = false;
			}
			if (!liftRunning) {
				lifting = StartCoroutine(LiftIt());
			}
		}

		public override string GetTag () {
			return TagConstants.WEIGHTBASED;
		}

		protected void OnTriggerEnter(Collider other){
			if ( other.tag != TagConstants.PENGUIN ) {
				return;
			}

			switch (other.GetComponent<Penguin>().GetWeight()){
				case Penguin.Weight.Big:
					penguins.Add(other.gameObject);
					Sinking();
					break;
				case Penguin.Weight.Small:
					break;
				case Penguin.Weight.Normal:
					penguins.Add(other.gameObject);
					break;
				default:
					break;
			}

			if ( penguins.Count >= amountOfPenguinsThatMakeItSink ) {
				Sinking();
			}
		}

		//POSSIBLY TODO implement OnTriggerStay checking the size of the penguins, if it is possible to place objects on it, and depending on the speed
		protected void OnTriggerExit(Collider other) {
			if ( other.tag != TagConstants.PENGUIN ) {
				return;
			}
			penguins.Remove(other.gameObject);
			if ( penguins.Count == 0 ) {
				Lifting();
			}
		}

		public float GetInitialHeight(){
			return initialHeight;
		}

		public float GetWhenSunk(){
			return whenSunk;
		}

		public List<GameObject> GetChildrenToManipulate(){
			return gos;
		}

		public float GetMovementFactor(){
			return movementFactor;
		}

		public float GetHeavinessFactor(){
			return sinksFasterFactor;
		}

		private IEnumerator SinkIt() {
			sinkRunning = true;
			while(gos[0].transform.position.y>whenSunk){
				ExecuteAction(TrapActions.WEIGHTBASEDSINKING);
				yield return new WaitForEndOfFrame();
			}
			sinkRunning = false;
		}

		private IEnumerator LiftIt() {
			liftRunning = true;
			while(gos[0].transform.position.y<initialHeight){
				ExecuteAction(TrapActions.WEIGHTBASEDLIFTING);
				yield return new WaitForEndOfFrame();
			}
			liftRunning = false;
		}
	}
}
