using System.Linq;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
	public class JumpButton : MonoBehaviour {

		private GameObject jumpObjTool;
		private bool dragging;

		// TODO maybe take z position of the penguin as a ref and add it to the offsets
		public float leftLaneOffset = 1f;
		public float rightLaneOffset = -1f;

		private Vector3 mouseHitPosition;

		public void PlaceJump() {
			dragging = true;
			var findGameObjectWithTag = GameObject.FindGameObjectWithTag(TagConstants.SPAWNPOOL);
			var obj = GetJumpButton(findGameObjectWithTag);
			jumpObjTool = Instantiate(obj);
			jumpObjTool.SetActive(true);
		}

		private static GameObject GetJumpButton(GameObject spawnPool) {
			return (from child
				in spawnPool.GetComponentsInChildren<Transform>()
				where child.tag == TagConstants.JUMPTEMPLATE
				select child.gameObject).FirstOrDefault();
		}

		protected void Update() {
			foreach ( var touch in Input.touches ) {
				if ( touch.phase == TouchPhase.Moved ) {
					PlaceObject(touch.position);
				}
				if ( touch.phase == TouchPhase.Ended ) {
					//Snap();
				}
			}

			if ( Input.GetMouseButton(0) ) {
				PlaceObject(Input.mousePosition);
			}
			// Release object to the scene
			if ( Input.GetMouseButtonUp(0) ) {
				//Snap();
			}
		}

		private void PlaceObject(Vector3 position) {
			if ( !dragging ) {
				return;
			}

			var ray =  Camera.main.ScreenPointToRay(position);
			RaycastHit hit;

			if ( Physics.Raycast(ray, out hit) ) {
				if ( hit.transform.tag.Equals(TagConstants.LANE) ) {
					jumpObjTool.transform.position = hit.point;
				}
			}
		}

		private void Snap() {
			dragging = false;

			// Handles snapping on the left lane
			jumpObjTool.transform.position =
				Mathf.Abs(leftLaneOffset - mouseHitPosition.z) < Mathf.Abs(rightLaneOffset - mouseHitPosition.z)
					? new Vector3(mouseHitPosition.x, mouseHitPosition.y, leftLaneOffset)
					: new Vector3(mouseHitPosition.x, mouseHitPosition.y, rightLaneOffset);
		}
	}
}
