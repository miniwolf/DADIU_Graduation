using UnityEngine;

namespace Assets.scripts.camera {
	/// <summary>
	///	Displays a red wired sphere for the CameraStep prefab.
	/// </summary>
	public class CameraStepGizmo : MonoBehaviour {
		public float radius;

		private void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}
