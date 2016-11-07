using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.character {
	public class Penguin : ActionableGameEntityImpl<ControllableActions>, Directionable, Killable {
		public enum Lane {Left, Right};

		public Vector3 direction;
		public float jumpSpeed = 7;
		public float walkSpeed = 5;
		public float speed;
		public bool jump = false;
		public Lane lane = Lane.Left;
		private bool isDead;
		private CharacterController characterController;
		private float groundY;

		void Start() {
			groundY = transform.position.y;
			direction = new Vector3(1,0,0);
			characterController = GetComponent<CharacterController>();
			speed = walkSpeed;
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

		public float GetJumpSpeed() {
			return jumpSpeed;
		}

		public float GetWalkSpeed() {
			return walkSpeed;
		}

		public void SetSpeed(float speed) {
			this.speed = speed;
		}

		public float GetSpeed() {
			return speed;
		}

		public void SetJump(bool jump) {
			this.jump = jump;
		}

		public bool GetJump() {
			return jump;
		}

		public float GetGroundY() {
			return groundY;
		}

		public void Kill() {
			isDead = true;
		}

		public bool IsDead() {
			return isDead;
		}
	}
}
