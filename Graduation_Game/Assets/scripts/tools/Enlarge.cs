using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;
using Asset.scripts.tools;
using Assets.scripts.character;
using Assets.scripts.components.registers;
using System.Collections;

namespace Assets.scripts.tools {
	public class Enlarge : MonoBehaviour, Tool {
		private Penguin penguin;
		public AnimationCurve curve;
		public float timeForEnlarge;

		protected void OnTriggerEnter(Collider collision) {
			if ( collision.tag == TagConstants.PENGUIN ) {
				penguin = collision.gameObject.GetComponent<Penguin>();
				penguin.SetCurve(Penguin.CurveType.Enlarge, curve);

				var actionable = collision.gameObject.GetComponent<Actionable<ControllableActions>>();
				actionable.ExecuteAction(ControllableActions.StartEnlarge);
				StartCoroutine(WaitForSpeedup(actionable));
			}
		}

		private IEnumerator WaitForSpeedup(Actionable<ControllableActions> actionable) {
			yield return new WaitForSeconds(timeForEnlarge);
			actionable.ExecuteAction(ControllableActions.StopEnlarge);
		}
		public ToolType GetToolType() {
			return ToolType.Enlarge;
		}
	}
}

