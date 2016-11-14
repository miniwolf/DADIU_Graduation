using UnityEngine;
using Assets.scripts.character;
using Assets.scripts.controllers.actions;
using Assets.scripts.components;
using Assets.scripts.controllers;

namespace AssemblyCSharp {
	public class StopMoving : Action {
		private readonly Actionable<ControllableActions> actionable;
		Penguin penguin;

		public StopMoving(Actionable<ControllableActions> actionable){
			this.actionable = actionable;
		}


		public void Setup(GameObject go){
			penguin = go.GetComponent<Penguin>();
		}

		public void Execute(){
			penguin.SetStop(true);
			actionable.ExecuteAction(ControllableActions.Freeze);
		}
	}
}

