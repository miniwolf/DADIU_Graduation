using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenShop : MonoBehaviour {
	private GameObject settingsPanel;
	private GameObject shopPanel;
	private GameObject currentPanel;

	private void Start() {
		settingsPanel = transform.GetChild(0).gameObject;
		shopPanel = transform.GetChild(1).gameObject;
	}

	public void OpenStore() {
		shopPanel.GetComponent<Animator>().Play("PanelIn");
		currentPanel = shopPanel;
	}

	public void OpenSettings() {
		settingsPanel.GetComponent<Animator>().Play("PanelIn");
		currentPanel = settingsPanel;
	}

	public void FlowUp() {
		currentPanel.GetComponent<Animator>().Play("PanelOut");
	}
}
