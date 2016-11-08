using Assets.scripts.components;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.scripts.controllers.actions.tools.pressurePlate{
	public class TriggerLinkingComponent : Action {
		private GameObject gameObject;
		private ObjectControlledByPressurePlate linkingObject;

		public TriggerLinkingComponent(LinkingComponent linkingObject){
			this.linkingObject = linkingObject.GetLinkingObject();
		}

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
		}

		public void Execute() {
			linkingObject.Trigger();
		}
	}
}