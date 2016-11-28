using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;
using System.Collections.Generic;
using Assets.scripts.components.registers;
using Assets.scripts.gamestate;
using Assets.scripts.UI.screen.ingame;
using JetBrains.Annotations;
using System.Collections;
using Assets.scripts.sound;

namespace Assets.scripts.character {
	public class Penguin : ActionableGameEntityImpl<ControllableActions>, Directionable, Killable, GameFrozenChecker, Notifiable {
		public enum Lane {Left, Right};
		public enum CurveType {Speed, Enlarge, Minimize};
		public enum Weight {Normal, Big, Small}
		public float timeOnWinPlatform = 3.5f;

		public Vector3 direction;
		public float jumpSpeed = 7;
		public float walkSpeed = 5;
		public float slideSpeedupIncrement = 0.1f;
		public float speed;
		public bool jump;
		public Lane lane = Lane.Left;
		public Lane goingToLane = Lane.Left;
	    public bool isSliding;
	    private bool isDead;
		private bool isFrozen;
		private CharacterController characterController;
		private float groundY;
		private bool isRunning;
		private bool isEnlarging;
		private bool isMinimizing;
		private Dictionary<CurveType, AnimationCurve> curveDict;
		private Dictionary<CurveType, float> initialTimeDict;
		private Weight weight;
	    private GameStateManager gameStateManager;
		private bool isStopped = false;
	    private NotifierSystem notifierSystem;
	    private Camera deathCam;

	    void Start() {
			groundY = transform.position.y;
			characterController = GetComponent<CharacterController>();
			speed = walkSpeed;
			curveDict = new Dictionary<CurveType, AnimationCurve>();
			initialTimeDict = new Dictionary<CurveType, float>();
			weight = Weight.Normal;
	        deathCam =  GetComponentInChildren<Camera>();
	        deathCam.enabled = false;
	    }

		void FixedUpdate() {
			if (isStopped) {
				return;
			}
			// TODO make a bool variable to disable (or not) the buttons in the UI
			// so game designer can try and decide what option is better

		    if (gameStateManager.IsGameFrozen()) {
		        ExecuteAction(ControllableActions.Freeze);
		        return;
		    } else {
		        ExecuteAction(ControllableActions.UnFreeze);
		    }

			if (!isDead) {
				ExecuteAction(ControllableActions.Move);
				/*
				if ( isRunning ) {
					ExecuteAction(ControllableActions.Speed);
				}
				if ( isEnlarging ) {
					ExecuteAction(ControllableActions.Enlarge);
				}
				if ( isMinimizing ) {
					ExecuteAction(ControllableActions.Minimize);
				}*/
			} else {
			    
				if ( !characterController.isGrounded ) {
					//characterController.Move(new Vector3(0, -9.8f, 0) * Time.deltaTime);
				} else {
					//TODO Instantiate a dead penguin mesh into the position of the penguin.
					//characterController.enabled = false;
				}
			}
		}

		 IEnumerator OnTriggerEnter(Collider collider) {
			if (collider.transform.tag == TagConstants.WINZONE) {
				ExecuteAction(ControllableActions.Celebrate);
				ExecuteAction(ControllableActions.Win);
				yield return new WaitForSeconds(timeOnWinPlatform);
				ExecuteAction(ControllableActions.Stop);
			}
		}

	    void OnDestroy() {
	        StopSound();
	        gameStateManager = null;
	    }

	    private void StopSound()	    {
	        AkSoundEngine.PostEvent(SoundConstants.PenguinSounds.STOP_MOVING, gameObject);
	    }

	    public override string GetTag() {
			return TagConstants.PENGUIN;
		}

		public void SetCurve(CurveType type, AnimationCurve curve) {
			AnimationCurve curveStored;
			if ( curveDict.TryGetValue(type, out curveStored) ) {
				//overwrite previous curve (from previous tool)
				curveDict[type] = curve;
			} else {
				curveDict.Add(type, curve);
			}
		}

		public void SetInitialTime(CurveType type, float time) {
			float timeStored;
			if ( initialTimeDict.TryGetValue(type, out timeStored) ) {
				//overwrite previous time (from previous tool)
				initialTimeDict[type] = time;
			} else {
				initialTimeDict.Add(type, time);
			}
		}

		public AnimationCurve GetCurve(CurveType type) {
			AnimationCurve curve;
			curveDict.TryGetValue(type, out curve);
			return curve;
		}

		public float GetInitialTime(CurveType type) {
			float time;
			initialTimeDict.TryGetValue(type, out time);
			return time;
		}

		public void removeCurve(CurveType type) {
			curveDict.Remove(type);
		}

		public void removeInitialTime(CurveType type) {
			initialTimeDict.Remove(type);
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

	    public float GetSlideSpeedupIncrement()
	    {
	        return slideSpeedupIncrement;
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

	    public void SetSlide(bool sliding)
	    {
	        isSliding = sliding;
	    }

	    public bool GetJump() {
			return jump;
		}

		public float GetGroundY() {
			return groundY;
		}

		public void SetGoingTo(Lane left) {

		}

		public void Kill() {
			StopSound();
			isDead = true;
		}

		public bool IsDead() {
			return isDead;
		}

		public bool IsRunning() {
			return isRunning;
		}

	    public bool IsSliding() {
	        return isSliding;
	    }

	    public void SetRunning(bool running) {
			isRunning = running;
		}

		public bool IsEnlarging() {
			return isEnlarging;
		}

		public void SetEnlarging(bool enlarging) {
			isEnlarging = enlarging;
		}

		public bool IsMinimizing() {
			return isMinimizing;
		}

		public void SetMinimizing(bool minimizing) {
			isMinimizing = minimizing;
		}

		public void SetScale(Vector3 scale) {
			transform.localScale = scale;
		}

		public Vector3 GetInitialScale() {
			return new Vector3(1, 1, 1);
		}

		public Weight GetWeight() {
			return weight;
		}

		public void SetWeight(Weight weight) {
			this.weight = weight;
		}

	    public void SetGameStateManager(GameStateManager manager) {
	        gameStateManager = manager;
	    }

	    public void SetNotifyManager(NotifierSystem s)
	    {
	        notifierSystem = s;
	        notifierSystem.Register(NotifierSystem.Event.PenguinDied, this);
	    }

		public void SetStop(bool stop){
			isStopped = stop;
		}

	    /// <summary>
	    /// Triggered when some penguin dies
	    /// </summary>
	    /// <param name="penguin"></param>
	    public void Notify(GameObject penguin) {
	        ExecuteAction(ControllableActions.OtherPenguinDied);
	    }

	    public Camera GetDeathCam() {
	        return deathCam;
	    }
	}
}
