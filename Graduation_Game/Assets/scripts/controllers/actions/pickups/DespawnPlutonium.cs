using Assets.scripts.components;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Assets.scripts.controllers.actions.pickups
{
	public class DespawnPlutonium : Action {
		private GameObject gameObject;
		private Text plutoniumCounter;
		private int pointsToAdd;

		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			pointsToAdd = 0;
			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
		}

		public void Execute() {
			gameObject.transform.parent.gameObject.SetActive (false);
			pointsToAdd += 10;
			plutoniumCounter.text = (int.Parse(plutoniumCounter.text) + 1).ToString();
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
