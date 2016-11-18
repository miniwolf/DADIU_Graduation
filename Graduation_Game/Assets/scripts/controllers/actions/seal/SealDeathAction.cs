using UnityEngine;
using Assets.scripts;
using Assets.scripts.controllers.actions;

namespace AssemblyCSharp {
	public class SealDeathAction : Action {
		private GameObject seal;


		public SealDeathAction () {
			
		}
			
		public void Setup(GameObject gameObject){
			seal = gameObject;
		}

		public void Execute(){
			seal.GetComponent<Seal>().SetisDead(true);
			seal.GetComponent<CharacterController>().enabled = false;
		}
	}
}

