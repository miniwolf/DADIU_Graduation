using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.character {
	public class Penguin : ActionableGameEntityImpl<ControllableActions>, Directionable, Killable {
		public enum Lane {Left, Right};

		private Vector3 direction;
		public Lane lane = Lane.Left;
		private bool isDead;
		private CharacterController characterController;

		void Start() {
			direction = new Vector3(1,0,0);
			characterController = GetComponent<CharacterController>();
		}

		void Update() {
			if (!isDead) {
				ExecuteAction(ControllableActions.Move);
			} else {
				if ( !characterController.isGrounded ) {
					characterController.Move(new Vector3(0, -9.8f, 0) * Time.deltaTime);
				} else {
					characterController.enabled = false;
				}
			}
		}
		public override string GetTag() {
			return TagConstants.PENGUIN;
		}

		public Vector3 GetDirection() {
			return direction;
		}

		public void SetDirection(Vector3 direction) {
			this.direction = direction;
		}

		public Lane GetLane() {
			return lane;
		}

		public void SetLane(Lane lane) {
			this.lane = lane;
		}

		public void Kill() {
			isDead = true;
		}

		public bool IsDead() {
			return isDead;
		}
	}
}
