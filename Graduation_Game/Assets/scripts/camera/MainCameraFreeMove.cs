using UnityEngine;
using System.Collections.Generic;
using Assets.scripts.UI.screen.ingame;
using Assets.scripts.UI;
using System;

namespace Assets.scripts.camera {
	public class MainCameraFreeMove : MonoBehaviour, TouchInputListener, MouseInputListener {
		InputManager inputManager;

		public float speed = .5f; // Speed of the camera while freely moving
		public float edgeSpeed = .25f; // Speed of camera when dragging nearby edges
		public float screenBoundaryThreshold = 10f; // Determines a window of pixels from left-/rightScreenBoundary
		public Transform[] cameraSteps = new Transform[2]; // Place 2 camera steps in a level to determine the camera boundary positions

		// Holds the initial input positions before starting to drag the camera
		private Vector3 dragOrigin;
		private Vector2 dragTouchOrigin;

		private bool usingTouch;
		private Vector3 lastTouch;

		// Limiters where the camera is allowed to move to
		// Depends on cameraSteps placements
		private float limitLeftX;
		private float limitRightX;

		// Screen boundaries for left and right side
		private float leftScreenBoundary;
		private float rightScreenBoundary;

		// Threshold from camera boundaries
		private float cameraStopThreshold = 0.01f;

		List<Draggable> draggable = new List<Draggable>();

		// Use this for initialization
		void Start() {
			ToolButtons[] os = FindObjectsOfType<ToolButtons>();
			foreach (ToolButtons o in os) {
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
		
		void Update() {
			foreach (Draggable d in draggable) {
				if (d.IsDragging()) { // if any object is dragged, move the camera when touching the screen boundaries
					MoveWhileDragging();
				}
			}
		}


		//
		// TOUCH INPUT
		//

		public void OnTouch(Touch[] allTouches) {
			Touch touch = allTouches[0];

			switch (touch.phase) {
				case TouchPhase.Began:
					usingTouch = true;
					dragTouchOrigin = touch.position;
					lastTouch = touch.position;
					break;
				case TouchPhase.Moved:
					TouchMoveCamera(true, touch);
					lastTouch = touch.position;
					break;
				case TouchPhase.Ended:
					break;
			}
		}

		// Moves the camera in x direction when touch is held down
		private void TouchMoveCamera(bool isFreelyMoving, Touch touch) {
			Vector2 pos = Camera.main.ScreenToViewportPoint(touch.position - dragTouchOrigin);
			Vector2 move = new Vector2(pos.x * speed, 0);
			//transform.Translate(-move, Space.World); // Moves the camera in -x direction while touching and moving the finger
			float xMovement = touch.position.x - lastTouch.x;
			if (Mathf.Abs(xMovement) > 10f && CameraMovementLimit(xMovement*speed)) {
				transform.position -= new Vector3((xMovement) * speed, 0f, 0f);
			}

			if (isFreelyMoving) {
			}
			else {
				// Makes sure to have the same movement speed while dragging on the edges
				if (touch.position.x >= rightScreenBoundary) {
					move = new Vector3(edgeSpeed, 0);
					transform.Translate(move, Space.World);
				}

				if (touch.position.x <= leftScreenBoundary) {
					move = new Vector3(-edgeSpeed, 0);
					transform.Translate(move, Space.World);
				}
			}
		}


		//
		// LEFT MOUSE BUTTON
		//

		// Handles moving the camera from the edges of the screen
		// while dragging an object
		private void MoveWhileDragging() {
			if (Input.mousePosition.x >= rightScreenBoundary) {
				MoveCamera(false);
			}

			if (Input.mousePosition.x <= leftScreenBoundary) {
				MoveCamera(false);
			}
		}

		public void OnMouseLeftDown() {
			dragOrigin = Input.mousePosition;
			lastTouch = Input.mousePosition;
			//lastTouch = Input.mousePosition;
			return;
		}

		public void OnMouseLeftUp() {
			return;
		}

		public void OnMouseLeftPressed() {
			MoveCamera(true);
			lastTouch = Input.mousePosition;
		}

		// Moves the camera in x direction when mouse is held down
		private void MoveCamera(bool isFreelyMoving) {
			if (usingTouch) {
				return;
			}

			Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector2 move = new Vector3(pos.x * speed, 0);

			float xMovement = Input.mousePosition.x - lastTouch.x;
			if (Mathf.Abs(xMovement) > 10f && CameraMovementLimit(xMovement*speed)) {
				transform.position -= new Vector3((xMovement) * speed, 0f, 0f);
			}

			if (isFreelyMoving) {
			}
			else {
				if (Input.mousePosition.x >= rightScreenBoundary) {
					move = new Vector3(edgeSpeed, 0);
					transform.Translate(move, Space.World);
				}

				if (Input.mousePosition.x <= leftScreenBoundary) {
					move = new Vector3(-edgeSpeed, 0);
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

			if (transform.position.x - xMove > limitLeftX && transform.position.x - xMove < limitRightX) {
				return true;
			} else {
				return false;
			}
		}

		//
		// RIGHT MOUSE BUTTON (not used)
		//

		public void OnMouseRightDown() {
			throw new NotImplementedException();
		}

		public void OnMouseRightUp() {
			throw new NotImplementedException();
		}

		public void OnMouseRightPressed() {
			throw new NotImplementedException();
		}

	}
}
