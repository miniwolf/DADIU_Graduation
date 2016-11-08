using System;

namespace Assets.scripts.UI {
	public interface MouseInputListener : InputListener {
		void OnMouseRightDown();
		void OnMouseRightUp();
		void OnMouseRightPressed();

		void OnMouseLeftDown();
		void OnMouseLeftUp();
		void OnMouseLeftPressed();
	}
}

