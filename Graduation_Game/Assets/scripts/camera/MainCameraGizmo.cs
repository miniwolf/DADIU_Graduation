using UnityEngine;
using System.Collections;

namespace Assets.scripts.camera
{
	/// <summary>
	/// Always displays the camera frustum in the Scene
	/// in color red, even when the camera is not selected.
	/// </summary>
	public class MainCameraGizmo : MonoBehaviour
	{
		public virtual void OnDrawGizmos()
		{
			Matrix4x4 temp = Gizmos.matrix;
			Gizmos.color = Color.red;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

			// For perspective FOV
			Gizmos.DrawFrustum(Vector3.zero, GetComponent<Camera>().fieldOfView, GetComponent<Camera>().farClipPlane,GetComponent<Camera>().nearClipPlane, GetComponent<Camera>().aspect);
			Gizmos.matrix = temp;
		}
	}
}
