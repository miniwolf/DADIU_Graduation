using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollbarAspect : MonoBehaviour {

	void OnValidate () {
		Awake();
	}

	void Start () {
		Awake();
	}

	public void Awake() {
		transform.GetComponent<Scrollbar>().size = 0;
	}
}
