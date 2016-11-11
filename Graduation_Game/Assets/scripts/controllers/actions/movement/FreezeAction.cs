using Assets.scripts.components;
using Assets.scripts.gamestate;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement {
	public class FreezeAction : Action {
		private Animator animator;
		private bool frozen;

		public FreezeAction(Animator animator, bool frozen) {
			this.animator = animator;
			this.frozen = frozen;
		}

		public void Setup(GameObject gameObject) {

		}

		public void Execute() {
			animator.speed = frozen ? 0 : 1;
		}
	}
}
