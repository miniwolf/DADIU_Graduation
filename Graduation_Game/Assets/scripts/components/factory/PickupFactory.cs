﻿using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.pickups;
using Assets.scripts.controllers.actions.sound;
using Assets.scripts.controllers.handlers;
using Assets.scripts.sound;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.components.factory {
	public class PickupFactory {
	    private static CouroutineDelegateHandler coroutineDelegator;
		private static readonly Camera c = Camera.main;

		public PickupFactory(CouroutineDelegateHandler handler) {
	        coroutineDelegator = handler;
	    }

		public void BuildPlutonium(Actionable<PickupActions> actionable) {
			actionable.AddAction(PickupActions.PickupPlutonium, PickupPlutonium(actionable));
			actionable.AddAction(PickupActions.FlowScore, FlowScore(actionable));
			actionable.AddAction(PickupActions.CurrencyFly, FlyToScore());
			actionable.AddAction(PickupActions.CurrencyAdd, AddToScore());
		}

		private static Handler PickupPlutonium(Actionable<PickupActions> actionable) {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new DespawnPlutonium(coroutineDelegator, GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>(), actionable));
			actionHandler.AddAction(new PostSoundEvent(SoundConstants.PickUpSounds.PICKUP_CURRENCY));
			return actionHandler;
		}

		private static Handler FlowScore(Actionable<PickupActions> actionable) {
			var textTotal = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_TOTAL).GetComponent<Text>();
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new DespawnPlutonium(coroutineDelegator, GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>(), actionable, textTotal));
			//actionHandler.AddAction(new PostSoundEvent(SoundConstants.CURRENCY_FLY));
			return actionHandler;
		}

		private static Handler AddToScore(){
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new PostSoundEvent(SoundConstants.PickUpSounds.PICK_UP_ADD, c));
			return actionHandler;
		}

		private static Handler FlyToScore(){
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new PostSoundEvent(SoundConstants.PickUpSounds.CURRENCY_FLY, c));
			return actionHandler;
		}

		public void BuildEgg(Actionable<PickupActions> actionable) {
			actionable.AddAction(PickupActions.HatchEgg, HatchEgg());
			actionable.AddAction(PickupActions.ShakeEgg, ShakeEgg());
			actionable.AddAction(PickupActions.StartNewEgg, StartNewEgg());
		}

		private static Handler ShakeEgg() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new ShakeEgg()); // TODO: Probably some animation
			return actionHandler;
		}

		private static Handler HatchEgg() {
			var actionHandler = new ActionHandler();
			actionHandler.AddAction(new HatchEgg());
			actionHandler.AddAction(new PostSoundEvent(SoundConstants.StoreSounds.EGG_HATCH));
			return actionHandler;
		}

		public void BuildHatchableEgg(Actionable<PickupActions> actionable) {
			actionable.AddAction(PickupActions.CollectPenguin, CollectPenguin());
		}

		private static Handler StartNewEgg() {
			var handler = new ActionHandler();
			handler.AddAction(new SpawnNewTimer());
			return handler;
		}

		private static Handler CollectPenguin() {
			var handler = new ActionHandler();
			handler.AddAction(new CollectPenguin());
			return handler;
		}
	}
}
