using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadEggHatchScene : MonoBehaviour {
	public void LoadEggScene(){
		SceneManager.LoadSceneAsync("EggScene");
	}
}
