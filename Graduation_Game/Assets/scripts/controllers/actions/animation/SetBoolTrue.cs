using UnityEngine;

namespace Assets.scripts.controllers.actions.animation {
	public class SetBoolTrue : Action {
		private readonly Animator animator;
		private readonly string animationName;

		public SetBoolTrue(Animator animator, string animationName) {
			this.animator = animator;
			this.animationName = animationName;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			animator.SetBool(animationName, true);
		}
	}
}
