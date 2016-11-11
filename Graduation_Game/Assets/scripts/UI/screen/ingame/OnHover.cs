using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.scripts;
using Assets.scripts.components;
using Assets.scripts.components.registers;

namespace Assets.scripts.UI.screen.ingame{
	public class OnHover : MonoBehaviour, GameEntity, IPointerEnterHandler, IPointerExitHandler, SetSnappingTool {
		public Color returning, notReturning;
		private RaycastHit hit;
		Camera cam;
		private bool shouldMove = false;
		private GameObject movingAround;
		private int layerMask = 1 << 8;
		private Image img;
		private bool shouldReturn = false;
		private bool touchPhaseEnded = false;
		private SnappingToolInterface snap;
		private InputManager inputManager;

		void Awake(){
			InjectionRegister.Register(this);
		}

		void Start(){
			img = GetComponent<Image>();
			cam = Camera.main;
		}

		void Update(){
			if (Input.touchCount > 0) {
				Touch touch = Input.GetTouch(0);
				switch (touch.phase) {
					case TouchPhase.Began:
						IsAToolHit(touch.position);
						break;
					case TouchPhase.Moved:
						if (shouldMove) {
							MoveTheTool(touch.position);
						}
						break;
					case TouchPhase.Ended:
						if (shouldMove) {
							touchPhaseEnded = true;
							NoLongerMoveTool();
						}
						break;
				}
			}

			if (Input.GetMouseButtonDown(0)) {
				IsAToolHit(Input.mousePosition);
			}
			if (shouldMove) {
				MoveTheTool(Input.mousePosition);
			}
			if (Input.GetMouseButtonUp(0) && shouldMove) {
				touchPhaseEnded = true;
				NoLongerMoveTool();
			}
		}

		public void OnPointerEnter(PointerEventData data){
			if (shouldMove) {
				ReturningColor();
				shouldReturn = true;
			}
		}
		public void OnPointerExit(PointerEventData data){
			if (shouldMove) {
				NoColor();
				StartCoroutine(CheckIfItemShouldBeDestroyedUsingTouch());
			}
		}
		void NoColor(){
			img.color = notReturning;
		}
		void ReturningColor(){
			img.color = returning;
		}

		void IsAToolHit(Vector3 pos){
			if (Physics.Raycast(cam.ScreenPointToRay(pos),out hit)) {
				if (hit.transform.tag == TagConstants.JUMPTEMPLATE || hit.transform.tag == TagConstants.SWITCHTEMPLATE 
					|| hit.transform.tag == TagConstants.SPEEDTEMPLATE || hit.transform.tag == TagConstants.ENLARGETEMPLATE
					|| hit.transform.tag == TagConstants.MINIMIZETEMPLATE || hit.transform.tag == TagConstants.BRIDGETEMPLATE) {
					shouldMove = true;
					if (hit.transform.tag != TagConstants.BRIDGETEMPLATE) {
						hit.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
					} else {
						hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
					}
					movingAround = hit.transform.parent.gameObject;
					inputManager.BlockCameraMovement();
					print(inputManager.IsCameraBlocked());
				}
			}
		}

		void MoveTheTool(Vector3 pos){
			if (Physics.Raycast(cam.ScreenPointToRay(pos), out hit, 400f, layerMask)) {
				if (hit.transform.tag ==TagConstants.LANE) {
						snap.Snap(hit.point, movingAround.transform);
					//Add some kind of visual which makes it clear it will be reclaimed
				}

			}
		}

		void NoLongerMoveTool(){
				if (shouldReturn&&touchPhaseEnded) {
				Destroy(movingAround);
				shouldMove = false;
				NoColor();
				shouldReturn = false;
				//Add it back to the count you have for the stash
			} else {
				shouldMove = false;
				if (movingAround.tag != TagConstants.BRIDGETEMPLATE) {
					movingAround.GetComponentInChildren<SphereCollider>().enabled = true;
				} else {
					movingAround.GetComponentInChildren<BoxCollider>().enabled = true;
				}
			}
			StartCoroutine(CameraHack());
		}

		IEnumerator CameraHack(){
			yield return new WaitForSeconds(0.2f);
			inputManager.UnblockCameraMovement();
		}

		IEnumerator CheckIfItemShouldBeDestroyedUsingTouch(){
			yield return new WaitForSeconds(0.2f);
			shouldReturn = false;
			NoColor();
		}

		public void SetSnap (SnappingToolInterface snapTool) {
			snap = snapTool;
		}

		public void SetInputManager (InputManager inputManage) {
			this.inputManager = inputManage;
		}

		public string GetTag () {
			return TagConstants.TOOLBUTTON;
		}
		public void SetupComponents () {
			return;
		}
		public GameObject GetGameObject () {
			return gameObject;
		}

		public Actionable<T> GetActionable<T>() {
			return null;
		}
	}
}