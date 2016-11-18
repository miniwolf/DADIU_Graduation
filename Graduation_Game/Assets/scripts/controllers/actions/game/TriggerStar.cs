using Assets.scripts.components;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.game {
	public class TriggerStar : Action {
		private GameObject gameObject;
		private Vector3 initialPos;
		private Vector3 targetPos;
		private readonly CouroutineDelegateHandler delegator;
		private Camera c = Camera.main;

		public TriggerStar(CouroutineDelegateHandler d) {
			delegator = d;
		}

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			initialPos = gameObject.transform.localPosition;
			targetPos = new Vector3(initialPos.x, 13.5f, initialPos.z);
		}

		public void Execute() {
			delegator.StartCoroutine(FeedbackCoroutine());
		}

		private IEnumerator FeedbackCoroutine() {
			float fraction = 0;
			float speed = 2f;
			float distance = float.MaxValue;
			
			while (distance > 1) {
				if (fraction < 1) {
					fraction += Time.deltaTime * speed;
					gameObject.transform.localPosition = Vector3.Lerp(initialPos, targetPos, fraction);
				}

				distance = Vector3.Distance(gameObject.transform.position, targetPos);
				float scale = 1 + (gameObject.transform.localPosition.y - 13.5f) / 100;
				gameObject.transform.localScale = new Vector3(scale, scale, scale);
				yield return new WaitForEndOfFrame();
			}
			yield return null;
		}
	}
}
