using System;
using System.Collections;
using System.Reflection;
using Assets.scripts;
using UnityEngine;
using NUnit.Framework;

namespace Assets.Editor.buildserver {
	public class MissingTagsTest {
		[Test]
		public void EditorTest() {
			var gameObject = new GameObject();
			gameObject.name = "TagTest";

			CheckMissingTags(gameObject, typeof(TagConstants));
			CheckMissingTags(gameObject, typeof(TagConstants.Tool));
			CheckMissingTags(gameObject, typeof(TagConstants.UI));
		}

		private void CheckMissingTags(GameObject go, Type type) {
			var fields = GetConstants(type);
			Debug.Log("Fields in " + type.Name + " : " + fields.Length);

			foreach(var prop in fields) {
				string tag = (string)prop.GetValue(null);

				try {
					go.tag = tag;
				} catch(Exception e) {
					Debug.LogWarning(e.Message);
				}
			}
		}

		private FieldInfo[] GetConstants(Type type) {
			ArrayList constants = new ArrayList();
			FieldInfo[] fieldInfos =
				type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
			foreach(FieldInfo fi in fieldInfos)
				if(fi.IsLiteral && !fi.IsInitOnly)
					constants.Add(fi);
			return (FieldInfo[])constants.ToArray(typeof(FieldInfo));
		}
	}
}