using UnityEngine;

namespace Assets.scripts.controllers.actions.animation {
	public class SetBoolFalse : Action {
		private readonly Animator animator;
		private readonly string animationName;

		public SetBoolFalse(Animator animator, string animationName) {
			this.animator = animator;
			this.animationName = animationName;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			animator.SetBool(animationName, false);
		}
	}
}