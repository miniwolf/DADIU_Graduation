using System.Collections;
using UnityEngine;
using Assets.scripts.controllers.actions;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts.UI;
using UnityEngine.UI;
using Assets.scripts;

namespace AssemblyCSharp {
	public class DisableRetryAction : Action {
		private Button retryCircle, retryButton;
		private Image retryCircleImage;
		private Text retryPrize;
		private string toFind;
		private CanvasController canvas;

		public DisableRetryAction (string toFind) {
			this.toFind = toFind;
		}
			
		public void Setup (GameObject gameObject) {
			canvas = gameObject.GetComponent<CanvasController>();
			Transform[] trans = GameObject.FindGameObjectWithTag(toFind).GetComponentsInChildren<Transform>();
			for (int i = 0; i < trans.Length; i++) {
				switch (trans[i].tag) {
					case TagConstants.UI.RETRY_CIRCLE:
						retryCircle = trans[i].gameObject.GetComponent<Button>();
						retryCircleImage = trans[i].gameObject.GetComponent<Image>();
						break;
					case TagConstants.UI.RETRY_BUTTON:
						retryButton = trans[i].gameObject.GetComponent<Button>();
						break;
					case TagConstants.UI.RETRY_PRIZE:
						retryPrize = trans[i].gameObject.GetComponent<Text>();
						break;
					default:
						break;
				}
			}
		}

		public void Execute () {
			DisableRetry();
		}
		private void DisableRetry(){
			retryButton.gameObject.SetActive(false);
		}

	}
}

