using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenShop : MonoBehaviour {
	public void OpenStore(){
		SceneManager.LoadSceneAsync("Store");
	}
}
