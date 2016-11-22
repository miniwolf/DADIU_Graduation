using UnityEngine;
using System.Collections;
using Assets.scripts;
using Assets.scripts.level;
using System.Collections.Generic;
using Assets.scripts.character;
using UnityEngine.SceneManagement;

public class SecretLevelPortal : MonoBehaviour {

	private PenguinSpawner pSpawner;
	private List<GameObject> penguins = new List<GameObject>();
	public string secretLevelLoad;

	// Use this for initialization
	void Start () {
		pSpawner = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_SPAWNER).GetComponent<PenguinSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag != TagConstants.PENGUIN || PlayerPrefs.GetInt("hasVisited")==1) {
			return;
		}
		PlayerPrefs.SetInt("hasVisited", 1);
		PlayerPrefs.SetInt("backFromSecret", 1);
		PlayerPrefs.SetString("thisCurrLvl", SceneManager.GetActiveScene().name);
		penguins = pSpawner.GetAllPenguins();
		SavePosOfPenguins();
		SceneManager.LoadScene(secretLevelLoad);
	}


	void SavePosOfPenguins(){
		for (int i = 0; i < penguins.Count; i++) {
			if (!penguins[i].GetComponent<Penguin>().IsDead()) {
				PlayerPrefs.SetFloat("penguin_" + i + "_x", penguins[i].transform.position.x);
				PlayerPrefs.SetFloat("penguin_" + i + "_y", penguins[i].transform.position.y);
				PlayerPrefs.SetFloat("penguin_" + i + "_z", penguins[i].transform.position.z);
			}
		}
	}


}
