using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts.controllers.actions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts;
using Assets.scripts.UI;

namespace AssemblyCSharp {
	public class RetryAction : Action {
		private readonly Actionable<GameActions> actionable;
		private readonly CouroutineDelegateHandler handler;
		private Button retryCircle, retryButton;
		private Image retryCircleImage;
		private Text retryPrize;
		private string toFind;
		private CanvasController canvas;

		public RetryAction (CouroutineDelegateHandler handler, Actionable<GameActions> actionable, string toFind) {
			this.handler = handler;
			this.actionable = actionable;
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
			canvas.SetActiveClickBlocker(false);
		}

		public void Execute () {
			handler.StartCoroutine(RetryCircle());
		}

		IEnumerator RetryCircle(){
			while(retryCircleImage.fillAmount > 0){
				retryCircleImage.fillAmount -= 1.0f/canvas.timeForRetry * Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			DisableRetry();
		}

		private void DisableRetry(){
			retryButton.gameObject.SetActive(false);
		}

	}
}

