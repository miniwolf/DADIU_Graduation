using Assets.scripts.components;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.pickups {
	public class DespawnPlutonium : Action {
		private GameObject gameObject;
		private Text plutoniumCounter;
		private int pointsToAdd;
		private readonly CouroutineDelegateHandler delegator;

		public DespawnPlutonium(CouroutineDelegateHandler d) {
			delegator = d;
		}

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			pointsToAdd = 0;
			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
		}

		public void Execute() {
			gameObject.transform.parent.gameObject.SetActive (false);
			pointsToAdd += 10;
			delegator.StartCoroutine(FeedbackCoroutine());
		}

		private IEnumerator FeedbackCoroutine() {
			Camera c = Camera.main;
			GameObject currencyGameObject = gameObject.transform.parent.gameObject;
			Vector3 counterCanvasPos = c.ScreenToWorldPoint(plutoniumCounter.transform.position);
			Vector3 currencyCanvasPos = currencyGameObject.transform.position;

//		    Vector3 pos = c.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
//            Debug.Log(pos + ", Screen.width: " + Screen.width);

			counterCanvasPos.z = currencyCanvasPos.z; // we don't want to change the z-axis of item picked up
		    counterCanvasPos.x += 10; // Mathf.Abs(pos.x); // this is a mind fuck

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

		private IEnumerator UpdateScore() {
			while (pointsToAdd>0) {
				yield return new WaitForSeconds(0.6f);
				ApplyChange(1);
			}
		}

		private void ApplyChange(int portion) {
			plutoniumCounter.text = (int.Parse(plutoniumCounter.text) + portion).ToString();
			pointsToAdd -= portion;
		}
	}
}