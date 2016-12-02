using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.gamestate;
using Assets.scripts.sound;
using Assets.scripts.level;

namespace Assets.scripts.UI.screen.ingame {
	public class ToolButtons : MonoBehaviour, GameEntity, Draggable, SetSnappingTool, /*IPointerEnterHandler, IPointerExitHandler,*/ GameFrozenChecker {
		public Color returning, notReturning;
		[Tooltip("How long the level will be frozen when freeze tool is used (seconds)")]
		public int freezeToolTime = 5;
		private string buttonText;

		private SnappingToolInterface snapping;
		private InputManager inputManager;
		private GameObject currentObject;
		private Vector3 mouseHitPosition;
		private Camera cam;
		private Image img;
		private GameStateManager gameStateManager;
		private float timeFirstClick;
		private bool tutorialShown = false;
		private GameObject[] tooltips;

		private readonly Dictionary<string, List<GameObject>> tools = new Dictionary<string, List<GameObject>>();
		private bool dragging;
		private bool oneClick;
		private bool doubleTap;
		private const int layermask = 1 << 8;
		Color[] origColors;
		MeshRenderer[] meshes;
		private bool touchUsed = false;
		private bool wasToolHit = false;
		public float closessToCam = 10f;
		public float toolOffSetWhileMoving = 20f;

		protected void Awake() {
			InjectionRegister.Register(this);
		}

		protected void Start() {
			tools.Add(TagConstants.JUMPTEMPLATE, new List<GameObject>());
			tools.Add(TagConstants.SWITCHTEMPLATE, new List<GameObject>());
		//	tools.Add(TagConstants.SPEEDTEMPLATE, new List<GameObject>());
		/*	tools.Add(TagConstants.BRIDGETEMPLATE, new List<GameObject>());
			tools.Add(TagConstants.ENLARGETEMPLATE, new List<GameObject>());
			tools.Add(TagConstants.MINIMIZETEMPLATE, new List<GameObject>());*/

		/*	if (!GameObject.FindGameObjectWithTag(TagConstants.TOOLTUTORIAL)) {
				tutorialShown = true;
			}*/

			tooltips = GameObject.FindGameObjectsWithTag(TagConstants.TOOLTIP);
			tools.Add(TagConstants.Tool.FREEZE_TIME, new List<GameObject>());
			img = GetComponent<Image>();
			cam = Camera.main;
			PoolSystem(GameObject.FindGameObjectWithTag(TagConstants.SPAWNPOOL));

			foreach(var key in tools.Keys) {
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
			if(toolArray != null) {
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
			if(tools[TagConstants.Tool.FREEZE_TIME].Count > 0) {
				gameStateManager.SetGameFrozen(true);
				yield return new WaitForSeconds(freezeToolTime);
				gameStateManager.SetGameFrozen(false);
			}
		}

		public void PlaceTool(IList<GameObject> tools) {
			inputManager.BlockCameraMovement();
			var count = tools.Count;
			if(count <= 0) {
				inputManager.UnblockCameraMovement();
				return;
			}

			inputManager.BlockCameraMovement();
			dragging = true;
			currentObject = tools[count - 1];
			currentObject.SetActive(true);
			SaveOrigColors(currentObject);
			currentObject.GetComponentInChildren<BoxCollider>().enabled = false;
			tools.RemoveAt(count - 1);

		}

		protected void Update() {
			foreach(var touch in Input.touches) {
				touchUsed = true;
				if(touch.phase == TouchPhase.Began) {
					// first tap on tool
					if(!oneClick && IsATool(touch.position)) {
						oneClick = true;
						timeFirstClick = Time.time;
					}
					//second tap on tool
					else if(oneClick && IsATool(touch.position)) {
						oneClick = false;
						if(Time.time - timeFirstClick < 0.6f) {
							doubleTap = true;
						}
					}
				}
				if(touch.phase == TouchPhase.Began) {
					IsAToolHit(touch.position);
				} else if(dragging) {
					switch(touch.phase) {
					case TouchPhase.Moved:
							//Debug.Log(currentObject);
							// If Bridge PlaceBridgeObject
						PlaceObject(currentObject, touch.position);
						break;
					case TouchPhase.Ended:
							ReleaseTool(doubleTap);
						break;
					}
				}
			}
			if (touchUsed) {
				return;
			}
			// check double click
			if(Input.GetMouseButtonDown(0)) {
				// first click on tool
				if(!oneClick && IsATool(Input.mousePosition)) {
					oneClick = true;
					timeFirstClick = Time.time;
				}
				//second click on tool
				else if(oneClick && IsATool(Input.mousePosition)) {
					oneClick = false;
					if(Time.time - timeFirstClick < 0.6f) {
						doubleTap = true;
					}
				}
			}

			// pickup tool
			if(Input.GetMouseButtonDown(0) && !dragging) {
				IsAToolHit(Input.mousePosition);
			}

			// Place tool
			if(Input.GetMouseButton(0) && dragging) {
				PlaceObject(currentObject, Input.mousePosition);
			}
			// Release tool
			if(Input.GetMouseButtonUp(0) && dragging) {
				ReleaseTool(doubleTap);
			}
		}

		private bool IsATool(Vector3 pos) {
			RaycastHit hit;
			if(!Physics.Raycast(cam.ScreenPointToRay(pos), out hit, 400f)
			    || hit.transform == null
			    || hit.transform.parent == null
			    || hit.transform.parent.gameObject.GetComponent<components.Draggable>() == null) {
				return false;
			}

			return true;
		}

		private void IsAToolHit(Vector3 pos) {
			RaycastHit hit;
			if(!Physics.Raycast(cam.ScreenPointToRay(pos), out hit, 400f)
			    || hit.transform == null
			    || hit.transform.parent == null
			    || hit.transform.parent.gameObject.GetComponent<components.Draggable>() == null) {
				return;
			}
			wasToolHit = true;
			dragging = true;
			inputManager.BlockCameraMovement();
			hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
			currentObject = hit.transform.parent.gameObject;
			foreach (Transform t in currentObject.GetComponentsInChildren<Transform>()) {
				if (t.tag == TagConstants.LANECHANGEARROW) {
					t.localScale = new Vector3(1, 1, 1);
				} else if (t.tag == TagConstants.JUMPARROW) {
					t.localScale = new Vector3(2, 2, 2);
				}
			}
			SaveOrigColors(currentObject);
			AkSoundEngine.PostEvent(SoundConstants.ToolSounds.TOOL_PICK_UP, currentObject);
		}

		private void ReleaseTool(bool doubleTap) {
			if(doubleTap) { // return tool back
				dragging = false;
				// tooltip for remove tool finishes
				TooltipsRemove();
				FlyGOToButton(currentObject);

			} else { // place tool to the scene
				switch(currentObject.tag) {
				case TagConstants.JUMPTEMPLATE:
						AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.JUMP_PLACED, currentObject);
					break;
				case TagConstants.SWITCHTEMPLATE:
						AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.CHANGE_LANE_PLACED, currentObject);
					break;
				}
				foreach (Transform t in currentObject.GetComponentsInChildren<Transform>()) {
					if (t.tag == TagConstants.LANECHANGEARROW || t.tag == TagConstants.JUMPARROW) {
						t.gameObject.transform.localScale = Vector3.zero;
					}
				}
				dragging = false;
				currentObject.GetComponentInChildren<BoxCollider>().enabled = true;
				if ( wasToolHit ) {
					//tooltip for move tool finishes
					TooltipsMove(); 
					wasToolHit = false;
				} else {
					//tooltip for place tool finishes
					TooltipsPlace(); 
				}
			}
		/*	if (!tutorialShown) {
				DismissTutorial(currentObject.tag);
			}*/

			ChangeObjColotToOriginal(currentObject);
			StartCoroutine(CameraHack());
		}

		void DismissTutorial(string tag) {
			GameObject go = null;
			switch (tag) {
				case TagConstants.SWITCHTEMPLATE:
					go = GameObject.FindGameObjectWithTag(TagConstants.UI.IN_GAME_TOOL_SWITCH_LANE);
					break;
				case TagConstants.JUMPTEMPLATE:
					go = GameObject.FindGameObjectWithTag(TagConstants.UI.IN_GAME_TOOL_JUMP);
					break;
				case TagConstants.Tool.FREEZE_TIME:
					go = GameObject.FindGameObjectWithTag(TagConstants.UI.IN_GAME_TOOL_FREEZE_TIME);
					break;
			}
			foreach (Transform child in go.transform) {
				if (child.CompareTag(TagConstants.TOOLTUTORIAL))
					Destroy(child.gameObject);
			}
			tutorialShown = true;
		}
		void UpdateUI(string tag) {
			var tool = tools[tag];
			string uiTag = "";
			string textValue = "";

			switch(tag) {
				case TagConstants.SWITCHTEMPLATE:
					uiTag = TagConstants.UI.IN_GAME_TOOL_SWITCH_LANE;
					//textValue = "Switch Lane: ";
					break;
				case TagConstants.JUMPTEMPLATE:
					uiTag = TagConstants.UI.IN_GAME_TOOL_JUMP;
					//textValue = "Jump: ";
					break;
				case TagConstants.Tool.FREEZE_TIME:
					uiTag = TagConstants.UI.IN_GAME_TOOL_FREEZE_TIME;
					//textValue = "Freeze time: ";
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
			
			var newPos = new Vector3(position.x, position.y + toolOffSetWhileMoving, position.z);
			var ray = Camera.main.ScreenPointToRay(newPos);
			RaycastHit hit;
			RaycastHit hit2;

			if(!Physics.Raycast(ray, out hit, 400f, layermask) ||
				!hit.transform.tag.Equals(TagConstants.LANE)) {
				obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, closessToCam));
				doubleTap = true;
				ChangeObjColorToRed(obj);
				return;
			}

			if (Physics.Raycast(hit.transform.position, Vector3.up, out hit2, 1f, layermask)) {
				obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, closessToCam));
				doubleTap = true;
				ChangeObjColorToRed(obj);
				return;
			}
			doubleTap = false;
			obj.transform.position = hit.point;
			snapping.Snap(new Vector3(hit.point.x,hit.transform.position.y,hit.transform.position.z), obj.transform);
			ChangeObjColorToGreen(obj);
		}

		private void SaveOrigColors(GameObject obj){
			foreach (Transform go in obj.GetComponentsInChildren<Transform>()) {
				if (go.gameObject.tag == obj.tag) {
					meshes = new MeshRenderer[go.gameObject.GetComponentsInChildren<MeshRenderer>().Length];
					meshes = go.gameObject.GetComponentsInChildren<MeshRenderer>();
				}
			}

			origColors = new Color[meshes.Length];
			for (int i = 0; i < meshes.Length; i++) {
				origColors[i] = meshes[i].material.color;
			}
		}

		private void ChangeObjColorToRed(GameObject obj){
			for (int i = 0; i < meshes.Length; i++) {
				meshes[i].material.color = Color.red;
			}
		}

		private void ChangeObjColorToGreen(GameObject obj){
			for (int i = 0; i < meshes.Length; i++) {
				meshes[i].material.color = Color.green;
			}
		}

		private void ChangeObjColotToOriginal(GameObject obj){
			for (int i = 0; i < meshes.Length; i++) {
				meshes[i].material.color = origColors[i];
			}
		}

		private void FlyGOToButton(GameObject obj){
			Vector3 flyTo;
			GameObject go;
			switch (obj.transform.tag) {
				case TagConstants.JUMPTEMPLATE:
				case TagConstants.JUMP:
					go = GameObject.FindGameObjectWithTag(TagConstants.UI.IN_GAME_TOOL_JUMP);
					flyTo = Camera.main.ScreenToWorldPoint(new Vector3(go.transform.position.x, go.transform.position.y, 10f));
					StartCoroutine(FlyingObjToButton(obj, flyTo));
					break;
				case TagConstants.SWITCHTEMPLATE:
				case TagConstants.SWITCHLANE:
					go = GameObject.FindGameObjectWithTag(TagConstants.UI.IN_GAME_TOOL_SWITCH_LANE);
					flyTo = Camera.main.ScreenToWorldPoint(new Vector3(go.transform.position.x, go.transform.position.y, 10f));
					StartCoroutine(FlyingObjToButton(obj, flyTo));
					break;
			
			}
		}

		private IEnumerator FlyingObjToButton(GameObject obj, Vector3 destination){
			Vector3 startPos = obj.transform.position;
			Vector3 origScale = obj.transform.localScale;
			float startTime = Time.time;
			float speedFactor = 10f, journeyLength = Vector3.Distance(startPos, destination);
			float distCovered = (Time.time - startTime)*speedFactor;
			float fracJourney = distCovered / journeyLength;
			//print(path[0] + " " + path[1]);
			while(fracJourney<1f){
				distCovered = (Time.time - startTime)*speedFactor;
				fracJourney = distCovered / journeyLength;
				obj.transform.position = Vector3.Lerp(startPos, destination, fracJourney);
				obj.transform.localScale = origScale * (1f - fracJourney);
				yield return new WaitForEndOfFrame();
			}
			obj.transform.localScale = origScale;
			PutObjectInPool(currentObject.transform);
			UpdateUI(currentObject.tag);
			currentObject.SetActive(false);
			currentObject.GetComponentInChildren<BoxCollider>().enabled = false;
			currentObject = null;
			ChangeColor(notReturning);
			doubleTap = false;
			AkSoundEngine.PostEvent(SoundConstants.ToolSounds.TOOL_RETURNED, currentObject);
		}

		private void TooltipsPlace() {
			foreach ( GameObject t in tooltips ) {
				Tooltip tooltip = t.GetComponent<Tooltip>();
				if ( tooltip.GetType() == Tooltip.Type.Place && tooltip.IsActive()) {
					tooltip.SetPlace(true);
					return;
				}
			}
		}

		private void TooltipsMove() {
			foreach ( GameObject t in tooltips ) {
				Tooltip tooltip = t.GetComponent<Tooltip>();
				if ( tooltip.GetType() == Tooltip.Type.Move && tooltip.IsActive()) {
					tooltip.SetMove(true);
					return;
				}
			}
		}

		private void TooltipsRemove() {
			foreach ( GameObject t in tooltips ) {
				Tooltip tooltip = t.GetComponent<Tooltip>();
				if ( tooltip.GetType() == Tooltip.Type.Remove && tooltip.IsActive()) {
					tooltip.SetRemove(true);
					return;
				}
			}
		}

		private IEnumerator CameraHack() {
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

		public void SetInputManager(InputManager inputManage) {
			this.inputManager = inputManage;
		}

		public string GetTag() {
			return TagConstants.TOOLBUTTON;
		}

		public void SetupComponents() {
		}

		public GameObject GetGameObject() {
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
