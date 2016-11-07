using System;

namespace Assets.scripts.UI {
	public interface InputManager {
		void SubscribeForTouch(TouchInputListener l);
		void SubscribeForKeyboard(KeyboardInputListener l);
		void SubscribeForMouse(MouseInputListener l);

		void UnsubscribeForTouch(TouchInputListener l);
		void UnsubscribeForKeyboard(KeyboardInputListener l);
		void UnsubscribeForMouse(MouseInputListener l);
	}
}

