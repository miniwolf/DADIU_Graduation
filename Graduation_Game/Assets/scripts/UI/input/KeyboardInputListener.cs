using System;
using UnityEngine;

namespace Assets.scripts.UI {
	public interface KeyboardInputListener : InputListener {
//		void OnKeyPressed(KeyCode code);
//		void OnArrowDown();
//		void OnArrowUp();
		void OnArrowRight();
		void OnArrowLeft();
	}
}

