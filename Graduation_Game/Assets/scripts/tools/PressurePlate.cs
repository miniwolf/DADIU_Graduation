using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.controllers;


namespace Assets.scripts.tools {
	public class PressurePlate : ActionableGameEntityImpl<PressurePlateActions>, LinkingComponent {
        public GameObject linkingObject;
        public bool triggerOnlyOnce;

        protected void OnTriggerEnter(Collider collision) {
			if ( collision.tag == TagConstants.PENGUIN ) {
                if (triggerOnlyOnce) {
                    foreach (Collider c in GetComponents<Collider>())
                        c.enabled = false;
                    ExecuteAction(PressurePlateActions.Excute);
                }
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
