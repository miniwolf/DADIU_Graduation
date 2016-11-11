using UnityEngine;
using Assets.scripts;
using Assets.scripts.tools.slope;

public class SlopeTriggerEnter : MonoBehaviour {
	void OnTriggerEnter(Collider collision) {
		if(collision.tag.Equals(TagConstants.PENGUIN)) {
			SlopeScript slope = GetComponentInParent<SlopeScript>();
			slope.addPenguin(collision.gameObject);
		}
	}
}