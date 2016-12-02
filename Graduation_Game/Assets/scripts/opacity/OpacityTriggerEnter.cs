using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts;

public class OpacityTriggerEnter : MonoBehaviour {

	public GameObject[] toMakeTransparent;
	public Material transparent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider col){
		if (col.transform.tag != TagConstants.PENGUIN) {
			return;
		}
		if (toMakeTransparent == null) {
			Debug.LogError("No gameobjects added to be made transparent");
			return;
		}
		for (int i = 0; i < toMakeTransparent.Length; i++) {
			foreach (Transform t in toMakeTransparent[i].GetComponentInChildren<Transform>()) {
				t.gameObject.GetComponent<MeshRenderer>().material = transparent;
			}
		}
	}
}
