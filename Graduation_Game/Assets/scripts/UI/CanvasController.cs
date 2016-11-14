﻿using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.controllers;
using Assets.scripts.components;
using System.Collections;

namespace Assets.scripts.UI {
	public class CanvasController : ActionableGameEntityImpl<GameActions> {

		// Use this for initialization
		public int penguinsRequiredFor3Stars;
		public int penguinsRequiredFor2Stars;
		public int penguinsRequiredFor1Stars;
		private Text penguinCounter;
		private bool levelEnded;
		private GameObject endScene;

		public void Start() {
			levelEnded = false;
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			foreach (Transform g in gameObject.GetComponentsInChildren<Transform>(true))
				if (g.tag == TagConstants.ENDSCENE)
					endScene = g.gameObject;
		}
		void Update () {
			if (int.Parse(penguinCounter.text) < 1) {
				Wait(0.8f);
				ExecuteAction(GameActions.EndLevel);
			}
		}

		public IEnumerator Wait(float delayInSecs) {
			yield return new WaitForSeconds(delayInSecs);
		}

		public override string GetTag() {
			return TagConstants.CANVAS;
		}

	}
}
