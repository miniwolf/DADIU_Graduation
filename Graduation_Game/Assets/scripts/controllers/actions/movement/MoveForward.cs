using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement {
    public class MoveForward : Action {
        private CharacterController characterController;
		private readonly Directionable direction;
		private const float GRAVITY = 9.8f;

		public MoveForward(Directionable direction){
			this.direction = direction;
		}

        public void Setup(GameObject gameObject) {
            characterController = gameObject.GetComponent<CharacterController>();
        }

        public void Execute() {
			if ( !characterController.isGrounded ) {
				var dir = direction.GetDirection();
				direction.SetDirection(new Vector3(dir.x, dir.y - GRAVITY * Time.deltaTime, dir.z));
				direction.SetJump(true);
			} else if ( characterController.isGrounded && direction.GetJump() ) {
				direction.SetSpeed(direction.GetWalkSpeed());
				direction.SetJump(false);
			}

			characterController.Move(direction.GetDirection() * direction.GetSpeed() * Time.deltaTime);
        }
    }
}