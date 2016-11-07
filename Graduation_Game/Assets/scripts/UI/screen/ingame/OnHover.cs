using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.scripts;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public Color returning, notReturning;
	private RaycastHit hit;
	public Camera cam;
	private bool shouldMove = false;
	private GameObject movingAround;
	private int layerMask = 1 << 8;
	private Image img;
	private bool shouldReturn = false;

	void Start(){
		img = GetComponent<Image>();
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
			NoLongerMoveTool();
		}
	}

	public void OnPointerEnter(PointerEventData data){
		if (shouldMove) {
			img.color = returning;
			shouldReturn = true;
		}
	}
	public void OnPointerExit(PointerEventData data){
		if (shouldMove) {
			NoColor();
			shouldReturn = false;
		}
	}
	void NoColor(){
		img.color = notReturning;
	}

	void IsAToolHit(Vector3 pos){
		if (Physics.Raycast(cam.ScreenPointToRay(pos),out hit)) {
			if (hit.transform.tag == TagConstants.JUMPTEMPLATE||hit.transform.tag == TagConstants.SWITCHTEMPLATE) {
				shouldMove = true;
				hit.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
				movingAround = hit.transform.gameObject;
			}
		}
	}

	void MoveTheTool(Vector3 pos){
		if (Physics.Raycast(cam.ScreenPointToRay(pos), out hit, 400f, layerMask)) {
			if (hit.transform.tag ==TagConstants.LANE) {
				movingAround.transform.parent.position = hit.point;
				//print(cam.WorldToViewportPoint(Input.mousePosition).y);
				//Add some kind of visual which makes it clear it will be reclaimed
			}
		}
	}

	void NoLongerMoveTool(){
		if (shouldReturn) {
			Destroy(movingAround.transform.parent.transform.gameObject);
			shouldMove = false;
			NoColor();
			shouldReturn = false;
			//Add it back to the count you have for the stash
		} else {
			shouldMove = false;
			movingAround.GetComponentInChildren<SphereCollider>().enabled = true;
		}
		}
}
