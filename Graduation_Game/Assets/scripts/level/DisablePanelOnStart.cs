using System;
using UnityEngine;

namespace Assets.scripts.level {
	public class DisablePanelOnStart : MonoBehaviour {
		void Start() {
			gameObject.SetActive(false);
		}
	}
}

