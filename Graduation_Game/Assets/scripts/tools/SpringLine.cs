using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringLine : MonoBehaviour {

	LineRenderer line;
	public GameObject start, end;
	Vector3[] vec = new Vector3[2];

	// Use this for initialization
	void Start () {
		
		line = gameObject.GetComponent<LineRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		vec[0] = start.transform.position;
		vec[1] = end.transform.position;
		line.SetPositions(vec);
	}
}
