using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.traps {
	public class KillPenguin : Action {
		private Killable killable;

		public KillPenguin(Killable killable) {
			this.killable = killable;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			killable.Kill();
		}
	}
}
