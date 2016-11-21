using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.traps {
	public class Wire : ActionableGameEntityImpl<TrapActions> {
		private bool started;
		public int pulsateInterval = 3;
		public int startTime = 2;

		// Update is called once per frame
		protected void Update () {
			if ( !(Time.fixedTime >= startTime) || started ) {
				return;
			}

			ExecuteAction(TrapActions.PULSATE);
			started = true;
		}

		protected void OnTriggerEnter(Collider other) {
			if ( other.tag == TagConstants.PENGUIN ) {
				other.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.KillPenguinByElectricution);
			}else if(other.tag == TagConstants.SEAL){
				other.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.SealDeath);
			}
		}

		public override string GetTag() {
			return TagConstants.WIRE;
		}

		public float GetPulsateInterval() {
			return pulsateInterval;
		}
	}
}
