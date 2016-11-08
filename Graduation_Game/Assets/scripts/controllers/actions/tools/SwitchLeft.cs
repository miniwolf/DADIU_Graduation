using UnityEngine;
using System.Collections;
using Assets.scripts.components;
using Assets.scripts.level;

namespace Assets.scripts.controllers.actions.tools {
	public class SwitchLeft : Action {
		private readonly Directionable direction;
		private GameObject penguin;
		private MonoBehaviour couroutineHandler;
		private LevelSettings levelSettings;
		int layerMask = 1 << 8;

		public SwitchLeft(Directionable direction, GameObject levelSettings){
			this.direction = direction;
			this.levelSettings = levelSettings.GetComponent<LevelSettings>();
		}

		public void Setup(GameObject gameObject) {
			penguin = gameObject;
			couroutineHandler = gameObject.GetComponent<MonoBehaviour>();
		}

		public void Execute() {
			Vector3 oldDirection = direction.GetDirection();
			var oldRotation = penguin.transform.rotation;
			Quaternion newRotation;

			if ( oldRotation.y == 0) {
				newRotation = Quaternion.Euler(0, -45, 0);
			} else {
				newRotation = Quaternion.Euler(0, -90, 0);
			}
			Vector3 newDirection = newRotation * direction.GetDirection();
			Vector3 tempDir = new Vector3(newDirection.x, penguin.transform.position.y, newDirection.z);
			// make sure that penguin can change lane
			RaycastHit hit;
			if (!Physics.Raycast(new Ray(penguin.transform.position, tempDir), out hit, levelSettings.GetLaneWidth(),layerMask)
				|| hit.transform.tag == TagConstants.SWITCHTEMPLATE)
			{
				direction.SetDirection(newDirection); //change penguin's direction
				penguin.transform.rotation = newRotation; //rotate penguin
				float zPos = penguin.transform.position.z;
				couroutineHandler.StartCoroutine(LaneReached(zPos, oldDirection, oldRotation));
			}
		}

		IEnumerator LaneReached(float zPos, Vector3 oldDirection, Quaternion oldRotation) {
			do {
				// check every 0.25 seconds if penguin has reached the half of the lane
				yield return new WaitForSeconds(0.05f);
			} while(penguin.transform.position.z < zPos + levelSettings.GetLaneWidth());

			//when half lane reached, change direction to old one
			direction.SetDirection(oldDirection); 
			penguin.transform.rotation = oldRotation;
		}
	}
}
