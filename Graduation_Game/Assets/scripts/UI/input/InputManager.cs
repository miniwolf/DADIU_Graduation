using System;
using Assets.scripts.UI.input;

namespace Assets.scripts.UI {
	public interface InputManager {
	    /// <summary>
	    /// Don't forget to call Unsubscribe
	    /// </summary>
	    /// <param name="l">L.</param>
	    void SubscribeForTouch(TouchInputListener l);
	    /// <summary>
	    /// Don't forget to call Unsubscribe
	    /// </summary>
	    /// <param name="l">L.</param>
	    void SubscribeForKeyboard(KeyboardInputListener l);
	    /// <summary>
	    /// Don't forget to call Unsubscribe
	    /// </summary>
	    /// <param name="l">L.</param>
	    void SubscribeForMouse(MouseInputListener l);

		void UnsubscribeForTouch(TouchInputListener l);
		void UnsubscribeForKeyboard(KeyboardInputListener l);
		void UnsubscribeForMouse(MouseInputListener l);
		/// <summary>
		/// Blocks the camera movement. Call this when you start dragging something into scene
		/// </summary>
	    void BlockCameraMovement();
		/// <summary>
		/// Call when dragging is finished
		/// </summary>
	    void UnblockCameraMovement();
		/// <summary>
		/// Determines whether camera is blocked. This code shoud be used in camera to block its movement
		/// </summary>
		/// <returns><c>true</c> if this instance is camera blocked; otherwise, <c>false</c>.</returns>
	    bool IsCameraBlocked();
	}
}

