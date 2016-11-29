using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.gamestate;
using System.Collections;
using Assets.scripts.UI.translations;
using Assets.scripts.components;
using Assets.scripts.controllers;

namespace Assets.scripts.level {
	public class Tooltip : ActionableGameEntityImpl<ControllableActions> {
		[Tooltip("type of tooltip: jump, switchlane")]
		public string type;
		[Tooltip("time to be frozen")]
		public float time; 

		private bool triggered;
		private GameObject panel;
		private GameStateManager gameStateManager;

		void Start() {
			panel = GameObject.FindGameObjectWithTag(TagConstants.UI.TOOLTIP_PANEL);	
			panel.SetActive(false);

		}

		void Update() {
			
		}

		void OnTriggerEnter(Collider collider) {
			if ( !triggered && collider.transform.tag == TagConstants.PENGUIN ) {
				// show tooltipPanel with corresponding text and freeze time for x seconds 
				ShowText();
				StartCoroutine(Freeze());
				triggered = true; //only for the first penguin
			}
		}

		private void ShowText() {
			panel.SetActive(true);
			switch(type) {
				case "jump":
					panel.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.jump);
				break;
				case "lane":
					panel.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.switchlane);
					break;
				default:
					throw new Exception("Incorrect tooltip type");
			}
		}

		private IEnumerator Freeze() {
			gameStateManager.SetGameFrozen(true);
			yield return new WaitForSeconds(time);
			panel.SetActive(false);
			gameStateManager.SetGameFrozen(false);
		}

		public void SetGameStateManager(GameStateManager manager) {
			gameStateManager = manager;
		}

		public override string GetTag() {
			return TagConstants.TOOLTIP;
		}

	}
}

