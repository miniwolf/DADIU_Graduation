using UnityEngine;
using System.Collections;
using Assets.scripts;
using Assets.scripts.character;
using Assets.scripts.controllers;
using Assets.scripts.components;

namespace Asset.scripts.tools {
	public class SwitchLane : MonoBehaviour {
		protected void OnTriggerEnter(Collider collision) {
			if ( collision.tag != TagConstants.PLAYER ) {
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

		private void ChangeLane(Collider collision, ControllableActions action, Penguin.Lane lane) {
			collision.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(action);
			collision.gameObject.GetComponent<Penguin>().SetLane(lane);
		}
	}
}
