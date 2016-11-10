using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.scripts.tools
{
	public class DraggableTool : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
		public void OnBeginDrag(PointerEventData eventData) {
			Debug.Log("Getting called");
			// TODO: Call to ToolButtons
		}

		public void OnDrag(PointerEventData eventData) {
			GetComponent<SphereCollider>().enabled = false;
			transform.position = Input.mousePosition;
		}

		public void OnEndDrag(PointerEventData eventData) {
			GetComponent<SphereCollider>().enabled = false;
		}
	}
}