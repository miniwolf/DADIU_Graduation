using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.gamestate;
using Assets.scripts.sound;

namespace Assets.scripts.UI.screen.ingame {
	public class ToolButtons : MonoBehaviour, GameEntity, Draggable, SetSnappingTool, /*IPointerEnterHandler, IPointerExitHandler,*/ GameFrozenChecker {
		public Color returning, notReturning;
		[Tooltip("How long the level will be frozen when freeze tool is used (seconds)")]
		public int freezeToolTime = 5;

		private SnappingToolInterface snapping;
		private InputManager inputManager;
		private GameObject currentObject;
		private Vector3 mouseHitPosition;
		private Camera cam;
		private Image img;
		private GameStateManager gameStateManager;
		private float timeFirstClick;

		private readonly Dictionary<string, List<GameObject>> tools = new Dictionary<string, List<GameObject>>();
		private bool dragging;
		private bool oneClick;
		private bool doubleTap;
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
			//tools.Add(TagConstants.METALTEMPLATE, new List<GameObject>());

			img = GetComponent<Image>();
			cam = Camera.main;
			PoolSystem(GameObject.FindGameObjectWithTag(TagConstants.SPAWNPOOL));

		    foreach (var key in tools.Keys) {
		        UpdateUI(key);
		    }
		}

		private void PoolSystem(GameObject spawnPool) {
			foreach(var child in spawnPool.transform.GetComponentsInChildren<Transform>()) {
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
			} catch(KeyNotFoundException) {
//				Debug.Log("Did not add '" + objArray + "' for object '" + template.name + "'");
			}
			if (toolArray != null) {
				toolArray.Add(template);
			}
		}

		public void PlaceTool(string toolName) {
			if(gameStateManager.IsGameFrozen())
				return;

			switch(toolName) {
			case TagConstants.JUMPTEMPLATE:
			case TagConstants.BRIDGETEMPLATE:
			case TagConstants.ENLARGETEMPLATE:
			case TagConstants.MINIMIZETEMPLATE:
			case TagConstants.SPEEDTEMPLATE:
			case TagConstants.SWITCHTEMPLATE:
			//case TagConstants.METALTEMPLATE:
				PlaceTool(tools[toolName]);
				break;
			case TagConstants.Tool.FREEZE_TIME:
				StartCoroutine(FreezeTime());
				break;
			default:
				Debug.LogError("Could find a handler for '" + toolName + "' in ToolButtons.cs");
				break;
			}

		    UpdateUI(toolName);
		}

		public IEnumerator FreezeTime() {
		    if (tools[TagConstants.Tool.FREEZE_TIME].Count > 0)		    {
                gameStateManager.SetGameFrozen(true);
                yield return new WaitForSeconds(freezeToolTime);
                gameStateManager.SetGameFrozen(false);
		    }
		}

		public void PlaceTool(IList<GameObject> tools) {
			inputManager.BlockCameraMovement();
			var count = tools.Count;
			if ( count <= 0 ) {
				return;
			}

			inputManager.BlockCameraMovement();
			dragging = true;
			currentObject = tools[count - 1];
			currentObject.SetActive(true);
			currentObject.GetComponentInChildren<BoxCollider>().enabled = false;
			tools.RemoveAt(count - 1);
		}

		protected void Update() {
			foreach ( var touch in Input.touches) {
				if( touch.phase == TouchPhase.Began ) {
					// first tap on tool
					if ( !oneClick && IsATool(touch.position)) {
						oneClick = true;
						timeFirstClick = Time.time;
					} 
					//second tap on tool
					else if (oneClick && IsATool(touch.position)) { 
						oneClick = false;
						if ( Time.time - timeFirstClick < 0.6f ) {
							doubleTap = true;
						}
					}
				} 
				if ( touch.phase == TouchPhase.Began ) {
					IsAToolHit(touch.position);
				} else if ( dragging ) {
					switch (touch.phase) {
						case TouchPhase.Moved:
							//Debug.Log(currentObject);
							// If Bridge PlaceBridgeObject
							PlaceObject(currentObject, touch.position);
							break;
						case TouchPhase.Ended:
							ReleaseTool();
							break;
					}
				}
			}

			// check double click
			if ( Input.GetMouseButtonDown(0)) {
				// first click on tool
				if ( !oneClick && IsATool(Input.mousePosition)) {
					oneClick = true;
					timeFirstClick = Time.time;
				} 
				//second click on tool
				else if (oneClick && IsATool(Input.mousePosition)) { 
					oneClick = false;
					if ( Time.time - timeFirstClick < 0.6f ) {
						doubleTap = true;
					}
				}
			}

			// pickup tool
			if(Input.GetMouseButtonDown(0) && !dragging) {
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

		private bool IsATool(Vector3 pos) {
			RaycastHit hit;
			if ( !Physics.Raycast(cam.ScreenPointToRay(pos), out hit, 400f)
				|| hit.transform == null
				|| hit.transform.parent == null
				|| hit.transform.parent.gameObject.GetComponent<components.Draggable>() == null ) {
				return false;
			}

			return true;
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
			AkSoundEngine.PostEvent(SoundConstants.TOOL_PICK_UP, currentObject);
		}

		private void ReleaseTool() {
			if ( doubleTap ) {
				PutObjectInPool(currentObject.transform);
				UpdateUI(currentObject.tag);
				currentObject.SetActive(false);
				currentObject.GetComponentInChildren<BoxCollider>().enabled = false;
				currentObject = null;
				dragging = false;
				ChangeColor(notReturning);
				doubleTap = false;
				AkSoundEngine.PostEvent(SoundConstants.TOOL_RETURNED, currentObject);
			} else {
				switch ( currentObject.tag ) {
					case TagConstants.JUMPTEMPLATE:
						AkSoundEngine.PostEvent(SoundConstants.JUMP_TRIGGERED, currentObject);
						break;
					case TagConstants.SWITCHTEMPLATE:
						AkSoundEngine.PostEvent(SoundConstants.CHANGE_LANE, currentObject);
						break;
				}
				dragging = false;
				currentObject.GetComponentInChildren<BoxCollider>().enabled = true;
			}

			StartCoroutine(CameraHack());
		}

		void UpdateUI(string tag) {
//			print(tag);
			var tool = tools[tag];
		    string uiTag = "";
		    string textValue = "";

			switch(tag) {
			    case TagConstants.SWITCHTEMPLATE:
			        uiTag = TagConstants.UI.IN_GAME_TOOL_SWITCH_LANE;
			        textValue = "Switch Lane: ";
			        break;
				case TagConstants.JUMPTEMPLATE:
			        uiTag = TagConstants.UI.IN_GAME_TOOL_JUMP;
			        textValue = "Jump: ";
			        break;
			    case TagConstants.BRIDGETEMPLATE:
			        uiTag = TagConstants.UI.IN_GAME_TOOL_BRIDGE;
			        textValue = "Bridge: ";
			        break;
			    case TagConstants.ENLARGETEMPLATE:
			        uiTag = TagConstants.UI.IN_GAME_TOOL_ENLARGE;
			        textValue = "Enlarge: ";
			        break;
			    case TagConstants.MINIMIZETEMPLATE:
			        uiTag = TagConstants.UI.IN_GAME_TOOL_MINIMIZE;
			        textValue = "Minimize: ";
			        break;
			    case TagConstants.SPEEDTEMPLATE:
			        uiTag = TagConstants.UI.IN_GAME_TOOL_SPEED;
			        textValue = "Speed: ";
			        break;
			    case TagConstants.Tool.FREEZE_TIME:
			        uiTag = TagConstants.UI.IN_GAME_TOOL_FREEZE_TIME;
			        textValue = "Freeze time: ";
			        break;
			}

		    var text = GetText(uiTag);
		    if(text != null)
		        text.text = textValue + tool.Count;
		}

	    private Text GetText(string uiTag) {
	        GameObject go = GameObject.FindGameObjectWithTag(uiTag);
	        if (go != null) // tool might be disabled in first levels
	            return go.GetComponentInChildren<Text>();
	        return null;
	    }

	    private void PlaceObject(GameObject obj, Vector3 position) {

			// Special case when we have a bridge
			if(obj.tag == TagConstants.BRIDGETEMPLATE) {
				var ray = Camera.main.ScreenPointToRay(position);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 400f, layermask)) {
					//Debug.Log("Level height : " + hit.transform.position.y + ", Hit point height : " + hit.point.y);
					float hitPointHeight = hit.point.y;
					float currentLevelHeight = hit.transform.position.y + 1f; // 1f is added because of how the parent of the blocks is transformed

					// Makes sure the placement of a bridge does not go
					// below the height of the current block
					if (hitPointHeight < currentLevelHeight) {
						return;
					}

					Vector3 hitWithFixedLevelHeight = new Vector3(hit.point.x, currentLevelHeight, hit.point.z);

					obj.transform.position = hitWithFixedLevelHeight;
					snapping.Snap(hitWithFixedLevelHeight, obj.transform);
				}
			}

			// All other tools
			else {
				var ray = Camera.main.ScreenPointToRay(position);
				RaycastHit hit;


				if (!Physics.Raycast(ray, out hit, 400f, layermask) ||
					!hit.transform.tag.Equals(TagConstants.LANE)) {
					return;
				}

				obj.transform.position = hit.point;
				snapping.Snap(hit.point, obj.transform);
			}

		}

		/*
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
		*/

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

		public void SetGameStateManager(GameStateManager manager) {
			gameStateManager = manager;
		}
	}
}
