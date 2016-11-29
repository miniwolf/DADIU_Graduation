using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace Assets.scripts.tools {
	public class Star : ActionableGameEntityImpl<GameActions> {
		private Vector3 currScale;
		private Vector3 targetScale = new Vector3(0.2f, 0.2f, 0.2f);

		void Start(){
			gameObject.GetComponent<Image>().enabled = false;
		}
		public void FlyIn() {
			gameObject.GetComponent<Image>().enabled = true;
			GetComponent<RectTransform>().localScale = Vector3.zero;
			StartCoroutine(StarSpawnAnim());
			//ExecuteAction(GameActions.TriggerStar);
		}

		private IEnumerator StarSpawnAnim(){
			float startTime = Time.time, journeyLength = Vector3.Distance(Vector3.zero,targetScale), speedFactor = 1f;
			float distCovered = (Time.time - startTime)*speedFactor;
			float fracJourney = distCovered / journeyLength;
			//print(path[0] + " " + path[1]);
			while(fracJourney<1f){
				distCovered = (Time.time - startTime) * speedFactor;
				fracJourney = distCovered / journeyLength;
				if (fracJourney < 0.70f) {
					GetComponent<RectTransform>().localScale = Vector3.Lerp(Vector3.zero, new Vector3(targetScale.x + 0.2f, targetScale.y + 0.2f, targetScale.z + 0.2f), fracJourney);
					currScale = transform.localScale;
				} else {
					GetComponent<RectTransform>().localScale = Vector3.Lerp(currScale, targetScale, fracJourney);
				}
				yield return new WaitForEndOfFrame();
			}
		}


		public override string GetTag() {
			return gameObject.tag;
		}
	}
}