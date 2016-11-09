using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Editor
{
    [CustomEditor(typeof(SlopeGenerator))]
    public class MapGeneratorEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            var generator = (SlopeGenerator)target;

            if ( DrawDefaultInspector() ) {
                // this happens every time you edit the object (auto generated)
            }

            if ( GUILayout.Button("Generate slope") ) {
                generator.Generate();
            }
        }
    }
}
