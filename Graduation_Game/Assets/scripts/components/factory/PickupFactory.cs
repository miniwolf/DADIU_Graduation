using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.pickups;
using Assets.scripts.controllers.actions.sound;
using Assets.scripts.controllers.handlers;
using Assets.scripts.sound;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.components.factory {
	public class PickupFactory {
	    private static CouroutineDelegateHandler coroutineDelegator;

	    public PickupFactory(CouroutineDelegateHandler handler) {
	        coroutineDelegator = handler;
	    }

		public void BuildPlutonium(Actionable<PickupActions> actionable) {
			actionable.AddAction(PickupActions.PickupPlutonium, PickupPlutonium());
			actionable.AddAction(PickupActions.FlowScore, FlowScore());
		}
		private static Handler PickupPlutonium() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new DespawnPlutonium(coroutineDelegator, GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>()));
			actionHandler.AddAction(new PostSoundEvent(SoundConstants.PICKUP_CURRENCY));
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
			actionHandler.AddAction(new PostSoundEvent(SoundConstants.PICKUP_CURRENCY_FLY));
			return actionHandler;
		}

		public void BuildEgg(Actionable<PickupActions> actionable) {
			actionable.AddAction(PickupActions.HatchEgg, HatchEgg());
			actionable.AddAction(PickupActions.ShakeEgg, ShakeEgg());
		}

		private static Handler ShakeEgg() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new ShakeEgg()); // TODO: Probably some animation
			return actionHandler;
		}

		private static Handler HatchEgg() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new HatchEgg());
			return actionHandler;
		}
	}
}
