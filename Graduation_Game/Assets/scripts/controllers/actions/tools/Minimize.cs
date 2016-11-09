﻿using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;
using Assets.scripts.character;

namespace Assets.scripts.controllers.actions.tools {
	public class Minimize : Action {
		private Directionable direction;

		public Minimize(Directionable direction) {
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			return;
		}

		public void Execute() {
			Vector3 initialScale = direction.GetInitialScale();
			float initialTime = direction.GetInitialTime(Penguin.CurveType.Minimize);
			float actualTime = Time.timeSinceLevelLoad;
			AnimationCurve curve = direction.GetCurve(Penguin.CurveType.Minimize);
			float scalingFactor = curve.Evaluate(actualTime - initialTime);
			Vector3 newScale = initialScale / scalingFactor;
			direction.SetScale(newScale);
		}
	}
}