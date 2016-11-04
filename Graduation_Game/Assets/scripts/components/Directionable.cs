using UnityEngine;

namespace Assets.scripts.components {
	public interface Directionable {
		Vector3 GetDirection();
		void SetDirection(Vector3 direction);
		float GetJumpSpeed();
		float GetWalkSpeed();
		void SetSpeed(float speed);
		float GetSpeed();
		void SetJump(bool jump);
		bool GetJump();
    }
}
