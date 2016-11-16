using System;
using UnityEngine;

namespace Assets.scripts.UI.inventory {
	public class PreferenceItem<T> : Item<T> where T: IComparable {
		private readonly string name;
		private T value;

		public PreferenceItem(string name) {
			this.name = name;
		}

		public void SetValue(T value) {
			this.value = value;
			if ( value is int ) {
				var i = (int)(object) value;
				PlayerPrefs.SetInt(name, i);
			} else if ( value is float ) {
				var f = (float) (object) value;
				PlayerPrefs.SetFloat(name, f);
			} else if ( value is string ) {
				var s = (string) (object) value;
				PlayerPrefs.SetString(name, s);
			}
		}

		public T GetValue() {
			if ( value is int ) {
				return (T)(object)PlayerPrefs.GetInt(name);
			}
			if ( value is float ) {
				return (T)(object)PlayerPrefs.GetFloat(name);
			}
			if ( value is string ) {
				return (T)(object)PlayerPrefs.GetString(name);
			}
			return default(T);
		}
	}
}
