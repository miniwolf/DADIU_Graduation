using System.Collections;
using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement
{
    public class StartSlidingAction : Action
    {
        private Directionable directionable;
        private Animator animator;
        private CouroutineDelegateHandler delegator;

        public StartSlidingAction(CouroutineDelegateHandler delegator, Animator anim, Directionable directionable) {
            this.directionable = directionable;
            this.animator = anim;
           this. delegator = delegator;
        }

        public void Setup(GameObject gameObject) {

        }

        public void Execute() {
            animator.SetBool(AnimationConstants.SLIDE, true);
            delegator.StartCoroutine(IncreasePenguinSpeed(directionable, animator));
        }

        IEnumerator IncreasePenguinSpeed(Directionable p, Animator anim) {
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
}