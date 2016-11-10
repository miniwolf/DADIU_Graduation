using Assets.scripts.character;
using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.tools {
	public class SwitchLane : MonoBehaviour, Tool {
		public void OnTriggerEnter(Collider collision) {
			if ( collision.tag != TagConstants.PENGUIN ) {
				return;
			}

			switch ( collision.gameObject.GetComponent<Penguin>().GetLane() ) {
				case Penguin.Lane.Right:
					ChangeLane(collision, ControllableActions.SwitchLeft, Penguin.Lane.Left);
					break;
				case Penguin.Lane.Left:
					ChangeLane(collision, ControllableActions.SwitchRight, Penguin.Lane.Right);
					break;
			}
		}

		private static void ChangeLane(Component collision, ControllableActions action, Penguin.Lane lane) {
			collision.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(action);
			collision.gameObject.GetComponent<Penguin>().SetLane(lane);
		}

		public ToolType GetToolType() {
			return ToolType.SwitchLane;
		}
	}
}
