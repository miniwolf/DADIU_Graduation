using UnityEngine;
using System.Collections;
using Assets.scripts;
using UnityEngine.SceneManagement;

public class ReturnFromSecretLevel : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if (other.tag != TagConstants.PENGUIN) {
			return;
		}
		PlayerPrefs.SetInt("backFromSecret", 1);
		SceneManager.LoadSceneAsync(PlayerPrefs.GetString("thisCurrLvl"));
	}
}
