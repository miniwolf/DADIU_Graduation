using System.Collections;
using Assets.scripts.components;
using Assets.scripts.controllers.actions.tools.lane;
using Assets.scripts.level;
using UnityEngine;

namespace Assets.scripts.controllers.actions.tools {
	public class Switch : Action {
		private readonly Directionable direction;
		private GameObject penguin;
		private MonoBehaviour couroutineHandler;
		private readonly LevelSettings levelSettings;
		private const int layerMask = 1 << 8;
		private readonly LaneSwitch laneSwitch;

		public Switch(Directionable direction, GameObject levelSettings, LaneSwitch laneSwitch) {
			this.direction = direction;
			this.levelSettings = levelSettings.GetComponent<LevelSettings>();
			this.laneSwitch = laneSwitch;
		}

		public void Setup(GameObject gameObject) {
			penguin = gameObject;
			couroutineHandler = gameObject.GetComponent<MonoBehaviour>();
		}

		public void Execute() {
			var oldDirection = direction.GetDirection();
			var oldRotation = penguin.transform.rotation;

			var newRotation = laneSwitch.GetNewRotation(oldRotation);
			var newDirection = newRotation * oldDirection;
			newDirection = new Vector3(newDirection.x, newDirection.y+0.2f,newDirection.z);
			var tempDir = new Vector3(newDirection.x, penguin.transform.position.y, newDirection.z);
			// make sure that penguin can change lane
			RaycastHit hit;
			Debug.DrawRay(penguin.transform.position, newDirection,Color.red);
			if ( Physics.Raycast(new Ray(penguin.transform.position, newDirection),
				out hit, 8f, layerMask) ) {
				Debug.Log("returning");
				return;
			}

			direction.SetDirection(newDirection); //change penguin's direction
			penguin.transform.rotation = newRotation; //rotate penguin
			couroutineHandler.StartCoroutine(LaneReached(penguin.transform.position.z, oldDirection, oldRotation));
		}

		private IEnumerator LaneReached(float zPos, Vector3 oldDirection, Quaternion oldRotation) {
			do {
				// check every 0.25 pulsateInterval if penguin has reached the half of the lane
				yield return new WaitForSeconds(0.05f);
			} while (laneSwitch.LaneSwitchCondition(penguin.transform.position.z, laneSwitch.GetDirection(zPos, levelSettings)) );

			//when half lane reached, change direction to old one
			direction.SetDirection(oldDirection);
			penguin.transform.rotation = oldRotation;

		}
	}
}