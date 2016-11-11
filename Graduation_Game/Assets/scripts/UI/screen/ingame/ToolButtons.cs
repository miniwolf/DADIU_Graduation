using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.scripts.character;
using Assets.scripts.gamestate;

namespace Assets.scripts.UI.screen.ingame {
	public class ToolButtons : MonoBehaviour, GameEntity, Draggable, SetSnappingTool, IPointerEnterHandler, IPointerExitHandler, GameFrozenChecker {
		public Color returning, notReturning;

		private SnappingToolInterface snapping;
		private InputManager inputManager;
		private GameObject currentObject;
		private Vector3 mouseHitPosition;
		private Camera cam;
		private Image img;
	    private GameStateManager gameStateManager;

		private readonly Dictionary<string, List<GameObject>> tools = new Dictionary<string, List<GameObject>>();
		private bool dragging;
		private bool shouldReturn;
		private const int layermask = 1 << 8;

		protected void Awake() {
			InjectionRegister.Register(this);
		}

		protected void Start() {
			tools.Add(TagConstants.JUMPTEMPLATE, new List<GameObject>());
			tools.Add(TagConstants.SPEEDTEMPLATE, new List<GameObject>());
			tools.Add(TagConstants.SWITCHTEMPLATE, new List<GameObject>());
			tools.Add(TagConstants.BRIDGETEMPLATE, new List<GameObject>());
			tools.Add(TagConstants.ENLARGETEMPLATE, new List<GameObject>());
			tools.Add(TagConstants.MINIMIZETEMPLATE, new List<GameObject>());
			tools.Add(TagConstants.Tool.FREEZE_TIME, new List<GameObject>());

			img = GetComponent<Image>();
			cam = Camera.main;
			PoolSystem(GameObject.FindGameObjectWithTag(TagConstants.SPAWNPOOL));
		}

		private void PoolSystem(GameObject spawnPool) {
			foreach ( var child in spawnPool.transform.GetComponentsInChildren<Transform>() ) {
				PutObjectInPool(child);
			}
		}

		private void PutObjectInPool(Component child) {
			PoolSystem(child.tag, child.gameObject);
		}

		// Instantiates prefabs of length n, stores them in an array objArray
		// and sets them to all to false.
		private void PoolSystem(string objArray, GameObject template) {
			List<GameObject> toolArray = null;
			try {
				toolArray = tools[objArray];
			} catch (KeyNotFoundException) {
				Debug.Log("Did not add '" + objArray +"' for object '" + template.name + "'");
			}
			if (toolArray != null) {
				toolArray.Add(template);
			}
		}

		public void PlaceTool(string toolName)
		{
		    if (gameStateManager.IsGameFrozen()) return;

			switch ( toolName ) {
				case TagConstants.JUMPTEMPLATE:
				case TagConstants.BRIDGETEMPLATE:
				case TagConstants.ENLARGETEMPLATE:
				case TagConstants.MINIMIZETEMPLATE:
				case TagConstants.SPEEDTEMPLATE:
				case TagConstants.SWITCHTEMPLATE:
					PlaceTool(tools[toolName]);
					break;
                case TagConstants.Tool.FREEZE_TIME:
			        StartCoroutine(FreezeTime());
			        break;
			    default:
					Debug.LogError("Could find a handler for '" + toolName + "' in ToolButtons.cs");
					break;
			}
		}

		public IEnumerator FreezeTime() {
		    gameStateManager.SetGameFrozen(true);
		    yield return new WaitForSeconds(5f);
		    gameStateManager.SetGameFrozen(false);
		}

		public void PlaceTool(IList<GameObject> tools) {
			inputManager.BlockCameraMovement();
			var count = tools.Count;
			if ( count <= 0 ) {
				return;
			}

			dragging = true;
			currentObject = tools[count - 1];
			currentObject.SetActive(true);
			currentObject.GetComponentInChildren<BoxCollider>().enabled = false;
			tools.RemoveAt(count - 1);
		}

		protected void Update() {
			foreach ( var touch in Input.touches) {
				if ( touch.phase == TouchPhase.Began ) {
					IsAToolHit(touch.position);
				} else if ( dragging ) {
					switch (touch.phase) {
						case TouchPhase.Moved:
							PlaceObject(currentObject, touch.position);
							break;
						case TouchPhase.Ended:
							ReleaseTool();
							break;
					}
				}
			}

			// Pickup tool
			if ( Input.GetMouseButtonDown(0) && !dragging ) {
				IsAToolHit(Input.mousePosition);
			}
			// Place tool
			if ( Input.GetMouseButton(0) && dragging ) {
				PlaceObject(currentObject, Input.mousePosition);
			}
			// Release tool
			if ( Input.GetMouseButtonUp(0) && dragging ) {
				ReleaseTool();
			}
		}

		private void IsAToolHit(Vector3 pos) {
			RaycastHit hit;
			if ( !Physics.Raycast(cam.ScreenPointToRay(pos), out hit, 400f)
				 || hit.transform == null
				 || hit.transform.parent == null
				 || hit.transform.parent.gameObject.GetComponent<components.Draggable>() == null ) {
				return;
			}

			dragging = true;
			inputManager.BlockCameraMovement();
			hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
			currentObject = hit.transform.parent.gameObject;
		}

		private void ReleaseTool() {
			if ( shouldReturn ) {
				PutObjectInPool(currentObject.transform);
				currentObject.SetActive(false);
				currentObject.GetComponentInChildren<BoxCollider>().enabled = false;
				currentObject = null;
				dragging = false;
				ChangeColor(notReturning);
				shouldReturn = false;
			} else {
				dragging = false;
				currentObject.GetComponentInChildren<BoxCollider>().enabled = true;
			}
			StartCoroutine(CameraHack());
		}

		private void PlaceObject(GameObject obj, Vector3 position) {
			var ray =  Camera.main.ScreenPointToRay(position);
			RaycastHit hit;

			if ( !Physics.Raycast(ray, out hit, 400f, layermask) ||
			     !hit.transform.tag.Equals(TagConstants.LANE) ) {
				return;
			}

			obj.transform.position = hit.point;
			snapping.Snap(hit.point, obj.transform);
		}

		public void OnPointerEnter(PointerEventData data){
			if ( !dragging ) {
				return;
			}

			ChangeColor(returning);
			shouldReturn = true;
		}

		public void OnPointerExit(PointerEventData data){
			if ( !dragging ) {
				return;
			}

			ChangeColor(notReturning);
			StartCoroutine(CheckIfItemShouldBeDestroyedUsingTouch());
		}

		private IEnumerator CheckIfItemShouldBeDestroyedUsingTouch(){
			yield return new WaitForSeconds(0.2f);
			shouldReturn = false;
			ChangeColor(notReturning);
		}

		private IEnumerator CameraHack(){
			yield return new WaitForSeconds(0.2f);
			inputManager.UnblockCameraMovement();
		}

		private void ChangeColor(Color color) {
			img.color = color;
		}

		public bool IsDragging() {
			return dragging;
		}

		public void SetSnap(SnappingToolInterface snapTool) {
			snapping = snapTool;
		}

		public void SetInputManager(InputManager inputManage){
			this.inputManager = inputManage;
		}

		public string GetTag () {
			return TagConstants.TOOLBUTTON;
		}

		public void SetupComponents () {
		}

		public GameObject GetGameObject () {
			return gameObject;
		}

		public Actionable<T> GetActionable<T>() {
			return null;
		}

	    public void SetGameStateManager(GameStateManager manager)
	    {
	        gameStateManager = manager;
	    }
	}
}
