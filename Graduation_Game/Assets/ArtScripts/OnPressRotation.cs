using UnityEngine;
using System.Collections;

public class OnPressRotation : MonoBehaviour {
	public float speed = 10f;
	public bool isInSettings = false;
	RectTransform rektTransform;

	private IEnumerator Rotation () {
		while ( true ) {
			if ( isInSettings ) {
				var rotate = rektTransform.transform.rotation;
				rektTransform.transform.rotation = new Quaternion(rotate.x, rotate.y, rotate.z + Time.deltaTime * speed, rotate.w);
			}
		}
		yield break;
	}

	public void InSettingsRotation (){
		isInSettings = true;
	}

	void Start () {
		rektTransform = gameObject.GetComponent<RectTransform>();
		StartCoroutine(Rotation());

	}

}
