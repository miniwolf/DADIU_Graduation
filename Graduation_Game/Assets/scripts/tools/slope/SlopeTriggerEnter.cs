using System.Collections;
using UnityEngine;
using Assets.scripts;
using Assets.scripts.character;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts.tools.slope;

public class SlopeTriggerEnter : MonoBehaviour {
    void OnTriggerEnter(Collider collision) {
        if (TagConstants.PENGUIN.Equals(collision.tag)) {



//			SlopeScript slope = GetComponentInParent<SlopeScript>();
//			slope.addPenguin(collision.gameObject);

            Penguin penguin = collision.gameObject.GetComponent<Penguin>();
            penguin.ExecuteAction(ControllableActions.StartSliding);
        }
    }

}