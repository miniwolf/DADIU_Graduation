using UnityEngine;

namespace Assets.scripts.camera {
	/// <summary>
	/// Always displays the camera frustum in the Scene
	/// in color red, even when the camera is not selected.
	/// </summary>
	public class MainCameraGizmo : MonoBehaviour {
		public virtual void OnDrawGizmos() {
			var temp = Gizmos.matrix;
			Gizmos.color = Color.red;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

			// For perspective FOV
			var cam = GetComponent<Camera>();
			Gizmos.DrawFrustum(Vector3.zero, cam.fieldOfView, cam.farClipPlane, cam.nearClipPlane, cam.aspect);
			Gizmos.matrix = temp;
		}
	}
}
