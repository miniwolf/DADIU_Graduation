using System.Collections;
using Assets.scripts.components;
using Assets.scripts.traps;
using UnityEngine;

namespace Assets.scripts.controllers.actions.traps {
	public class Pulsate : Action {
		private readonly Wire wire;
		private readonly CouroutineDelegateHandler handler;
		private bool toggle;

		public Pulsate(Wire wire, CouroutineDelegateHandler handler) {
			this.wire = wire;
			this.handler = handler;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			handler.StartCoroutine(StartPulsate(wire, this));
		}

		private static IEnumerator StartPulsate(Wire wire, Pulsate pulsate) {
			while ( true ) {
				yield return new WaitForSeconds(wire.GetPulsateInterval());
				wire.GetGameObject().SetActive(pulsate.GetToggle());
				pulsate.Toggle();
			}
		}

		private void Toggle() {
			toggle = !toggle;
		}

		private bool GetToggle() {
			return toggle;
		}
	}
}
