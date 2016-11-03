using UnityEngine;
using System.Collections;
using Assets.scripts;
using Assets.scripts.character;
using Assets.scripts.controllers;
using Assets.scripts.components;

namespace Asset.scripts.tools {
	public class Jump : MonoBehaviour {
		
		protected void OnTriggerEnter(Collider collision) {
			if ( collision.tag == TagConstants.PLAYER ) {
				collision.gameObject.GetComponent<Actionable<ControllableActions>>().ExecuteAction(ControllableActions.Jump);
			}
		}
	}
}

