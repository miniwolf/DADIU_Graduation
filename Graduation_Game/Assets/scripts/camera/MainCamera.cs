﻿using UnityEngine;using System.Collections;using System.Collections.Generic;using Assets.scripts.UI.screen.ingame;using Assets.scripts.UI.screen;using Assets.scripts.UI;namespace Assets.scripts.camera {	public class MainCamera : MonoBehaviour, TouchInputListener, KeyboardInputListener {		public float cameraMovementSpeed = .5f;		public bool smoothMove = true;		public float factor;		// points where camera can move		public Transform[] cameraSteps = new Transform[10];		private Vector3 startPos, endPos;		Vector3 lastPos; // we should get ride of this		private int currentCameraStepIndex = 0;		private Vector3 cameraStart;		private Vector3 cameraDestination;		private float cameraMovementFraction = 1;		// this is blocking the camera from moving while dragging (and touchBlocked blocks camera movement right after drag drop);		List<Draggable> draggable = new List<Draggable>();		bool somethingIsDragged = false, touchBlocked = false;		InputManager inputManager;		void Start() {			ToolButtons [] os = GameObject.FindObjectsOfType<ToolButtons>();			foreach ( ToolButtons o in os ) {				draggable.Add((Draggable) o);			}			// todo - if this is ever rewritten using InjectionRegister, remove this from here			inputManager = (InputManager) GameObject.FindObjectOfType<InputManagerImpl>();			inputManager.SubscribeForKeyboard(this);			inputManager.SubscribeForKeyboard(this);		}		// Update is called once per frame		void Update() {			// hot fix for preventing camera movement when dragging objects into scene			foreach(Draggable d in draggable) {				if(d.IsDragged()) { // if any object is dragged, don't move the camera					somethingIsDragged = true;					return;							} else {					if(somethingIsDragged) {						somethingIsDragged = false;						touchBlocked = true;						StartCoroutine(UnblockTouchHack());					}				}			}							// smoothly move the camera with each update if smooth move enabled			if(smoothMove)				SmoothMove();		}		void OnDestroy() {			inputManager.UnsubscribeForKeyboard(this);			inputManager.UnsubscribeForTouch(this);		}		IEnumerator UnblockTouchHack() {			yield return new WaitForSeconds(.2f);			touchBlocked = false;		}		public void OnArrowRight() {			MoveRight();		}		public void OnArrowLeft() {			MoveLeft();		}		public void OnTouch(Touch[] touches) {			Touch touch = touches[0];			switch (touch.phase) {			case TouchPhase.Began:				startPos = touch.position;				break;			case TouchPhase.Ended:				endPos = touch.position;				SwipeIfNeeded();				break;			}		}		private void SmoothMove() {			if (cameraMovementFraction < 1) {				cameraMovementFraction += Time.deltaTime * cameraMovementSpeed;				transform.position = Vector3.Lerp(cameraStart, cameraDestination, cameraMovementFraction);			}		}		private void MoveRight() {			if (currentCameraStepIndex < cameraSteps.Length - 1) {				currentCameraStepIndex++;				Vector3 next = new Vector3(cameraSteps[currentCameraStepIndex].position.x, transform.position.y, transform.position.z);				if (smoothMove) {					cameraStart = transform.position;					cameraDestination = next;						cameraMovementFraction = 0;				} else{					transform.position = next;				}			}		}		private void MoveLeft() {			if (currentCameraStepIndex > 0) {				currentCameraStepIndex--;				Vector3 next = new Vector3(cameraSteps[currentCameraStepIndex].position.x, transform.position.y,  transform.position.z);				if (smoothMove) {					cameraStart = transform.position;					cameraDestination = next;					cameraMovementFraction = 0;				} else {					transform.position = next;				}			}		}		void SwipeIfNeeded() {			if(touchBlocked)				return;			if (startPos.x > endPos.x) {				MoveRight();			} else if (startPos.x < endPos.x) {				MoveLeft();			}		}	}}