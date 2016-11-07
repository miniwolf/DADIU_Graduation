using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.scripts.components;

namespace Assets.scripts.UI {
	public class CanvasController : MonoBehaviour {

		// Use this for initialization
		void Start () {
			GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>().text = GameObject.FindGameObjectsWithTag(TagConstants.PENGUIN).Length.ToString();
		}
	}
}
