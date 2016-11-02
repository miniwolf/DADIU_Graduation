using UnityEngine;
using System.Collections;
using Assets.scripts;
using Assets.scripts.controllers;
using Assets.scripts.components;

namespace Asset.scripts.tools {
public class SwitchLane : MonoBehaviour {
	void OnCollisionEnter (Collision collision) {
		if ( collision.collider.tag == TagConstants.PLAYER) {
				var lane = collision.gameObject.GetComponent<Penguin>().GetLane();
				if ( lane == Penguin.Lane.Right ) {
					collision.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.SwitchLeft);
					collision.gameObject.GetComponent<Penguin>().SetLane(Penguin.Lane.Left);
				}
				else if ( lane == Penguin.Lane.Left ) {
					collision.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.SwitchRight);
					collision.gameObject.GetComponent<Penguin>().SetLane(Penguin.Lane.Right);
				}
		}
	}
}
}
