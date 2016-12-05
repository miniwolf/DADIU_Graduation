using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.tools {
	public class Jump : ActionableGameEntityImpl<ToolActions> {
		protected void OnTriggerStay(Collider collision) {
			if ( collision.tag == TagConstants.PENGUIN ) {
				ExecuteAction(ToolActions.OnTrigger);
				collision.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.StartJump);
			}
		}

		public override string GetTag() {
			return TagConstants.JUMP;
		}
	}
}
