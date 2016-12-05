using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.tools {
	public class Jump : ActionableGameEntityImpl<ToolActions> {
		public GameObject lastPenguin;

		protected void OnTriggerStay(Collider collision) {
			if ( collision.tag == TagConstants.PENGUIN ) {
				collision.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.StartJump);
			}
		}

		private void OnTriggerEnter(Collider collision) {
			if ( collision.tag != TagConstants.PENGUIN ) {
				return;
			}

			ExecuteAction(ToolActions.OnTrigger);
		}

		public override string GetTag() {
			return TagConstants.JUMP;
		}
	}
}
