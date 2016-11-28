using UnityEngine;
using Assets.scripts;
using Assets.scripts.character;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts.controllers.actions.animation;
using Assets.scripts.tools.slope;

public class SlopeTriggerExit : MonoBehaviour {
    void OnTriggerExit(Collider collision) {
        if (collision.tag.Equals(TagConstants.PENGUIN)) {
//			SlopeScript slope = GetComponentInParent<SlopeScript>();
//			slope.removePenguin(collision.gameObject);

            Penguin penguin = collision.gameObject.GetComponent<Penguin>();
            penguin.ExecuteAction(ControllableActions.StopSliding);
        }
    }
}