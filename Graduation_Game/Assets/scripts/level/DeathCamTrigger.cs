using System;
using UnityEngine;
using System.Collections;
using Assets.scripts.character;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using Assets.scripts.controllers.actions;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UI;


namespace Assets.scripts.level {
	public class DeathCamTrigger : MonoBehaviour {
		public int deathCamVisibleSeconds = 5;

		/// <summary>
		/// Determines if this trigger object is visible by camera (don't show death cam then) or is in one of {Right, Left}OfCamera sides
		/// </summary>
		private enum TriggerRelativePos {
			RightOfCamera,
			LeftOfCamera,
			Visible
		}

		private GameObject deathCamLeft, deathCamRight;

		void Awake() {
			deathCamLeft = GameObject.FindGameObjectWithTag(TagConstants.UI.DEATH_CAM_LEFT);
			deathCamRight = GameObject.FindGameObjectWithTag(TagConstants.UI.DEATH_CAM_RIGHT);
			HideCamerasImmediately();
		}

		// Use this for initialization
		void Start() {
			
		}

		// Update is called once per frame
		void Update() {
			
		}

		void OnTriggerEnter(Collider collider) {
			// when penguin crosses dangerous zone, display death camera in UI and point it to the penguin
			if(TagConstants.PENGUIN.Equals(collider.gameObject.tag)) {
				TriggerRelativePos currentPos = GetCurrentPos();
				if(currentPos == TriggerRelativePos.Visible)
					return; // don't show death cam if user sees the dying penguin

				System.Action hideCamAction;
				GameObject dc;

				switch(currentPos) {
					case TriggerRelativePos.LeftOfCamera:
						dc = deathCamLeft;
						hideCamAction = HideLeftCameraImmediately;
						break;
					case TriggerRelativePos.RightOfCamera:
						dc = deathCamRight;
						hideCamAction = HideRightCameraImmediately;
						break;
					default :
						throw new Exception("Death cam cannot be displayed");
				}

				Camera cam = collider.gameObject.GetComponent<Penguin>().GetDeathCam();
				cam.enabled = true;
				dc.transform.localScale = Vector3.one;
				cam.targetTexture = (RenderTexture)dc.GetComponent<RawImage>().texture;
				StartCoroutine(HideCamera(hideCamAction));
			}
		}

		private TriggerRelativePos GetCurrentPos() {
			var viewport = Camera.main.WorldToViewportPoint(transform.position);
			// x is an offset of how where on the screen from (0,1) is the object displayed. Anything outside this bound is off the screen
			if(viewport.x < 0)
				return TriggerRelativePos.LeftOfCamera;
			if(viewport.x > 1)
				return TriggerRelativePos.RightOfCamera;
			return TriggerRelativePos.Visible;
		}

		IEnumerator HideCamera(System.Action a) {
			yield return new WaitForSeconds(deathCamVisibleSeconds);
			a();
		}

		void HideCamerasImmediately() {
			HideLeftCameraImmediately();
			HideRightCameraImmediately();
		}

		void HideLeftCameraImmediately() {
			HideCamera(deathCamLeft);
		}

		void HideRightCameraImmediately() {
			HideCamera(deathCamRight);
		}

		void HideCamera(GameObject o) {
			o.transform.localScale = Vector3.zero;
		}
	}
}