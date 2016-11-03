﻿using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
	public class SnappingTool : EditorWindow {
		private Vector3 prevPosition;
		private bool doSnap;
		private float snapValue = 1;

		[MenuItem("Edit/Auto Snap %_l")]
		public static void Init() {
			var window = GetWindow(typeof(SnappingTool));
		}

		protected void OnGUI() {
			doSnap = EditorGUILayout.Toggle("Autosnap", doSnap);
			snapValue = EditorGUILayout.FloatField("Snap Value", snapValue);
		}

		protected void Update() {
			if ( Selection.transforms.Length <= 0 || EditorApplication.isPlaying || !doSnap
				|| Selection.transforms[0].position == prevPosition ) {
				return;
			}
			Snap();
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