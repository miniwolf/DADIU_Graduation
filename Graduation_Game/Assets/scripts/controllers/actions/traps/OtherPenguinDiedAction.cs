using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.scripts.controllers.actions.traps
{
    public class OtherPenguinDiedAction : Action
    {
        private GameObject penguin;
		private Animator anim;
		private string animation;

		public OtherPenguinDiedAction(Animator animator, string animation) {
            anim = animator;
			this.animation = animation;
		}

        public void Setup(GameObject gameObject) {
            penguin = gameObject;
        }

        public void Execute() {
//            Debug.Log("Penguin " + penguin + " was notified about the death");
            anim.SetTrigger(animation);
        }
    }
}