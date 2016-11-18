using UnityEngine;
using Assets.scripts.controllers.actions;
using Assets.scripts.components;
using Assets.scripts.controllers;

namespace AssemblyCSharp {
	public class SealMoveForward : Action {
		private CharacterController chrController;
		private Actionable<ControllableActions> actionable;
		private GameObject seal;
		public SealMoveForward (Actionable<ControllableActions> actionable) {
			this.actionable = actionable;
		}

		public void Setup (GameObject gameObject) {
			chrController = gameObject.GetComponent<CharacterController>();
			seal = gameObject;
		}

		public void Execute () {
			if (!chrController.isGrounded) {
				chrController.Move(new Vector3(0, -9.82f, 0) * Time.deltaTime);
				if(!Physics.Raycast(seal.transform.position,-Vector3.up,0.7f)){
					actionable.ExecuteAction(ControllableActions.SealFall);
				}
			} else {
				chrController.Move(new Vector3(1, 0, 0) * 2f * Time.deltaTime);
			}
		}
	}
}

