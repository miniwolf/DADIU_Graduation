using UnityEngine;
using Assets.scripts;
using Assets.scripts.tools.slope;

public class SlopeTriggerExit : MonoBehaviour {
	void OnTriggerExit(Collider collision) {
		if(collision.tag.Equals(TagConstants.PENGUIN)) {
			SlopeScript slope = GetComponentInParent<SlopeScript>();
			slope.removePenguin(collision.gameObject);
		}
	}
}