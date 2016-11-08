using UnityEngine;

public interface WeightBasedInterface {
	GameObject[] GetChildrenToManipulate();
	float GetInitialHeight();
	float GetWhenSunk();
	float GetMovementFactor();
}
