using UnityEngine;
using System.Collections;

/// <summary>
///	Displays a red wired sphere for the CameraStep prefab.
/// <param name="radius">Radius of the wired sphere</param>
/// </summary>
namespace Assets.scripts.camera {
	public class CameraStepGizmo : MonoBehaviour {
		public float radius;

		void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}
