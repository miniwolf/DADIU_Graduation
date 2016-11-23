using UnityEngine;
using System.Collections;
using Assets.scripts.components;
using Assets.scripts.controllers;
using System;
using UnityEngine.UI;
using Assets.scripts.components.registers;

namespace Assets.scripts.UI {
	public class CutSceneController : MonoBehaviour/*: ActionableGameEntityImpl<GameActions>*/ {

		public float triggerTime;
		public bool displayCutscene = false;
		// Use this for initialization

		void Start() {

		}

		public void ShowCutScene() {
			GetComponent<Image>().enabled = false;
		//	ExecuteAction(GameActions.TriggerCutScene);
		}

		public string GetTag() {
			return TagConstants.CUTSCENE;
		}
		public bool GetDisplayCutScene() {
			return displayCutscene;
		}
	}
}
