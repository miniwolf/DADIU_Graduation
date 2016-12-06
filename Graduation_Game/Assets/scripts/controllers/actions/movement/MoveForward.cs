using System.Collections;
using System.Diagnostics;
using Assets.scripts.components;
using Assets.scripts.sound;
using UnityEngine;
<<<<<<< HEAD
using Debug = UnityEngine.Debug;
=======
using Assets.scripts.character;
>>>>>>> develop

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
		private float speed;
		private float raycastLength = 0.45f;
	    private float jumpStartMillis;
		private Penguin penguin;

		public MoveForward(Directionable directionable, Actionable<ControllableActions> actionable, CouroutineDelegateHandler delegator){
			this.actionable = actionable;
			this.directionable = directionable;
		    this.delegator = delegator;
		}

		public void Setup(GameObject gameObject) {
			penguin = gameObject.GetComponent<Penguin>();
			characterController = gameObject.GetComponent<CharacterController>();
		    //AkSoundEngine.PostEvent(SoundConstants.PenguinSounds.START_MOVING, gameObject);
		}

		public void Execute() {
			speed = directionable.GetWalkSpeed();

			if (movementBlocked) return;

		    if ( !characterController.isGrounded ) {
				var dir = directionable.GetDirection();
				directionable.SetDirection(new Vector3(dir.x, dir.y - GRAVITY * Time.deltaTime, dir.z));
				speed = directionable.GetSpeed();
				characterController.gameObject.GetComponentInChildren<Animator>().speed = 1.0f;
				directionable.SetJump(true);
		        if (jumpStartMillis < 0.0000001f) {
		            jumpStartMillis = Time.time;
//		            Debug.Log("Start time " + jumpStartMillis);
		        }
		    } else if ( characterController.isGrounded && directionable.GetJump() ) {
				if (!directionable.IsSliding()) {
					directionable.SetSpeed(directionable.GetWalkSpeed());
				}
				//directionable.SetSpeed(directionable.GetWalkSpeed());
				var dir = directionable.GetDirection();
				directionable.SetDirection(new Vector3(dir.x, -0.2f, dir.z));
				directionable.SetJump(false);
//		        Debug.Log("End jump time:" + Time.time);
		        jumpStartMillis = 0;
				actionable.ExecuteAction(ControllableActions.StopJump);
			}

			if (directionable.IsSliding()) {
				raycastLength = 1f;
			} else {
				raycastLength = 0.45f;
			}

			//Debug.DrawRay(characterController.gameObject.transform.position, -Vector3.up * 0.45f, Color.red, 10000);
			// if penguin is not hitting the ground (i.e. penguin is in the air) and it wasn't falling before,
			// is it falling now
			if (!Physics.Raycast(characterController.gameObject.transform.position, -Vector3.up, raycastLength, layerMask)) {
				if ( !isFalling && penguin.notWalkingOnSolidSurface) {
					actionable.ExecuteAction(ControllableActions.PenguinFall);
					isFalling = true;
				}
			} else {
				if ( isFalling && !directionable.GetDoubleJump() ) {
					if ( !directionable.IsSliding() ) {
						movementBlocked = true;
						actionable.ExecuteAction(ControllableActions.PenguinStopFall);
						isFalling = false;
						delegator.StartCoroutine(BlockMovementWhileFallAnimationFinishes());
					}
				} else if(isFalling && directionable.GetDoubleJump()) {
					delegator.StartCoroutine(RemoveDoubleJump());
				}

			}
			if (!directionable.GetJump()&&directionable.GetSpeedUp()) {
				characterController.gameObject.GetComponentInChildren<Animator>().speed = 1.5f;
			}
			characterController.Move(directionable.GetDirection() * speed * Time.deltaTime);
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
		    float jumpDuration = (Time.time - jumpStartMillis);
//		    Debug.Log("Penguin jumping: " + directionable.GetJump() + " isFalling: " + isFalling + " jump duration: " + jumpDuration );
		    jumpStartMillis = 0;
		    if(jumpDuration >= 0.6f) // hot fix for w0lvl1 penguin stopping when falling from the small cliff
		    yield return new WaitForSeconds(1f);
			movementBlocked = false;
		}
	}
}
