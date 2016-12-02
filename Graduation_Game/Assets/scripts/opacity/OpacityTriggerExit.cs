using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts;

public class OpacityTriggerExit : MonoBehaviour {

	public OpacityTriggerEnter enter;
	public Material nonTransparent;

	void OnTriggerEnter(Collider col){
		if (col.transform.tag != TagConstants.PENGUIN) {
			return;
		}
		if (enter.toMakeTransparent == null) {
			Debug.LogError("No gameobjects added to be made transparent");
			return;
		}
		for (int i = 0; i < enter.toMakeTransparent.Length; i++) {
			foreach (Transform t in enter.toMakeTransparent[i].GetComponentInChildren<Transform>()) {
				t.gameObject.GetComponent<MeshRenderer>().material = nonTransparent;
			}
		}
	}
}
