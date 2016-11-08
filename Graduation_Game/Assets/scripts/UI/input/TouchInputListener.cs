using System;
using UnityEngine;

namespace Assets.scripts.UI {
	public interface TouchInputListener : InputListener {
		void OnTouch(Touch firstTouch, Touch[] allTouches);
	}
}

