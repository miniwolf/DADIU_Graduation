using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using Assets.scripts.components;
using Assets.scripts.controllers;

// TODO add conditions: 
// Minimize and metal penguin tools does not active this trap

namespace Assets.scripts.traps {
	// Note: This trap uses a trigger and a collider seperately

	public class ExcavatorTrap : MonoBehaviour {
		public float endDist = 0.5f;
		public float resetAfterSeconds = 1.5f;
		private float initialPosY;

		private Vector3 initialPosition;
		private bool immune;

		void Start() {
			initialPosition = transform.position;
			initialPosY = initialPosition.y;
		}

		// The trigger event handles the activation event of the excavator
		protected IEnumerator OnTriggerEnter(Collider other) {
			if ( other.tag == TagConstants.METALTEMPLATE ) {
				ToggleImmune();
			}

			if ( other.transform.tag != TagConstants.PENGUIN || immune ) {
				yield break;
			}

			ExecuteExcavator();
			yield return new WaitForSeconds(resetAfterSeconds);
			ResetExcavator();
		}

		private void ToggleImmune() {
			immune = !immune;
		}

		// The collision event handles the killing of a penguin
		protected void OnCollisionEnter(Collision collision) {
			if (collision.transform.tag == TagConstants.PENGUIN) {
				collision.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinByExcavator);
				collision.collider.enabled = false; // Disables the character controller on the dead penguins
			}
		}

		protected void OnTriggerExit(Collider collider) {
			if ( collider.tag == TagConstants.METALTEMPLATE ) {
				ToggleImmune();
			}
		}

		// Moves the excavator down by endDist
		private void ExecuteExcavator() {
			transform.position = new Vector3(initialPosition.x, endDist, initialPosition.z);
		}

		// Resets the excavator to its initial position and rotation
		private void ResetExcavator() {
			transform.position = new Vector3(initialPosition.x, initialPosY, initialPosition.z);
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		}
	}
}