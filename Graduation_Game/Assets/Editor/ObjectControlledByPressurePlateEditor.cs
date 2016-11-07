using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(Assets.scripts.tools.ObjectControlledByPressurePlate))]
public class ObjectControlledByPressurePlateEditor : Editor {

	GameObject gameObject;

	public override void OnInspectorGUI(){
		gameObject = (GameObject)EditorGUILayout.ObjectField (gameObject, typeof(GameObject), true);
	}
}
