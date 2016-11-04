﻿using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
	public class SnappingTool : EditorWindow {
		private Vector3 prevPosition;
		private bool doSnap;
		private bool doBlockSnap;
		private float snapValue = 1;

		[MenuItem("Edit/Auto Snap %_l")]
		public static void Init() {
			var window = GetWindow(typeof(SnappingTool));
		}

		protected void OnGUI() {
			doSnap = EditorGUILayout.Toggle("Autosnap", doSnap);
			doBlockSnap = EditorGUILayout.Toggle("PlacementSnap", doBlockSnap);
			snapValue = EditorGUILayout.FloatField("Snap Value", snapValue);
		}

		protected void Update() {
			if ( Selection.transforms.Length <= 0 || EditorApplication.isPlaying
				|| Selection.transforms[0].position == prevPosition ) {
				return;
			}
			PlacementSnap();
			if ( doBlockSnap ) {

			}
			if ( doSnap ) {
				Snap();
			}
		}

		private void PlacementSnap() {
			var transforms = Selection.transforms;
			foreach ( var myTransform in transforms ) {
				RaycastHit hit;
				if ( !Physics.Raycast(myTransform.position, -Vector3.up, out hit) ) {
					continue;
				}

				var targetPosition = hit.point;
				if ( myTransform.gameObject.GetComponent<MeshFilter>() != null ) {
					var bounds = myTransform.gameObject.GetComponent<MeshFilter>().sharedMesh.bounds;
					targetPosition.y += bounds.extents.y;
				}
				myTransform.position = targetPosition;
				var targetRotation = new Vector3 (hit.normal.x, myTransform.eulerAngles.y, hit.normal.z);
				myTransform.eulerAngles = targetRotation;
			}
		}

		private void Snap() {
			foreach ( var t1 in Selection.transforms ) {
				var t = t1.transform.position;
				t.x = Round(t.x);
				t.y = Round(t.y);
				t.z = Round(t.z);
				t1.transform.position = t;
			}
			prevPosition = Selection.transforms[0].position;
		}

		private float Round(float input) {
			return snapValue * Mathf.Round(input / snapValue);
		}
	}
}