using UnityEngine;
using System.Collections;

public class CameraStep : MonoBehaviour {
	public float radius;

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
