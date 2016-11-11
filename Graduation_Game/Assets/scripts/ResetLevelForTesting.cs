using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetLevelForTesting : MonoBehaviour {
	public void ResetLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
    public void LevelPicker(){
        SceneManager.LoadScene("MainMenuScene");
    }
}
