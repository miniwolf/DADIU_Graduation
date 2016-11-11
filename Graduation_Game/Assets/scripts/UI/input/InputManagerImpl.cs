using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Assets.scripts.UI {
	public class InputManagerImpl : MonoBehaviour, InputManager {

		private List<TouchInputListener> touchListeners = new List<TouchInputListener>();
		private List<MouseInputListener> mouseListeners = new List<MouseInputListener>();
		private List<KeyboardInputListener> keyboardListeners = new List<KeyboardInputListener>();

	    private bool cameraBlocked;

	    void Update() {
			if (IsCameraBlocked()) {
				return;
			}
			Touch();
			Mouse();
			Keyboard();
		}

		private void Touch() {
		    if(Input.touchCount > 0) {
		        foreach(TouchInputListener l in touchListeners) {
					l.OnTouch(Input.touches);
				}
			}
		}

		private void Mouse() {
			foreach(MouseInputListener l in mouseListeners) {
				if(Input.GetMouseButtonDown(0))
					l.OnMouseLeftDown();
				if(Input.GetMouseButtonUp(0))
					l.OnMouseLeftUp();
				if(Input.GetMouseButton(0))
					l.OnMouseLeftPressed();

				if(Input.GetMouseButtonDown(1))
					l.OnMouseRightDown();
				if(Input.GetMouseButtonUp(1))
					l.OnMouseRightUp();
				if(Input.GetMouseButton(1))
					l.OnMouseRightPressed();
			}
		}

		private void Keyboard() {
			foreach(KeyboardInputListener l in keyboardListeners) {
				if(Input.GetKeyDown(KeyCode.LeftArrow))
					l.OnArrowLeft();
				if(Input.GetKeyDown(KeyCode.RightArrow))
					l.OnArrowRight();
			}
		}

		public void SubscribeForTouch(TouchInputListener l) {
			touchListeners.Add(l);
		}
		/// <summary>
		/// Don't forget to call Unsubscribe
		/// </summary>
		/// <param name="l">L.</param>
		public void SubscribeForKeyboard(KeyboardInputListener l) {
			keyboardListeners.Add(l);
		}
		/// <summary>
		/// Don't forget to call Unsubscribe
		/// </summary>
		/// <param name="l">L.</param>
		public void SubscribeForMouse(MouseInputListener l) {
			mouseListeners.Add(l);
		}

		public void UnsubscribeForTouch(TouchInputListener l) {
			touchListeners.Remove(l);
		}

		public void UnsubscribeForKeyboard(KeyboardInputListener l) {
			keyboardListeners.Remove(l);
		}

		public void UnsubscribeForMouse(MouseInputListener l) {
			mouseListeners.Remove(l);
		}

	    public void BlockCameraMovement() {
	        cameraBlocked = true;
	    }

	    public void UnblockCameraMovement() {
			cameraBlocked = false;
	    }

	    public bool IsCameraBlocked() {
			return cameraBlocked;
	    }
	}
}

