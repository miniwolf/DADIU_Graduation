using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement
{
    public class MoveForward : Action {
        private CharacterController characterController;

        public void Setup(GameObject gameObject) {
            characterController = gameObject.GetComponent<CharacterController>();
        }

        public void Execute() {
            characterController.Move(Vector3.forward * Time.deltaTime);
        }
    }
}