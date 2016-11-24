using UnityEngine;
using Assets.scripts.components;
using Assets.scripts.level;

namespace Assets.scripts.controllers.actions.tools {
	public class Jump : Action {
		private readonly Directionable direction;
		private readonly LevelSettings levelSettings;

		public Jump(Directionable direction, GameObject levelSettings) {
			this.direction = direction;
			this.levelSettings = levelSettings.GetComponent<LevelSettings>();
		}

		public void Setup(GameObject gameObject) {
			return;
		}

		public void Execute() {
			var moveDirection = direction.GetDirection();
			moveDirection.y = levelSettings.GetJumpHeight();
			direction.SetSpeed(direction.GetJumpSpeed());
			direction.SetDirection(moveDirection);
		}
	}
}
