using UnityEngine;
using System.Collections;
using Asset.scripts.tools;
using Assets.scripts.controllers.handlers;
using Assets.scripts.components;
using Assets.scripts.controllers;


namespace Assets.scripts.tools {
	
	public class PressurePlate : ActionableGameEntityImpl<PressurePlateActions>, LinkingComponent {
		public GameObject linkingObject;

		protected void OnTriggerEnter(Collider collision) {
			if ( collision.tag == TagConstants.PENGUIN ) {
				ExecuteAction(PressurePlateActions.Excute);
			}
		}
		public override string GetTag() {
			return TagConstants.PRESSURE_PLATE;
		}

		public ObjectControlledByPressurePlate GetLinkingObject() {
			return linkingObject.GetComponent<ObjectControlledByPressurePlate> ();
		}
	}
}
