using UnityEngine;
using System.Collections;
using Assets.scripts.character;
using Assets.scripts.components;

namespace Assets.scripts.controllers.actions.tools {
	public class SwitchRight : Action {
		private readonly Directionable direction;
		private GameObject penguin;
		private MonoBehaviour couroutineHandler;
		public float laneWidth = 2.0f;

		public SwitchRight(Directionable direction){
			this.direction = direction;

		}

		public void Setup(GameObject gameObject) {
			penguin = gameObject;
			couroutineHandler = gameObject.GetComponent<MonoBehaviour>();
		}

		public void Execute() {		
			Vector3 oldDirection = direction.GetDirection();
			var oldRotation = penguin.transform.rotation;
			var newRotation = Quaternion.Euler(0, 45, 0);
			Vector3 newDirection = newRotation * direction.GetDirection();
			// make sure that penguin can change lane
			if ( !Physics.Raycast(penguin.transform.position, newDirection, laneWidth) ) {
				direction.SetDirection(newDirection); //change penguin's direction
				penguin.transform.rotation = newRotation; //rotate penguin
				float zPos = penguin.transform.position.z;
				couroutineHandler.StartCoroutine(LaneReached(zPos, oldDirection, oldRotation));
			}
		}

		IEnumerator LaneReached(float zPos, Vector3 oldDirection, Quaternion oldRotation) {
			do {
				// check every 0.25 seconds if penguin has reached the half of the lane
				yield return new WaitForSeconds(0.25f);
			} while(penguin.transform.position.z > zPos - laneWidth);

			//when half lane reached, change direction to old one
			direction.SetDirection(oldDirection); 
			penguin.transform.rotation = oldRotation;
		}
	}
}