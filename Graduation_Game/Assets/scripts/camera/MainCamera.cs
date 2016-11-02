﻿using UnityEngine;using System.Collections;public class MainCamera : MonoBehaviour {    public float speed = 10f;	public bool isPlacing = false;	public Transform[] placements = new Transform[10];	public Vector3 startPos, endPos;	public bool freeMove = false;	Vector3 lastPos;	public float factor;    private int currentCameraIndex = 0;    public float cameraSpeed = .5f;    private Vector3 start;    private Vector3 des;    private float fraction = 1;    // Update is called once per frame	void Update() {	    if (Input.GetKeyDown(KeyCode.LeftArrow) && !freeMove)			if (currentCameraIndex > 0) {				currentCameraIndex--;//				transform.position = new Vector3(placements[currentCameraIndex].position.x, transform.position.y,//					placements[currentCameraIndex].position.z);			        start = transform.position;			        des = new Vector3(placements[currentCameraIndex].position.x, transform.position.y,	            placements[currentCameraIndex].position.z);			        fraction = 0;			    }	    if (Input.GetKeyDown(KeyCode.RightArrow) && !freeMove)			if (currentCameraIndex < placements.Length - 1) {				currentCameraIndex++;			        start = transform.position;				des = new Vector3(placements[currentCameraIndex].position.x, transform.position.y,					placements[currentCameraIndex].position.z);			        fraction = 0;			    }	    if (Input.touchCount > 0 && !isPlacing && !freeMove) {			Touch touch = Input.GetTouch(0);			switch (touch.phase) {				case TouchPhase.Began:					startPos = touch.position;					break;				case TouchPhase.Ended:					endPos = touch.position;					StaticMove();					break;			}		}	    if (Input.touchCount > 0 && !isPlacing && freeMove) {			Touch touch = Input.GetTouch(0);			switch (touch.phase) {				case TouchPhase.Began:					startPos = touch.position;					lastPos = touch.position;					break;				case TouchPhase.Moved:					FreeMove(new Vector3((touch.position.x - lastPos.x) * factor,						(touch.position.y - lastPos.y) * factor, 0f));					lastPos = touch.position;					break;				case TouchPhase.Ended:					endPos = touch.position;					break;			}		}	    Debug.Log(fraction);	    if (fraction < 1) {	        fraction += Time.deltaTime * cameraSpeed;	        transform.position = Vector3.Lerp(start, des, fraction);	    }	}	void StaticMove() {		if (startPos.x > endPos.x) {			if (currentCameraIndex < placements.Length - 1) {				currentCameraIndex++;				transform.position = placements[currentCameraIndex].position;			}		} else if (startPos.x < endPos.x) {			if (currentCameraIndex > 0) {				currentCameraIndex--;				transform.position = placements[currentCameraIndex].position;			}		}	}	public void SetisPlacing(bool inc) {		isPlacing = inc;	}	void FreeMove(Vector3 vec) {		transform.position -= vec;	}}