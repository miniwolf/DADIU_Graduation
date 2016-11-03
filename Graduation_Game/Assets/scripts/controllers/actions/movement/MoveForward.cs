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
			if ( characterController.isGrounded ) {
				characterController.Move(direction.GetDirection() * Time.deltaTime);
			} else {
				characterController.Move(new Vector3(0, -GRAVITY, 0) * Time.deltaTime);
			}
        }
    }
}