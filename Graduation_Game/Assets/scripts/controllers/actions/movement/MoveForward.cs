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
		private const int layerMask = 1 << 8;

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

			//Debug.DrawRay(characterController.gameObject.transform.position, -Vector3.up * 0.45f, Color.red, 10000);
			// if penguin is not hitting the ground (i.e. penguin is in the air) and it wasn't falling before,
			// is it falling now
			if (!Physics.Raycast(characterController.gameObject.transform.position, -Vector3.up, 0.45f, layerMask)) {
				if ( !isFalling ) {
					actionable.ExecuteAction(ControllableActions.PenguinFall);
					isFalling = true;
				}
			} else {
				if (isFalling && !directionable.GetDoubleJump()) {
					movementBlocked = true;
					actionable.ExecuteAction(ControllableActions.PenguinStopFall);
					isFalling = false;
					delegator.StartCoroutine(BlockMovementWhileFallAnimationFinishes());
				} else if(isFalling && directionable.GetDoubleJump()) {
					delegator.StartCoroutine(RemoveDoubleJump());
				}

			}

			characterController.Move(directionable.GetDirection() * directionable.GetSpeed() * Time.deltaTime);
		}

		private IEnumerator RemoveDoubleJump(){
			yield return new WaitForSeconds(0.3f);
			directionable.SetDoubleJump(false);
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
	        yield return new WaitForSeconds(1f);
	        movementBlocked = false;
	    }
	}
}
