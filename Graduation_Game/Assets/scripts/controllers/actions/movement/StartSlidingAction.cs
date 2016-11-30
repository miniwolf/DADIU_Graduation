using System.Collections;
using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement {
    public class StartSlidingAction : Action {
        private Directionable directionable;
		private Animator animator;
		private string animation;
		private CouroutineDelegateHandler delegator;

        public StartSlidingAction(CouroutineDelegateHandler delegator, Animator anim, string animation, Directionable directionable) {
            this.directionable = directionable;
            this.animator = anim;
            this. delegator = delegator;
			this.animation = animation;
        }

        public void Setup(GameObject gameObject) {

        }

        public void Execute() {
            animator.SetBool(animation, true);
            delegator.StartCoroutine(IncreasePenguinSpeed(directionable, animator));
        }

        IEnumerator IncreasePenguinSpeed(Directionable p, Animator anim) {
            float originalSpeed = p.GetWalkSpeed();

            p.SetSlide(true);
            while (p.IsSliding()) {
                if(p.GetSpeed() <= p.GetSlideMaxSpeedMult())
                    p.SetSpeed(p.GetSpeed() + p.GetSlideSpeedupIncrement());
                Debug.Log("Current speed: " + p.GetSpeed());
                yield return new WaitForFixedUpdate();
            }

            anim.SetBool(animation, false);
            p.SetSpeed(originalSpeed);
            yield return null;
        }
    }
}