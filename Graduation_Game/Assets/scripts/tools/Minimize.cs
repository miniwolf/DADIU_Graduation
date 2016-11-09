using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;
using Asset.scripts.tools;
using Assets.scripts.character;
using Assets.scripts.components.registers;
using System.Collections;

namespace Assets.scripts.tools {
	public class Minimize : MonoBehaviour, Tool {
		private Penguin penguin;
		public AnimationCurve curve;
		public float timeForMinimize;

		protected void OnTriggerEnter(Collider collision) {
			if ( collision.tag == TagConstants.PENGUIN ) {
				penguin = collision.gameObject.GetComponent<Penguin>();
				penguin.SetCurve(Penguin.CurveType.Minimize, curve);

				var actionable = collision.gameObject.GetComponent<Actionable<ControllableActions>>();
				actionable.ExecuteAction(ControllableActions.StartMinimize);
				StartCoroutine(WaitForMinimize(actionable));
			}
		}

		private IEnumerator WaitForMinimize(Actionable<ControllableActions> actionable) {
			yield return new WaitForSeconds(timeForMinimize);
			actionable.ExecuteAction(ControllableActions.StopMinimize);
		}
		public ToolType GetToolType() {
			return ToolType.Minimize;
		}
	}
}

