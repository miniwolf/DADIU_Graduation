﻿using UnityEngine;using System.Collections;namespace Assets.scripts.camera {    public class MainCamera : MonoBehaviour {        public float cameraMovementSpeed = .5f;        public bool smoothMove = true;        public float factor;        //  player is placing objects in to the scene        public bool isPlacing = false;        // points where camera can move        public Transform[] cameraSteps = new Transform[10];        private Vector3 startPos, endPos; // we should get ride of this        private bool freeMove = false; // we should get ride of this        Vector3 lastPos; // we should get ride of this        private int currentCameraStepIndex = 0;        private Vector3 cameraStart;        private Vector3 cameraDestination;        private float cameraMovementFraction = 1;        // Update is called once per frame        void Update() {            /// keyboard handling            if (Input.GetKeyDown(KeyCode.LeftArrow) && !freeMove)                MoveLeft();            if (Input.GetKeyDown(KeyCode.RightArrow) && !freeMove)                MoveRight();            // touch handling            if (Input.touchCount > 0 && !isPlacing && !freeMove) {                Touch touch = Input.GetTouch(0);                switch (touch.phase) {                    case TouchPhase.Began:                        startPos = touch.position;                        break;                    case TouchPhase.Ended:                        endPos = touch.position;                        StaticMove();                        break;                }            }            if (Input.touchCount > 0 && !isPlacing && freeMove) {                Touch touch = Input.GetTouch(0);                switch (touch.phase) {                    case TouchPhase.Began:                        startPos = touch.position;                        lastPos = touch.position;                        break;                    case TouchPhase.Moved:                        FreeMove(new Vector3((touch.position.x - lastPos.x) * factor, (touch.position.y - lastPos.y) * factor, 0f));                        lastPos = touch.position;                        break;                    case TouchPhase.Ended:                        endPos = touch.position;                        break;                }            }            // smoothly move the camera with each update if smooth move enabled            if(smoothMove)                SmoothMove();        }        private void SmoothMove() {            if (cameraMovementFraction < 1) {                cameraMovementFraction += Time.deltaTime * cameraMovementSpeed;                transform.position = Vector3.Lerp(cameraStart, cameraDestination, cameraMovementFraction);            }        }        private void MoveRight() {            if (currentCameraStepIndex < cameraSteps.Length - 1) {                currentCameraStepIndex++;               Vector3 next = new Vector3(cameraSteps[currentCameraStepIndex].position.x, transform.position.y, cameraSteps[currentCameraStepIndex].position.z);                if (smoothMove) {                    cameraStart = transform.position;                    cameraDestination = next;                        cameraMovementFraction = 0;                } else{                    transform.position = next;                }            }        }        private void MoveLeft() {            if (currentCameraStepIndex > 0) {                currentCameraStepIndex--;                Vector3 next = new Vector3(cameraSteps[currentCameraStepIndex].position.x, transform.position.y, cameraSteps[currentCameraStepIndex].position.z);                if (smoothMove) {                    cameraStart = transform.position;                    cameraDestination = next;                    cameraMovementFraction = 0;                } else {                    transform.position = next;                }            }        }        void StaticMove() {            if (startPos.x > endPos.x) {                MoveRight();            } else if (startPos.x < endPos.x) {                MoveLeft();            }        }        public void SetisPlacing(bool inc) {            isPlacing = inc;        }        void FreeMove(Vector3 vec) {            transform.position -= vec;        }    }}