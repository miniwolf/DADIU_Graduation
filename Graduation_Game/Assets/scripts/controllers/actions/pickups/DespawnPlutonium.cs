using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.scripts.components;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.pickups {
	public class DespawnPlutonium : Action {
		private GameObject gameObject;
		private Text plutoniumCounter;
		private Image counter; 
		private RectTransform canvas;
		private readonly CouroutineDelegateHandler delegator;

		public DespawnPlutonium(CouroutineDelegateHandler d) {
			delegator = d;
		}

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
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
			RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas,plutoniumCounter.rectTransform.position,c,out worldPos);
			counterCanvasPos = worldPos;
			Vector3 currencyCanvasPos = currencyGameObject.transform.position;

			counterCanvasPos.z = currencyCanvasPos.z; // we don't want to change the z-axis of item picked up


		    ParticleSystem p = currencyGameObject.AddComponent<ParticleSystem>();
		    p.startSpeed = 10f;

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

			currencyGameObject.SetActive(false);
			plutoniumCounter.text = (int.Parse(plutoniumCounter.text) + 1).ToString();
			yield return null;
		}
	}
}