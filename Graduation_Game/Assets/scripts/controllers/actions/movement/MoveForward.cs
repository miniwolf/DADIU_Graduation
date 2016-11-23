using Assets.scripts.components;
using Assets.scripts.sound;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement {
	public class MoveForward : Action {
		private CharacterController characterController;
		private readonly Directionable direction;
		private const float GRAVITY = 9.8f;
		private readonly Actionable<ControllableActions> actionable;

		public MoveForward(Directionable direction, Actionable<ControllableActions> actionable){
			this.actionable = actionable;
			this.direction = direction;
		}

		public void Setup(GameObject gameObject) {
			characterController = gameObject.GetComponent<CharacterController>();
		    AkSoundEngine.PostEvent(SoundConstants.PenguinSounds.START_MOVING, gameObject);
		}

		public void Execute() {
			if ( !characterController.isGrounded ) {
				var dir = direction.GetDirection();
				direction.SetDirection(new Vector3(dir.x, dir.y - GRAVITY * Time.deltaTime, dir.z));
				direction.SetJump(true);
			} else if ( characterController.isGrounded && direction.GetJump() ) {
				direction.SetSpeed(direction.GetWalkSpeed());
				var dir = direction.GetDirection();
				direction.SetDirection(new Vector3(dir.x, -0.2f, dir.z));
				direction.SetJump(false);
				actionable.ExecuteAction(ControllableActions.StopJump);
			}

			characterController.Move(direction.GetDirection() * direction.GetSpeed() * Time.deltaTime);
		}
	}
}
