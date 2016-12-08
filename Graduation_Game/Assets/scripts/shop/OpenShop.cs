using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.scripts.camera;

public class OpenShop : MonoBehaviour {
	private GameObject settingsPanel;
	private GameObject shopPanel;
	private GameObject currentPanel;
	private MainCameraFreeMove cameraMove;

	private void Start() {
		settingsPanel = transform.GetChild(0).gameObject;
		shopPanel = transform.GetChild(1).gameObject;
		cameraMove = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<MainCameraFreeMove>();
	}

	public void OpenStore() {
		shopPanel.GetComponent<Animator>().Play("PanelIn");
		currentPanel = shopPanel;
		cameraMove.popUpOn = true;
	}

	public void OpenSettings() {
		settingsPanel.GetComponent<Animator>().Play("PanelIn");
		currentPanel = settingsPanel;
		cameraMove.popUpOn = true;
	}

	public void FlowUp() {
		currentPanel.GetComponent<Animator>().Play("PanelOut");
		cameraMove.popUpOn = false;
	}
}
