using UnityEngine;

namespace Assets.scripts.level {
	public class LevelSettings : MonoBehaviour {
		public float laneWidth = 2;
		public float jumpHeight = 1;
		public GameObject sceneCenter;

		public float GetLaneWidth() {
			return laneWidth;
		}

		public float GetJumpHeight() {
			return jumpHeight;
		}

		public GameObject GetSceneCenter(){
			return sceneCenter;
		}
	}
}
