using UnityEngine;
using Assets.scripts.pickups;
using System.Collections.Generic;

public class PlutoniumCounterController : MonoBehaviour {

	Queue<Plutonium> children;
	 
	public void SetupFlowing() {
		children = new Queue<Plutonium>(GetComponentsInChildren<Plutonium>(true));
	}

	public bool FlowPlutonium()	{
		if (children.Count > 0) {
			Plutonium g = children.Dequeue();
			g.gameObject.SetActive(true);
			/*Camera camera = Camera.main;
			g.transform.localPosition = camera.WorldToScreenPoint(gameObject.transform.localPosition);*/
			g.TriggerFlow();
			return true;
		}
		return false;
	}
}
