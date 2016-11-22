using UnityEngine;
using System.Collections;
using Assets.scripts;
using UnityEngine.SceneManagement;

public class ReturnFromSecretLevel : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if (other.tag != TagConstants.PENGUIN) {
			SceneManager.LoadScene(PlayerPrefs.GetString("thisCurrLvl"));
		}
	}
}
