using UnityEngine;

namespace Assets.scripts.UI.input {
	public interface TouchInputListener : InputListener {
		void OnTouch(Touch[] allTouches);
	}
}

