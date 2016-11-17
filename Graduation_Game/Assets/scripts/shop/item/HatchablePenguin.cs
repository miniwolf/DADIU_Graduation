using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.shop.item {
	public class HatchablePenguin : ActionableGameEntityImpl<PickupActions> {
		public override string GetTag() {
			return TagConstants.HATCHABLE_PENGUIN;
		}

		protected void Update() {
			foreach (var touch in Input.touches) {
				if ( touch.phase != TouchPhase.Ended ) {
					continue;
				}

				RaycastHit hit;
				// Create a particle if hit
				if ( Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit)
				     && hit.collider.gameObject == gameObject ) {
					ExecuteAction(PickupActions.CollectPenguin);
				}
			}
		}

		private void OnMouseUp() {
			ExecuteAction(PickupActions.CollectPenguin);
		}
	}
}