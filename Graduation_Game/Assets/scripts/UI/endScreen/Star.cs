using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.tools {
	public class Star : ActionableGameEntityImpl<GameActions> {
		public AnimationCurve curve;
		public Vector3 startPosition;
		public Vector3 endPosition;

		public void FlyIn() {
			gameObject.GetComponent<Image>().enabled = true;
			gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/star");
			ExecuteAction(GameActions.TriggerStar);
		}

		public override string GetTag() {
			return gameObject.tag;
		}
	}
}