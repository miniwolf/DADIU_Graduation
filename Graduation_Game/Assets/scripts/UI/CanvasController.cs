using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.scripts.components;

namespace Assets.scripts.components.factory {
	public class CanvasController : MonoBehaviour {

		// Use this for initialization
		void Start () {
			GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>().text = GameObject.FindGameObjectsWithTag(TagConstants.PENGUIN).Length.ToString();
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
