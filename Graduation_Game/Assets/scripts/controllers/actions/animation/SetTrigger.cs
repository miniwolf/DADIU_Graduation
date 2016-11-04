using UnityEngine;

namespace Assets.scripts.controllers.actions.animation {
	public class SetTrigger : Action {
		private readonly Animator animator;
		private readonly string anmationName;

		public SetTrigger(Animator animator, string anmationName)
		{
			this.animator = animator;
			this.anmationName = anmationName;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			animator.SetTrigger(anmationName);
		}
	}
}