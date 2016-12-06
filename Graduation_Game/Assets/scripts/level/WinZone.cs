using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.controllers;
using Assets.scripts.components;
using Assets.scripts.UI;
using Assets.scripts.UI.inventory;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using Assets.scripts.camera;

namespace Assets.scripts.level {
	public class WinZone : MonoBehaviour {
		private int penguins; //num of penguins in win zone
		private int alivePenguins;
		private CanvasController canvas; 
		private Text penguinCounter;

		public float cameraGoToWin = 10f, offSetTheWinZoneMoves = 50f, endSequenceSpeed = 3f, platformOnEndScreenOffset = 4.5f;

		private string levelName;
		private bool win;
		private CutSceneController cutSceneController;

		void Start() {
			levelName = SceneManager.GetActiveScene().name;
			penguins = 0;
			canvas = GameObject.FindGameObjectWithTag(TagConstants.CANVAS).GetComponent<CanvasController>();
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			cutSceneController = GameObject.FindGameObjectWithTag(TagConstants.CUTSCENE).GetComponent<CutSceneController>();
		}

		void Update() {
			if ( !win ) {
				alivePenguins = int.Parse(penguinCounter.text);
				
				if(penguins == alivePenguins) {
					switch (levelName) {
						case PrefsConstants.LEVEL1:
							SetPrefs(1);
							break;
						case PrefsConstants.LEVEL2:
							SetPrefs(2);
							break;
						case PrefsConstants.LEVEL3:
							SetPrefs(3);
							break;
						case PrefsConstants.LEVEL4:
							SetPrefs(4);
							break;
						case PrefsConstants.LEVEL5:
							SetPrefs(5);
							break;
						case PrefsConstants.LEVEL6:
							SetPrefs(6);
							break;
						case PrefsConstants.LEVEL7:
							SetPrefs(7);
							break;
						case PrefsConstants.LEVEL8:
							SetPrefs(8);
							break;
						case PrefsConstants.LEVEL9:
							SetPrefs(9);
							break;
						case PrefsConstants.LEVEL10:
							SetPrefs(10);
							break;
						case PrefsConstants.LEVEL11:
							SetPrefs(11);
							break;
					}
				}
			}
		}

		private void SetPrefs(int level) {
			//canvas.EndLevel();			// this thing will be called inside the cutscene after the required time has passed (look in controllers/actions/game/CutScene.cs )

			if(Prefs.GetLevelUnlockIndex() < level) Prefs.SetLevelUnlockIndex(level);
			//Inventory.UpdateCount();
			win = true;
			StartCoroutine(ForceCameraToWin());
			canvas.EndLevel();
			//GameObject.FindGameObjectWithTag(TagConstants.CUTSCENE).GetComponent<CutSceneController>().ShowCutScene();
		}

		void OnTriggerEnter(Collider collider) {
			if ( collider.transform.tag == TagConstants.PENGUIN ) {
				penguins++;
				collider.transform.parent = transform;
				collider.GetComponentInChildren<Animator>().speed = 1.0f;
			}
		}

		private IEnumerator ForceCameraToWin(){
			Transform[] path = new Transform[2];
			Camera cam = Camera.main;
			path = Camera.main.gameObject.GetComponent<MainCameraFreeMove>().cameraSteps;
			path[0].position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
			path[1].position = cam.transform.position;
			path[0].position = path[1].position;
			path[1].position = new Vector3(transform.position.x + platformOnEndScreenOffset, path[1].position.y, path[1].position.z);
			float startTime = Time.time;
			float speedFactor = cameraGoToWin;
			float journeyLength = Vector3.Distance(path[0].position, path[1].position);
			float distCovered = (Time.time - startTime)*speedFactor;
			float fracJourney = distCovered / journeyLength;
			//print(path[0] + " " + path[1]);
			while(fracJourney < 1f){
				distCovered = (Time.time - startTime)*speedFactor;
				fracJourney = distCovered / journeyLength;
				cam.transform.position = Vector3.Lerp(path[0].position, path[1].position, fracJourney);
				//cam.transform.position = Vector3.Lerp(startPosCam, moveTo, fracJourney+0.15f);
				yield return new WaitForEndOfFrame();
			}
			StartCoroutine(EndSequence());
		}

		private IEnumerator EndSequence(){
			Vector3[] path = new Vector3[2];
			Camera cam = Camera.main;
			path[0] = transform.position;
			path[1] = new Vector3(transform.position.x + offSetTheWinZoneMoves, transform.position.y, transform.position.z);
			float startTime = Time.time;
			float speedFactor = endSequenceSpeed;
			float journeyLength = Vector3.Distance(path[0], path[1]);
			float distCovered = (Time.time - startTime)*speedFactor;
			float fracJourney = distCovered / journeyLength;
			//print(path[0] + " " + path[1]);
			while(fracJourney < 1f){
				distCovered = (Time.time - startTime)*speedFactor;
				fracJourney = distCovered / journeyLength;
				transform.position = Vector3.Lerp(path[0], path[1], fracJourney);
				cam.transform.position = Vector3.Lerp(new Vector3(path[0].x + platformOnEndScreenOffset, cam.transform.position.y, cam.transform.position.z), new Vector3(path[1].x + platformOnEndScreenOffset, cam.transform.position.y, cam.transform.position.z), fracJourney);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
