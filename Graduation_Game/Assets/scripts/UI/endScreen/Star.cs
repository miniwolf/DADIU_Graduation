using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.tools {
	public class Star : ActionableGameEntityImpl<GameActions> {
		public AnimationCurve curve;
		public Vector3 startPosition;
		public Vector3 endPosition;

		public void FlyIn() {
			ExecuteAction(GameActions.TriggerStar);
		}

		public override string GetTag() {
			return gameObject.tag;
		}
	}
}