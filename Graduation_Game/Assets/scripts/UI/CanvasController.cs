using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.controllers;
using Assets.scripts.components;

namespace Assets.scripts.UI {
	public class CanvasController : ActionableGameEntityImpl<GameActions> {

		// Use this for initialization
		public int penguinsRequiredFor3Stars;
		public int penguinsRequiredFor2Stars;
		public int penguinsRequiredFor1Stars;
		private Text penguinCounter;
		private bool levelEnded;

		public void Start() {
			levelEnded = false;
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
		}
		void Update () {
			if (!levelEnded && int.Parse(penguinCounter.text) < 1) {
				levelEnded = true;
				ExecuteAction(GameActions.EndLevel);
			}
		}

		public override string GetTag() {
			return TagConstants.CANVAS;
		}

	}
}
