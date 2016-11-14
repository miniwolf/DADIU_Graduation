using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.UI.screen.ingame;
using Assets.scripts.UI.screen;
using Assets.scripts.UI;
using System;

namespace Assets.scripts.camera {
	public class MainCameraFreeMove : MonoBehaviour, TouchInputListener, MouseInputListener {
		InputManager inputManager;

		public float cameraMovementSpeed = .5f;
		public Transform[] cameraSteps = new Transform[2];

		private Vector3 dragOrigin;
		private Vector2 dragTouchOrigin;

		// Limiters where the camera is allowed to move to
		private float limitLeftX;
		private float limitRightX;
		private float cameraStopThreshold = 0.01f;


		// Use this for initialization
		void Start() {
			// todo - if this is ever rewritten using InjectionRegister, remove this from here
			inputManager = (InputManager)GameObject.FindObjectOfType<InputManagerImpl>();
			inputManager.SubscribeForMouse(this);
			inputManager.SubscribeForTouch(this);

			limitLeftX = cameraSteps[0].position.x;
			limitRightX = cameraSteps[cameraSteps.Length - 1].position.x;
		}

		public void OnTouch(Touch[] allTouches) {
			Touch touch = allTouches[0];
			dragTouchOrigin = touch.position;

			switch (touch.phase) {	
				case TouchPhase.Began:
					CameraMovementLimit();

					//dragTouchOrigin = touch.position;

					Vector2 pos = Camera.main.ScreenToViewportPoint(touch.position - dragTouchOrigin);
					Vector2 move = new Vector2(pos.x * cameraMovementSpeed, 0);

					// Moves the camera in -x direction when mouse is held down 
					transform.Translate(-move, Space.World);

					break;
				case TouchPhase.Ended:
					break;
			}
		}


		public void OnMouseLeftDown() {
			dragOrigin = Input.mousePosition;
			return;
		}

		public void OnMouseLeftUp() {
			return;
		}

		public void OnMouseLeftPressed() {
			CameraMovementLimit();
			/*
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(pos.x * cameraMovementSpeed, 0, 0);

			// Moves the camera in -x direction when mouse is held down 
			transform.Translate(-move, Space.World);
			*/

			Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector2 move = new Vector3(pos.x * cameraMovementSpeed, 0);

			// Moves the camera in -x direction when mouse is held down 
			transform.Translate(-move, Space.World);


		}

		// Limits the camera in x direction where cameraSteps[0] and cameraSteps[cameraSteps.Length - 1]
		// is placed in the scene
		private void CameraMovementLimit() {
			if (transform.position.x < limitLeftX) {
				transform.position = new Vector3(limitLeftX + cameraStopThreshold, transform.position.y, transform.position.z);
			}

			else if (transform.position.x > limitRightX) {
				transform.position = new Vector3(limitRightX - cameraStopThreshold, transform.position.y, transform.position.z);
			}
		}

		// Only left mouse button is used
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
