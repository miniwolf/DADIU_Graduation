using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts.character;

using System.Collections;
using UnityEngine;

namespace Assets.scripts.tools {
	public class Speed : MonoBehaviour, Tool {
		private Penguin penguin;
		public AnimationCurve curve;
		public float timeForRun;

		protected void OnTriggerEnter(Collider collision) {
			if ( collision.tag == TagConstants.PENGUIN ) {
				penguin = collision.gameObject.GetComponent<Penguin>();
				penguin.SetCurve(curve);

				var actionable = collision.gameObject.GetComponent<Actionable<ControllableActions>>();
				actionable.ExecuteAction(ControllableActions.StartSpeed);
				StartCoroutine(WaitForSpeedup(actionable));
			}
		}

		public ToolType GetToolType() {
			return ToolType.Speed;
		}

		private IEnumerator WaitForSpeedup(Actionable<ControllableActions> actionable) {
			yield return new WaitForSeconds(timeForRun);
			actionable.ExecuteAction(ControllableActions.StopSpeed);
		}
	}
}

