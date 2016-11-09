using UnityEngine;
using System.Collections;
using Assets.scripts.components;
using Assets.scripts.controllers;


// TODO add conditions: 
// Minimize and metal penguin tools does not active this trap

namespace Assets.scripts.traps {
	public class ExcavatorTrap : MonoBehaviour {
		public float disableAfterSeconds = 1f;

		private Rigidbody rb;

		void Start() {
			rb = GetComponent<Rigidbody>();
		}

		// The trigger event handles the falling event of the excavator
		protected void OnTriggerEnter(Collider other) {
			if (other.transform.tag == TagConstants.PENGUIN) {
				rb.useGravity = true;
				rb.isKinematic = false;
			}
		}

		// The collision event handles the killing of a penguin it lands on
		void OnCollisionEnter(Collision collision) {
			if(collision.transform.tag == TagConstants.PENGUIN) {
				collision.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinByExcavator);
				StartCoroutine(DisableGameObjectAfterSeconds(disableAfterSeconds));

			}
		}

		// Disables the excavator from the scene after seconds
		IEnumerator DisableGameObjectAfterSeconds(float seconds) {
			yield return new WaitForSeconds(seconds);
			gameObject.SetActive(false);
		}

	}
}