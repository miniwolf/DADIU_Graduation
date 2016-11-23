using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour {
	public void LoadMainMenu(){
		SceneManager.LoadSceneAsync("MainMenuScene");
	}
}
