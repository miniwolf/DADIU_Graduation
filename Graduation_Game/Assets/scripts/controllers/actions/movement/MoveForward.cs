using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement
{
    public class MoveForward : Action {
        private CharacterController characterController;
		private readonly Directionable direction;

		public MoveForward(Directionable direction){
			this.direction = direction;
		}

        public void Setup(GameObject gameObject) {
            characterController = gameObject.GetComponent<CharacterController>();
        }

        public void Execute() {
			characterController.Move(direction.GetDirection() * Time.deltaTime);
        }
    }
}