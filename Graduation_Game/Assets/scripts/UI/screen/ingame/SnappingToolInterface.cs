using UnityEngine;
using System.Collections;

public interface SnappingToolInterface  {
	void SetCenter (GameObject obj);
	void Snap (Vector3 hitPos, Transform tool);
}
