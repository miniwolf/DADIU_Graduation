using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.scripts.UI.screen.ingame {
	public class ToolButtons : MonoBehaviour, GameEntity, Draggable, SetSnappingTool, IPointerEnterHandler, IPointerExitHandler {
		public Color returning, notReturning;

		private SnappingToolInterface snapping;
		private GameObject currentObject;
		private Vector3 mouseHitPosition;
		private Camera cam;
		private Image img;

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

		public void PlaceTool(string toolName) {
			switch ( toolName ) {
				case TagConstants.JUMPTEMPLATE:
				case TagConstants.BRIDGETEMPLATE:
				case TagConstants.ENLARGETEMPLATE:
				case TagConstants.MINIMIZETEMPLATE:
				case TagConstants.SPEEDTEMPLATE:
				case TagConstants.SWITCHTEMPLATE:
					PlaceTool(tools[toolName]);
					break;
				default:
					Debug.LogError("Could find a handler for '" + toolName + "' in ToolButtons.cs");
					break;
			}
		}

		public void PlaceTool(IList<GameObject> tools) {
			var count = tools.Count;
			if ( count <= 0 ) {
				return;
			}

			dragging = true;
			currentObject = tools[count - 1];
			currentObject.SetActive(true);
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
			if ( !Physics.Raycast(cam.ScreenPointToRay(pos), out hit, layermask)
				 || hit.transform == null
				 || hit.transform.parent.gameObject.GetComponent<components.Draggable>() == null ) {
				return;
			}

			dragging = true;
			hit.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
			currentObject = hit.transform.parent.gameObject;
		}

		private void ReleaseTool() {
			if ( shouldReturn ) {
				PutObjectInPool(currentObject.transform);
				currentObject.SetActive(false);
				currentObject.GetComponentInChildren<SphereCollider>().enabled = false;
				currentObject = null;
				dragging = false;
				ChangeColor(notReturning);
				shouldReturn = false;
			} else {
				dragging = false;
				currentObject.GetComponentInChildren<SphereCollider>().enabled = true;
			}
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

		private void ChangeColor(Color color) {
			img.color = color;
		}

		public bool IsDragging() {
			return dragging;
		}

		public void SetSnap(SnappingToolInterface snapTool) {
			snapping = snapTool;
		}

		public string GetTag () {
			return TagConstants.SNAPPING;
		}

		public void SetupComponents () {
		}

		public GameObject GetGameObject () {
			return gameObject;
		}

		public Actionable<T> GetActionable<T>() {
			return null;
		}
	}
}
