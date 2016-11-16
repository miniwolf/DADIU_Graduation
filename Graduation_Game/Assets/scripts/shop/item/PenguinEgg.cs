using System;
using System.Collections;
using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.shop.item {
	public class PenguinEgg : ActionableGameEntityImpl<PickupActions> {
		private bool hatchable;
		public bool IsReady { get; set; }
		public string ID { get; set; }
		public DateTime HatchTime { get; set; }
		public int shakeInterval = 2;

		public override string GetTag() {
			return TagConstants.PENGUINEGG;
		}

		protected void Update() {
			if ( !hatchable && DateTime.Now >= HatchTime ) {
				hatchable = true;
				StartCoroutine(Hatchable());
			}
			foreach (var touch in Input.touches) {
				if ( touch.phase != TouchPhase.Ended ) {
					continue;
				}

				RaycastHit hit;
				// Create a particle if hit
				if ( Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit)
					 && hit.collider.gameObject == gameObject ) {
					ExecuteAction(PickupActions.HatchEgg);
				}
			}
		}

		private IEnumerator Hatchable() {
			while ( true ) {
				ExecuteAction(PickupActions.ShakeEgg);
				yield return new WaitForSeconds(shakeInterval);
			}
		}

		private void OnMouseUp() {
			ExecuteAction(PickupActions.HatchEgg);
		}

		public void DestroyThis() {
			Destroy(gameObject);
		}
	}
}
