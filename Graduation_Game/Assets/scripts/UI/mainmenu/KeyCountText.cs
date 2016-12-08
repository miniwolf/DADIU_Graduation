using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.UI.inventory;

public class KeyCountText : MonoBehaviour {
	Text text;
	protected void Start() {
		text = GetComponent<Text>();
		text.text = Inventory.key.GetValue().ToString();
	}
	void Update(){
		text.text = Inventory.key.GetValue().ToString();
	}
}
