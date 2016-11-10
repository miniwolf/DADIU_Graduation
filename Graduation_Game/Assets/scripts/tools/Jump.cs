using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.tools {
	public class Jump : MonoBehaviour, Tool {
		protected void OnTriggerEnter(Collider collision) {
			if ( collision.tag == TagConstants.PENGUIN ) {
				collision.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.StartJump);
			}
		}

		public ToolType GetToolType() {
			return ToolType.Jump;
		}
	}
}
