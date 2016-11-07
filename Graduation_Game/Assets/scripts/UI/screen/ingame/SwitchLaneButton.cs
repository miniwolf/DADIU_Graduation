using System.Linq;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
	public class SwitchLaneButton : MonoBehaviour {

		// TODO maybe take z position of the penguin as a ref and add it to the offsets
		public float leftLaneOffset = 1f;
		public float rightLaneOffset = -1f;


		private GameObject switchLaneTool;
		private GameObject player;
		private bool dragging;
		private Vector3 mouseHitPosition;
		private bool thisIsBeingPlaced = false;

		public void PlaceSwitchLane() {
			dragging = true;
			thisIsBeingPlaced = true;
			var switchLaneObj = GetSwitchLane(GameObject.FindGameObjectWithTag(TagConstants.SPAWNPOOL));
			switchLaneTool = Instantiate(switchLaneObj);
			switchLaneTool.SetActive(true);
		}

		private GameObject GetSwitchLane(GameObject spawnPool) {
			return (from child
				in spawnPool.GetComponentsInChildren<Transform>()
				where child.tag == TagConstants.SWITCHTEMPLATE
				select child.gameObject).FirstOrDefault();
		}

		void Update() {
			foreach ( var touch in Input.touches) {
				if ( touch.phase == TouchPhase.Moved && thisIsBeingPlaced ) {
					PlaceObject(touch.position);
				}
				if ( touch.phase == TouchPhase.Ended && thisIsBeingPlaced ) {
					Snap();
				}
			}

			if ( Input.GetMouseButton(0) && thisIsBeingPlaced ) {
				PlaceObject(Input.mousePosition);
			}
			// Release object to the scene
			if ( Input.GetMouseButtonUp(0) && thisIsBeingPlaced ) {
				Snap();
			}
		}

		private void PlaceObject(Vector3 position) {
			if ( !dragging ) {
				return;
			}

			Ray ray =  Camera.main.ScreenPointToRay(position);
			RaycastHit hit;

			if ( Physics.Raycast(ray, out hit) ) {
				if ( hit.transform.tag.Equals(TagConstants.LANE) ) {
					switchLaneTool.transform.position = hit.point;
				}
			}
		}

		private void Snap() {
			dragging = false;
			thisIsBeingPlaced = false;
			switchLaneTool.GetComponentInChildren<SphereCollider>().enabled = true;
			/*
			// Handles snapping on the left lane
			switchLaneTool.transform.position =
				Mathf.Abs(leftLaneOffset - mouseHitPosition.z) < Mathf.Abs(rightLaneOffset - mouseHitPosition.z)
				? new Vector3(mouseHitPosition.x, mouseHitPosition.y, leftLaneOffset)
				: new Vector3(mouseHitPosition.x, mouseHitPosition.y, rightLaneOffset);
			*/
		}
	}
}
