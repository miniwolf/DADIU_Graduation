using UnityEngine;
using System.Collections.Generic;
using Assets.scripts.UI;
using Assets.scripts.UI.input;
using Assets.scripts.UI.screen.ingame;

namespace Assets.scripts.camera {
	public class MainCameraFreeMove : MonoBehaviour, TouchInputListener, MouseInputListener {
		private InputManager inputManager;

		public float speed = .5f; // Speed of the camera while freely moving
		public float edgeSpeed = .25f; // Speed of camera when dragging nearby edges
		public float screenBoundaryThreshold = 10f; // Determines a window of pixels from left-/rightScreenBoundary
		public Transform[] cameraSteps = new Transform[2]; // Place 2 camera steps in a level to determine the camera boundary positions
		public bool popUpOn = false;

		private bool usingTouch;
		private Vector3 lastTouchPosPos;
		private Touch lastTouch;

		// Limiters where the camera is allowed to move to
		// Depends on cameraSteps placements
		private float limitLeftX;
		private float limitRightX;

		// Screen boundaries for left and right side
		private float leftScreenBoundary;
		private float rightScreenBoundary;

		private readonly List<Draggable> draggable = new List<Draggable>();

		// Use this for initialization
		private void Start() {
			var os = FindObjectsOfType<ToolButtons>();
			foreach ( var o in os ) {
				draggable.Add(o);
			}

			// todo - if this is ever rewritten using InjectionRegister, remove this from here
			inputManager = FindObjectOfType<InputManagerImpl>();
			inputManager.SubscribeForMouse(this);
			inputManager.SubscribeForTouch(this);

			limitLeftX = cameraSteps[0].position.x;
			limitRightX = cameraSteps[cameraSteps.Length - 1].position.x;

			leftScreenBoundary = screenBoundaryThreshold;
			rightScreenBoundary = Screen.width - screenBoundaryThreshold;
		}

		private void Update() {
			foreach ( var d in draggable ) {
				if ( !d.IsDragging() ) {
					continue;
				}
// if any object is dragged, move the camera when touching the screen boundaries
				if ( usingTouch ) {
					MoveWhileDragging(lastTouch);
				} else {
					MoveWhileDragging();
				}
			}
		}

		//
		// TOUCH INPUT
		//
		public void OnTouch(Touch[] allTouches) {
			usingTouch = true;
			var touch = allTouches[0];
			if ( !inputManager.IsCameraBlocked() ) {

				if ( touch.phase == TouchPhase.Moved ) {
					CameraMovement(true, touch.position.x);
				}

				lastTouchPosPos = touch.position;
			}
			lastTouch = touch;
		}


		//
		// LEFT MOUSE BUTTON
		//

		// Handles moving the camera from the edges of the screen
		// while dragging an object
		private void MoveWhileDragging() {
			if ( Input.mousePosition.x >= rightScreenBoundary ) {
				CameraMovement(false, Input.mousePosition.x);
			}

			if ( Input.mousePosition.x <= leftScreenBoundary ) {
				CameraMovement(false, Input.mousePosition.x);
			}
		}

		private void MoveWhileDragging(Touch touch) {
			if ( touch.position.x >= rightScreenBoundary ) {
				CameraMovement(false, touch.position.x);
			}

			if ( touch.position.x <= leftScreenBoundary ) {
				CameraMovement(false, touch.position.x);
			}
		}

		public void OnMouseLeftDown() {
			lastTouchPosPos = Input.mousePosition;
		}

		public void OnMouseLeftUp() {
		}

		public void OnMouseLeftPressed() {
			if ( usingTouch ) {
				return;
			}

			CameraMovement(true, Input.mousePosition.x);
			lastTouchPosPos = Input.mousePosition;
		}

		private void CameraMovement(bool isFreelyMoving, float inputX) {
			if (!popUpOn) {
				if (isFreelyMoving) {
					var xMovement = inputX - lastTouchPosPos.x;
					if (Mathf.Abs(xMovement) > 10f && CameraMovementLimit(xMovement * speed)) {
						transform.position -= new Vector3((xMovement) * speed, 0f, 0f);
					}
				}
				else {
					Vector2 move;
					if (Input.mousePosition.x >= rightScreenBoundary) {
						move = new Vector3(edgeSpeed, 0);
					}
					else if (Input.mousePosition.x <= leftScreenBoundary) {
						move = new Vector3(-edgeSpeed, 0);
					}
					else {
						return;
					}
					transform.Translate(move, Space.World);
				}
			}
		}

		//
		// GENERAL FUNCTIONS
		//

		// Limits the camera in x direction where cameraSteps[0] and 
		// cameraSteps[cameraSteps.Length - 1] is placed in the scene
		private bool CameraMovementLimit(float xMove) {
			return transform.position.x - xMove > limitLeftX && transform.position.x - xMove < limitRightX;
		}

		//
		// RIGHT MOUSE BUTTON (not used)
		//
		public void OnMouseRightDown() {
		}

		public void OnMouseRightUp() {
		}

		public void OnMouseRightPressed() {
		}
	}
}
