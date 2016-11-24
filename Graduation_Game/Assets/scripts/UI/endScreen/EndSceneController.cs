using UnityEngine;
using System.Collections;
using Assets.scripts.UI.inventory;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEditor;
using System;
using System.IO;

public class EndSceneController: MonoBehaviour {

	public void GoToShop() {
		SceneManager.LoadScene("Store");
	}
	public void Retry() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void BackToMenu() {
		SceneManager.LoadScene("MainMenuScene");
	}
	public void GoNextLevel() {
		string nextLevel = Regex.Replace(SceneManager.GetActiveScene().name, @"[\d-]", string.Empty) + (int.Parse(Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value) + 1).ToString();
		try {
			SceneManager.LoadScene(nextLevel);
		}
		catch {
			SceneManager.LoadScene("MainMenuScene");
		}
	}
}
