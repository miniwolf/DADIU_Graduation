using Assets.scripts.components;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.pickups {
	public class DespawnPlutonium : Action {
		private GameObject gameObject;
		private Text currencyCounter;
		private Text totalCounter;
		private int pointsToAdd;
		private Text plutoniumCounter;
		private Image counter; 
		private RectTransform canvas;
		private readonly CouroutineDelegateHandler delegator;

		public DespawnPlutonium(CouroutineDelegateHandler d, Text counter, Text total = null) {
			delegator = d;
			currencyCounter = counter; // GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
			totalCounter = total;
		}

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			pointsToAdd = 0;
			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
			counter = plutoniumCounter.transform.parent.GetComponent<Image>();
			canvas = GameObject.Find(TagConstants.CANVAS).GetComponent<RectTransform>();
		}

		public void Execute() {
			delegator.StartCoroutine(FeedbackCoroutine());
		}

		private IEnumerator FeedbackCoroutine() {
			Camera c = Camera.main;

			//plutoniumCounter.rectTransform
			Vector3 worldPos;
			GameObject currencyGameObject = gameObject.transform.parent.gameObject;
			Vector3 counterCanvasPos;
			if (totalCounter == null) {
				counterCanvasPos = c.ScreenToWorldPoint(currencyCounter.transform.position);
			}
			else {
				counterCanvasPos = c.ScreenToWorldPoint(totalCounter.transform.position);
			}
			RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas,plutoniumCounter.rectTransform.position,c,out worldPos);
			counterCanvasPos = worldPos;
			Vector3 currencyCanvasPos = currencyGameObject.transform.position;

			counterCanvasPos.z = currencyCanvasPos.z; // we don't want to change the z-axis of item picked up


			if (totalCounter == null) {
				ParticleSystem p = currencyGameObject.AddComponent<ParticleSystem>();
				p.startSpeed = 10f;
			}

			float fraction = 0;
			float speed = 1f;
			float distance = float.MaxValue;

			while(distance > 1) {
				if(fraction < 1) {
					fraction += Time.deltaTime * speed;
					currencyGameObject.transform.position = Vector3.Lerp(currencyCanvasPos, counterCanvasPos, fraction);
				}

				distance = Vector3.Distance(currencyGameObject.transform.position, counterCanvasPos);
				yield return new WaitForEndOfFrame();
			}

			if (totalCounter == null) {
				gameObject.transform.parent.parent = currencyCounter.transform;
			}
			else {
				gameObject.transform.parent.parent = totalCounter.transform;
			}
			
			currencyGameObject.SetActive(false);
			//plutoniumCounter.text = (int.Parse(plutoniumCounter.text) + 1).ToString();
			pointsToAdd += 10;
			while (pointsToAdd > 0) {
				ApplyChange(1);
				yield return new WaitForSeconds(0.02f);
			}
			yield return null;
		}

		private void ApplyChange(int portion) {
			if (totalCounter == null) {
				currencyCounter.text = (int.Parse(currencyCounter.text) + portion).ToString();
			}
			else {
				currencyCounter.text = (int.Parse(currencyCounter.text) - portion).ToString();
				totalCounter.text = (int.Parse(totalCounter.text) + portion).ToString();
			}
			pointsToAdd -= portion;
		}
	}
}