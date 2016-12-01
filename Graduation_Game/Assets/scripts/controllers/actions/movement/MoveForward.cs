using System.Collections;
using Assets.scripts.components;
using Assets.scripts.sound;
using UnityEngine;

namespace Assets.scripts.controllers.actions.movement {
	public class MoveForward : Action {
		private CharacterController characterController;
		private readonly Directionable directionable;
		private const float GRAVITY = 9.8f;
		private readonly Actionable<ControllableActions> actionable;
		private bool isFalling;
	    private CouroutineDelegateHandler delegator;
	    private bool movementBlocked;

		public MoveForward(Directionable directionable, Actionable<ControllableActions> actionable, CouroutineDelegateHandler delegator){
			this.actionable = actionable;
			this.directionable = directionable;
		    this.delegator = delegator;
		}

		public void Setup(GameObject gameObject) {
			characterController = gameObject.GetComponent<CharacterController>();
		    //AkSoundEngine.PostEvent(SoundConstants.PenguinSounds.START_MOVING, gameObject);
		}

		public void Execute() {
		    if (movementBlocked) return;

		    if ( !characterController.isGrounded ) {
				var dir = directionable.GetDirection();
				directionable.SetDirection(new Vector3(dir.x, dir.y - GRAVITY * Time.deltaTime, dir.z));
				directionable.SetJump(true);
			} else if ( characterController.isGrounded && directionable.GetJump() ) {
				if(!directionable.IsSliding())
			        directionable.SetSpeed(directionable.GetWalkSpeed());
				var dir = directionable.GetDirection();
				directionable.SetDirection(new Vector3(dir.x, -0.2f, dir.z));
				directionable.SetJump(false);
				actionable.ExecuteAction(ControllableActions.StopJump);
			}

			if (!Physics.Raycast(characterController.gameObject.transform.position, -Vector3.up, 0.5f)) {
				isFalling = true;
				actionable.ExecuteAction(ControllableActions.PenguinFall);
			} else {
				if (isFalling) {
					actionable.ExecuteAction(ControllableActions.PenguinStopFall);
					isFalling = false;
//				    delegator.StartCoroutine(BlockMovementWhileFallAnimationFinishes());
				}
			}

			characterController.Move(directionable.GetDirection() * directionable.GetSpeed() * Time.deltaTime);
		}
        /// <summary>
        /// This function will block penguins from moving while stop fall animation is being played.
        /// It doesn't work properly because "isFailing" is not being detected properly
        /// (sometimes it's triggered during when falling and collecting currency) and because
        /// different fall animations have different durations
        ///
        /// </summary>
        /// <returns></returns>
	    IEnumerator BlockMovementWhileFallAnimationFinishes() {
	        yield return new WaitForSeconds(0.3f);
	        movementBlocked = true;
	        yield return new WaitForSeconds(1f);
	        movementBlocked = false;
	    }
	}
}
