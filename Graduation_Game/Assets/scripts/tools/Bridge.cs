using UnityEngine;
using System.Collections;
using Asset.scripts.tools;
namespace Assets.scripts.tools {
	public class Bridge : MonoBehaviour, Tool {

		public ToolType GetToolType() {
			return ToolType.Bridge;
		}
	}
}
