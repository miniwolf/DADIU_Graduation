using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
	public class SnapToSurface : ScriptableObject {
		[MenuItem ("GameObject/Place Selection On Surface")]
		public static void CreateWizard () {
			var transforms = Selection.GetTransforms(SelectionMode.Deep |
							SelectionMode.ExcludePrefab | SelectionMode.OnlyUserModifiable);

			if (transforms.Length <= 0 || !EditorUtility.DisplayDialog("Place Selection On Surface?",
				    "Are you sure you want to place " + transforms.Length +
				    ((transforms.Length > 1) ? " objects" : " object") +
				    " on the surface in the -Y direction?", "Place", "Do Not Place")) return;
			foreach ( var transform in transforms ) {
				RaycastHit hit;
				if ( !Physics.Raycast(transform.position, Vector3.down, out hit) ) {
					continue;
				}
				transform.position = hit.point;
				var randomized = Random.onUnitSphere;
				randomized = new Vector3(randomized.x, 0F, randomized.z);
				transform.rotation = Quaternion.LookRotation(randomized, hit.normal);
			}
		}
	}
}
