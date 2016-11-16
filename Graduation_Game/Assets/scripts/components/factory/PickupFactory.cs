using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.game;
using Assets.scripts.controllers.actions.pickups;
using Assets.scripts.controllers.handlers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.components.factory {
	public class PickupFactory : Factory {
		private readonly Actionable<PickupActions> actionable;
	    private readonly CouroutineDelegateHandler coroutineDelegator;

	    public PickupFactory(Actionable<PickupActions> actionable, CouroutineDelegateHandler handler){
			this.actionable = actionable;
	        coroutineDelegator = handler;
	    }

		public void Build() {
			actionable.AddAction(PickupActions.PickupPlutonium, PickupPlutonium());
			actionable.AddAction(PickupActions.FlowScore, FlowScore());
		}
		
		private Handler PickupPlutonium() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new DespawnPlutonium(coroutineDelegator, GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>()));
			return actionHandler;
		}

		private Handler FlowScore() {
			Text textTotal = null;
			foreach (Transform g in GameObject.FindGameObjectWithTag(TagConstants.CANVAS).GetComponentsInChildren<Transform>(true))
				if (g.tag == TagConstants.ENDSCENE) {
					textTotal = g.GetChild(0).GetChild(3).GetComponent<Text>();
					break;
				}
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new DespawnPlutonium(coroutineDelegator, GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>(), textTotal));
			return actionHandler;
		}
	}
}
