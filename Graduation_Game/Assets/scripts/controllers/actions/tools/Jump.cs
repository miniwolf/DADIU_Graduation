using UnityEngine;
using System.Collections;
using Assets.scripts.character;
using Assets.scripts.components;
using Assets.scripts.level;

namespace Assets.scripts.controllers.actions.tools {
	public class Jump : Action {
		private readonly Directionable direction;
		private LevelSettings levelSettings;

		public Jump(Directionable direction, GameObject levelSettings) {
			this.direction = direction;
			this.levelSettings = levelSettings.GetComponent<LevelSettings>();
		}

		public void Setup(GameObject gameObject) {
			return;
		}

		public void Execute() {	
			Vector3 moveDirection = direction.GetDirection();
			moveDirection.y += levelSettings.GetJumpHeight();
			direction.SetSpeed(direction.GetJumpSpeed());
			direction.SetDirection(moveDirection);
		}
	}
}

