using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.tools {
	public class FallingBridge : ActionableGameEntityImpl<PressurePlateActions> {
		public override string GetTag() {
			return TagConstants.FALLING_BRIDGE;
		}
	}
}