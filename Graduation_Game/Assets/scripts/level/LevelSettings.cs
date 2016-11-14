using UnityEngine;

namespace Assets.scripts.level {
	public class LevelSettings : MonoBehaviour {
		public float laneWidth = 2;
		public float jumpHeight = 1;
		public GameObject sceneCenter;
	    public ToolSettings toolSettings;

		public float GetLaneWidth() {
			return laneWidth;
		}

		public float GetJumpHeight() {
			return jumpHeight;
		}

		public GameObject GetSceneCenter(){
			return sceneCenter;
		}

	    [System.Serializable]
	    public class ToolSettings
	    {
	        public int switchLaneCnt, freezeTimeCnt, jumpCnt, bridgeCnt, speedCnt, enlargeCnt, minimizeCnt;
	    }
	}
}
