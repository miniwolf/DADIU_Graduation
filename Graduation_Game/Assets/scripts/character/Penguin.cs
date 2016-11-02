using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.character {
    public class Penguin : ActionableGameEntityImpl<ControllableActions> {
        private Vector3 destination = Vector3.forward;

        void Update() {
            ExecuteAction(ControllableActions.Move);

            if ( GetComponent<CharacterController>().velocity.magnitude < 0.2f ) {
                ExecuteAction(ControllableActions.Stop);
            }
        }

        public override string GetTag() {
            return TagConstants.PLAYER;
        }

        public Vector3 GetDestination() {
            return destination;
        }

        public void SetDestination(Vector3 destination) {
            this.destination = destination;
        }
    }
}

