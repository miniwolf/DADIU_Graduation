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
			yield return null;
		}
	}
}
