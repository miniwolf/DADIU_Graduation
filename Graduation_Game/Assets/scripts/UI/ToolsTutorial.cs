using UnityEngine;
using Assets.scripts;
using UnityEngine.UI;

public class ToolsTutorial : MonoBehaviour {

	// Use this for initialization
	public GameObject DespawnTrigger;

	void Start () {
		GameObject toolTutorial = gameObject; //new GameObject("toolTutorial");
		toolTutorial.transform.SetParent(DespawnTrigger.transform);
		toolTutorial.tag = TagConstants.TOOLTUTORIAL;
		Text label = DespawnTrigger.GetComponentInChildren<Text>();
		Text t = toolTutorial.AddComponent<Text>();
		t.text = "Drag the tools to skip the traps.";
		t.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		t.color = Color.black;
		t.fontStyle = FontStyle.BoldAndItalic;
		t.fontSize = 24;
		t.rectTransform.offsetMax = new Vector2(100, 100);
		t.transform.position = new Vector3(Screen.width /2, Screen.height/2 + 50, label.transform.localPosition.z);
	}
}
