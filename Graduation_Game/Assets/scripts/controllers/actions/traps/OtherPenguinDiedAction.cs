using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.scripts.controllers.actions.traps
{
    public class OtherPenguinDiedAction : Action
    {
        private GameObject penguin;
        private Animator anim;

        public OtherPenguinDiedAction(Animator animator) {
            anim = animator;
        }

        public void Setup(GameObject gameObject) {
            penguin = gameObject;
        }

        public void Execute() {
//            Debug.Log("Penguin " + penguin + " was notified about the death");
            anim.SetTrigger(AnimationConstants.TRIGGER_REACT_TO_DEATH);
        }
    }
}