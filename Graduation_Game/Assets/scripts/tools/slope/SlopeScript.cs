using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.tools;
using UnityEngine;

namespace Assets.scripts.tools.slope {
	public class SlopeScript : MonoBehaviour {

		public AnimationCurve speedCurve;
		public AnimationCurve penguinRotationCurve;

		private Dictionary<GameObject, float> managedPenguins = new Dictionary<GameObject, float>();
		private float xEnd;
		private float slopeLength;

		void Start() {
			slopeLength = GetComponent<MeshRenderer>().bounds.size.x;
		}

		public void addPenguin(GameObject penguin) {
//			penguin.GetComponentInChildren<Rigidbody>().isKinematic = false;
//			managedPenguins.Add(penguin, penguin.transform.position.x);
//			penguin.GetComponent<ActionableGameEntityImpl<ControllableActions>>().ExecuteAction(ControllableActions.StartSliding);
		}

		public void removePenguin(GameObject penguin) {
//			managedPenguins.Remove(penguin);

//			penguin.GetComponent<ActionableGameEntityImpl<ControllableActions>>().ExecuteAction(ControllableActions.StopSliding);
		}

		void Update() {
			foreach(var penguin in managedPenguins) {
				var penguinObj = penguin.Key;
				var enterX = penguin.Value;

				float currentProgress = penguinObj.transform.position.x - enterX;
				float x = currentProgress / slopeLength;
				Vector3 position = penguinObj.transform.position;
				var f = speedCurve.Evaluate(x);
				position.x = position.x + f;
				penguinObj.transform.position = position;
				var penguinRotation = penguinRotationCurve.Evaluate(x);
				var q = Quaternion.identity;
				q.z = penguinRotation;
				penguinObj.transform.rotation = q;
			}
		}
	}
}