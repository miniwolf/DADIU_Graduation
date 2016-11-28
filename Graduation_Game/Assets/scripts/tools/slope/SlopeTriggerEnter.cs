using System.Collections;
using UnityEngine;
using Assets.scripts;
using Assets.scripts.character;
using Assets.scripts.components;
using Assets.scripts.tools.slope;

public class SlopeTriggerEnter : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (TagConstants.PENGUIN.Equals(collision.tag))
        {
//			SlopeScript slope = GetComponentInParent<SlopeScript>();
//			slope.addPenguin(collision.gameObject);

            Directionable penguin = collision.gameObject.GetComponent<Directionable>();
            Animator anim = collision.gameObject.GetComponentInChildren<Animator>();
            anim.SetBool(AnimationConstants.SLIDE, true);
            StartCoroutine(IncreasePenguinSpeed(penguin, anim));
        }
    }

    IEnumerator IncreasePenguinSpeed(Directionable p, Animator anim)
    {
        float originalSpeed = p.GetWalkSpeed();

        p.SetSlide(true);
        while (p.IsSliding())
        {
            p.SetSpeed(p.GetSpeed() + 0.1f);
            yield return new WaitForFixedUpdate();
        }

        anim.SetBool(AnimationConstants.SLIDE, false);
        p.SetSpeed(originalSpeed);
        yield return null;
    }
}