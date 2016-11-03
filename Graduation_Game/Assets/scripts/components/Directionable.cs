using UnityEngine;

namespace Assets.scripts.components {
    public interface Directionable {
		Vector3 GetDirection();
		void SetDirection(Vector3 direction);
    }
}