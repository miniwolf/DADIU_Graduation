using UnityEngine;
using System.Collections;
using Asset.scripts.tools;
using Assets.scripts.controllers.handlers;
using Assets.scripts.components;
using Assets.scripts.controllers;


namespace Assets.scripts.tools {
	public class PressurePlate : MonoBehaviour, Asset.scripts.tools.Tool {
		public readonly Actionable<PressurePlateActions> actionable;

		PressurePlate(Actionable<PressurePlateActions> actionable) {
			this.actionable = actionable;
		}
		// Use this for initialization
		protected void OnTriggerEnter(Collider collision) {
			if ( collision.tag == TagConstants.PENGUIN ) {
				actionable.ExecuteAction(PressurePlateActions.Excute);
			}
		}

		public ToolType GetToolType() {
			return ToolType.PressurePlate;
		}
	}
}
