using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	public float timeForSplashScreen = 3f;
	public string nextLevel;

	// Use this for initialization
	void Start () {
		StartCoroutine(Next());
	}

	private IEnumerator Next(){
		yield return new WaitForSeconds(timeForSplashScreen);
		SceneManager.LoadScene(nextLevel);
	}
}
