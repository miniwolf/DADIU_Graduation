using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine.UI;

namespace Assets.scripts.tools {
	public class Star : ActionableGameEntityImpl<GameActions> {
		void Start(){
			gameObject.GetComponent<Image>().enabled = false;
		}
		public void FlyIn() {
			
			gameObject.GetComponent<Image>().enabled = true;
			//ExecuteAction(GameActions.TriggerStar);
		}

		public override string GetTag() {
			return gameObject.tag;
		}
	}
}