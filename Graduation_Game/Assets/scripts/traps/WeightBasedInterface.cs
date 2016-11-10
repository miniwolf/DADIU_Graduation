using UnityEngine;
using System.Collections.Generic;

public interface WeightBasedInterface {
	List<GameObject> GetChildrenToManipulate();
	float GetInitialHeight();
	float GetWhenSunk();
	float GetMovementFactor();
	float GetHeavinessFactor();
}
