using System.Collections;
using Assets.scripts.character;
using Assets.scripts.components;
using Assets.scripts.controllers.actions.tools.lane;
using Assets.scripts.level;
using UnityEngine;

namespace Assets.scripts.controllers.actions.tools {
	public class Switch : Action {
		private readonly Directionable directionable;
		private GameObject penguin;
		private MonoBehaviour couroutineHandler;
		private readonly LevelSettings levelSettings;
		private const int layerMask = 1 << 8;
		private readonly LaneSwitch laneSwitch;
		private Penguin peng;
	    private Switch other;

	    private bool isSwitchingLane;  // penguin is currently switching lanes (no shit)

		public Switch(Directionable directionable, GameObject levelSettings, LaneSwitch laneSwitch) {
			this.directionable = directionable;
			this.levelSettings = levelSettings.GetComponent<LevelSettings>();
			this.laneSwitch = laneSwitch;
			directionable.SetGoingTo(laneSwitch.GetType() == typeof(Left)
				? Penguin.Lane.Left
				: Penguin.Lane.Right);
		}

		public void Setup(GameObject gameObject) {
			penguin = gameObject;
			couroutineHandler = gameObject.GetComponent<MonoBehaviour>();
			peng = penguin.GetComponent<Penguin>();
		}

		public void Execute() {
		    couroutineHandler.StartCoroutine(SwitchLane());
		}

	    private IEnumerator SwitchLane() {
	        // start switching lanes after the last lane switch is finished
	        while (other.IsSwitchingLanes()) {
	            yield return new WaitForSeconds(0.05f);
	        }

	        Debug.Log("Starting " + laneSwitch.GetType());
	        isSwitchingLane = true;

	        var oldDirection = directionable.GetDirection();
	        var oldRotation = penguin.transform.rotation;

	        var newRotation = laneSwitch.GetNewRotation(oldRotation);
	        var newDirection = newRotation * oldDirection;
	        newDirection = new Vector3(newDirection.x, newDirection.y+Mathf.Abs(directionable.GetDirection().y)+0.5f,newDirection.z);
	        var tempDir = new Vector3(newDirection.x, penguin.transform.position.y, newDirection.z);
	        // make sure that penguin can change lane
	        RaycastHit hit;

//	        var checkObstacleDirection = newDirection; // todo if penguin is walking uphill, this code works. If it's not walking uphill, set checkObstacleDirection.y and check the obstacle
//	        checkObstacleDirection.y = 0;

	        Debug.DrawRay(penguin.transform.position, newDirection * 100, Color.red, 10000);
	        if ( !Physics.Raycast(new Ray(penguin.transform.position, newDirection * 100), out hit, 1000f, layerMask) ) { // check if there's any obstacle in the direction of a switchlane
                directionable.SetDirection(newDirection); //change penguin's direction
                penguin.transform.rotation = newRotation; //rotate penguin
                // change lane of the penguin
                penguin.GetComponent<Penguin>().SetLane(laneSwitch.GetType() == typeof(Left)
                    ? Penguin.Lane.Left
                    : Penguin.Lane.Right);
                couroutineHandler.StartCoroutine(LaneReached(penguin.transform.position.z, oldDirection, oldRotation));
	        }
	    }

	    private bool IsSwitchingLanes() {
	        return isSwitchingLane;
	    }

	    private IEnumerator LaneReached(float zPos, Vector3 oldDirection, Quaternion oldRotation) {
		    do {
				// check every 0.05 pulsateInterval if penguin has reached the half of the lane
				yield return new WaitForSeconds(0.05f);
//		        Debug.Log("SwitchLaneOngoing - isSwitchingLanes " + isSwitchingLanes + " for " + penguin.gameObject);
		    } while (laneSwitch.LaneSwitchCondition(penguin.transform.position.z, laneSwitch.GetDirection(zPos, levelSettings)) );

			//when half lane reached, change direction to old one
			directionable.SetDirection(new Vector3(oldDirection.x,peng.direction.y,oldDirection.z));
			penguin.transform.rotation = oldRotation;
	        isSwitchingLane = false;
	    }

	    public void SetOther(Switch other) {
	        this.other = other;
	    }
	}
}