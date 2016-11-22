using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.shop.item {
	public class HatchablePenguin : ActionableGameEntityImpl<PickupActions> {
		private Actionable<PickupActions> egg;

		public override string GetTag() {
			return TagConstants.HATCHABLE_PENGUIN;
		}

		protected void Update() {
			foreach ( var touch in Input.touches ) {
				if ( touch.phase != TouchPhase.Ended ) {
					continue;
				}

				RaycastHit hit;
				// Create a particle if hit
				if ( Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit)
				     && hit.collider.gameObject == gameObject ) {
					Collect();
				}
			}
		}

		private void OnMouseUp() {
			Collect();
		}

		private void Collect() {
			ExecuteAction(PickupActions.CollectPenguin);
			egg.ExecuteAction(PickupActions.StartNewEgg);
			Destroy(gameObject);
		}

		public void SetEgg(PenguinEgg egg) {
			this.egg = egg;
		}
	}
}
