using UnityEngine;

namespace Assets.scripts.tools {
	public class Bridge : MonoBehaviour, Tool {

		public ToolType GetToolType() {
			return ToolType.Bridge;
		}
	}
}
