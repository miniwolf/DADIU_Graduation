using UnityEngine;
using Assets.scripts.controllers.actions;

namespace AssemblyCSharp {
	public class DefaultBloodSplatterAction : Action {
		GameObject splat;
		GameObject temp;
		GameObject penguin;


		public DefaultBloodSplatterAction(GameObject splat){
			this.splat = splat;
		}

		public void Setup (GameObject gameObject) {
			penguin = gameObject;
		}

		public void Execute () {
			temp = (GameObject)MonoBehaviour.Instantiate (splat, penguin.transform.position, Quaternion.identity);
			MonoBehaviour.Destroy (temp, 2);
		}




	}
}

