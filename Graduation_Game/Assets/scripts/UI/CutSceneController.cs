using UnityEngine;
using System.Collections;
using Assets.scripts.components;
using Assets.scripts.controllers;
using System;
using UnityEngine.UI;

namespace Assets.scripts.UI {
	public class CutSceneController : ActionableGameEntityImpl<GameActions> {

		public float triggerTime;
		public bool displayCutscene = false;
		// Use this for initialization
		void Start() {

		}

		public void ShowCutScene() {
			GetComponent<Image>().enabled = false;
			ExecuteAction(GameActions.TriggerCutScene);
		}

		public override string GetTag() {
			return TagConstants.CUTSCENE;
		}
		public bool GetDisplayCutScene() {
			return displayCutscene;
		}
	}
}
