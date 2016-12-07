using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;
using System.Collections.Generic;
using Assets.scripts.components.registers;
using Assets.scripts.gamestate;
using Assets.scripts.UI.screen.ingame;
using System.Collections;

namespace Assets.scripts.character {
	public class Penguin : ActionableGameEntityImpl<ControllableActions>, Directionable, Killable, GameFrozenChecker, Notifiable {
		public enum Lane {Left, Right};
		public enum CurveType {Speed, Enlarge, Minimize};
		public enum Weight {Normal, Big, Small}

		public Vector3 direction;
		private Vector3 screenPoint;
		public float timeOnWinPlatform = 2.75f;
		public float jumpSpeed = 7;
		public float walkSpeed = 5;
		public float slideSpeedupIncrement = 0.01f;
		public float slideMaxSpeedMult = 20;
		public float speed;
		private float groundY;
		public bool jump;
		public bool isSliding;
		public bool notWalkingOnSolidSurface;
		private bool doubleJump, speedUp, isDead, isFrozen, isRunning, isEnlarging, isMinimizing, isStopped;
		public Lane lane = Lane.Left;
		public Lane goingToLane = Lane.Left;
		private CharacterController characterController;
		private Dictionary<CurveType, AnimationCurve> curveDict;
		private Dictionary<CurveType, float> initialTimeDict;
		private Weight weight;
		private GameStateManager gameStateManager;
		private NotifierSystem notifierSystem;
		private Camera deathCam;
		private Moveable moveable;

		private void Start() {
			//moveable = new Moveable();
			groundY = transform.position.y;
			characterController = GetComponent<CharacterController>();
			speed = walkSpeed;
			curveDict = new Dictionary<CurveType, AnimationCurve>();
			initialTimeDict = new Dictionary<CurveType, float>();
			weight = Weight.Normal;
			deathCam =  GetComponentInChildren<Camera>();
			deathCam.enabled = false;
		}

		private void FixedUpdate() {
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
			} else {
				if ( !characterController.isGrounded ) {
					//characterController.Move(new Vector3(0, -9.8f, 0) * Time.deltaTime);
				} else {
					//TODO Instantiate a dead penguin mesh into the position of the penguin.
					//characterController.enabled = false;
				}
			}
		}

		private void OnCollisionEnter(Collision collision) {
			notWalkingOnSolidSurface = collision.transform.tag == TagConstants.LANE;
		}

		private IEnumerator OnTriggerEnter(Collider collider) {
			if ( collider.transform.tag != TagConstants.WINZONE ) {
				yield break;
			}

			ExecuteAction(ControllableActions.Celebrate);
			ExecuteAction(ControllableActions.Win);
			yield return new WaitForSeconds(timeOnWinPlatform);
			//ExecuteAction(ControllableActions.Stop);
			isStopped = true;
		}

		private void OnDestroy() {
			gameStateManager = null;
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

		public void SetWalkSpeed(float walkSpeed) {
			this.walkSpeed = walkSpeed;
		}

		public float GetSlideSpeedupIncrement() {
			return slideSpeedupIncrement;
		}

		public float GetSlideMaxSpeedMult() {
			return slideMaxSpeedMult;
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

		public void SetSlide(bool sliding) {
			isSliding = sliding;
		}

		public bool GetJump() {
			return jump;
		}

		public bool GetDoubleJump(){
			return doubleJump;
		}

		public void SetDoubleJump(bool doubleJump){
			this.doubleJump = doubleJump;
		}

		public bool GetSpeedUp(){
			return speedUp;
		}

		public void SetSpeedUp(bool speedUp){
			this.speedUp = speedUp;
		}

		public float GetGroundY() {
			return groundY;
		}

		public void SetGoingTo(Lane left) {

		}

		public void Kill() {
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

		public void SetStop(bool stop){
			isStopped = stop;
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

		public void SetNotifyManager(NotifierSystem s) {
			notifierSystem = s;
			notifierSystem.Register(NotifierSystem.Event.PenguinDied, this);
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
