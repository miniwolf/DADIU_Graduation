using UnityEngine;
using System.Collections;
using Assets.scripts;
using Assets.scripts.tools.slope;
using ADInterstitialAd = UnityEngine.iOS.ADInterstitialAd;

public class SlopeTriggerEnter : MonoBehaviour {
	void OnTriggerEnter(Collider collision) {
		if(collision.tag.Equals(TagConstants.PENGUIN)) {
			SlopeScript slope = GetComponentInParent<SlopeScript>();
			slope.addPenguin(collision.gameObject);
		}
	}
}