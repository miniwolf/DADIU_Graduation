using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components.registers;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading;
using Assets.scripts.gamestate;
using Assets.scripts.sound;
using Assets.scripts.character;
using Assets.scripts.camera;

namespace Assets.scripts.level {
	public class PenguinSpawner : MonoBehaviour {
		[Tooltip("How many penguins should spawn")]
		public int penguinCount = 3;

		[Tooltip("Time between penguins")]
		public float countTime = 3;

		[Tooltip("Time for the player to get ready")]
		public int waitTimeMilisecondsInterval = 1000; //the waiting time would be waitTimeMilisecondsInterval * numIntervals
		public int numIntervals = 3;

		public float speedUpFactor = 1.5f;

		public float speedForPlatform = 3f, cameraPanSpeed = 5f;

		private GameObject penguinObject;
		private Text countDown;
		private Text penguinCounter;
	    private GameStateManager gameStateManager;
		private List<GameObject> penguins = new List<GameObject>();
		private int count;
		private GameObject entrancePlatform;
		private Vector3 origPos;
		private int spawned = 0, layerMask = 1 << 8;

		public void Start() {
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			countDown = GameObject.FindGameObjectWithTag(TagConstants.COUNT_DOWN_TEXT).GetComponent<Text>();
		    gameStateManager = FindObjectOfType<GameStateManager>();
			entrancePlatform = GameObject.FindGameObjectWithTag("EntrancePlatform");
			count = penguinCount;


			for ( var i = 0; i < transform.childCount; i++ ) {
				var child = transform.GetChild(i);
				if ( child.tag != TagConstants.PENGUIN_TEMPLATE ) {
					continue;
				}
				penguinObject = child.gameObject;
				break;
			}
			if (PlayerPrefs.GetInt("backFromSecret") == 1) {
				//GetPreviousPositions();
			} else {
				for (int i = 0; i < count; i++) {
					SpawnPenguin();
				}
			}
			StartCoroutine(CameraPan());
		}


		private IEnumerator CameraPan(){
			Transform[] path = new Transform[2];
			path = Camera.main.gameObject.GetComponent<MainCameraFreeMove>().cameraSteps;
			path[0].position = new Vector3(path[0].transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
			path[1].position = new Vector3(path[1].transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
			Camera.main.transform.position = path[1].position;
			float startTime = Time.time;
			float speedFactor = cameraPanSpeed;
			float journeyLength = Vector3.Distance(path[1].position, path[0].position);
			float distCovered = (Time.time - startTime)*speedFactor;
			float fracJourney = distCovered / journeyLength;
			//print(path[0] + " " + path[1]);
			yield return new WaitForSeconds(0.5f);
			while(fracJourney < 1f){
				distCovered = (Time.time - startTime)*speedFactor;
				fracJourney = distCovered / journeyLength;
				Camera.main.transform.position = Vector3.Lerp(path[1].position, path[0].position, fracJourney);
				//cam.transform.position = Vector3.Lerp(startPosCam, moveTo, fracJourney+0.15f);
				yield return new WaitForEndOfFrame();
			}
			StartCoroutine(PlatformComeIn());
		}



		private IEnumerator PlatformComeIn(){
			RaycastHit hit;
			if(!Physics.Raycast(transform.position,new Vector3(1,0,0),out hit,50f, layerMask)){
				UnityEngine.Debug.LogError("The platform does not know where to move, because it cannot find where to go, put something with the levellayer on it, where it should stop");
				yield return null;
			}
			Vector3 whereToGo = hit.point, origPos = transform.position;
			float startTime = Time.time;;
			float speedFactor = speedForPlatform;
			float journeyLength = Vector3.Distance(origPos, whereToGo);
			float distCovered = (Time.time - startTime)*speedFactor;
			float fracJourney = distCovered / journeyLength;
			//print(path[0] + " " + path[1]);
			while(fracJourney < 0.85f){
				distCovered = (Time.time - startTime)*speedFactor;
				fracJourney = distCovered / journeyLength;
				transform.position = Vector3.Lerp(origPos, whereToGo, fracJourney);
				//cam.transform.position = Vector3.Lerp(startPosCam, moveTo, fracJourney+0.15f);
				yield return new WaitForEndOfFrame();
			}
			StartCoroutine(Spawn());
		}



		private IEnumerator Spawn() {

			yield return StartCoroutine(FreezeAndSpawnRest());
		}

		private IEnumerator FreezeAndSpawnRest() {

		    int counter = numIntervals;
			do {
				// print in text UI
				countDown.text = counter.ToString();
				Time.timeScale = 0.000001f;
				yield return new WaitForSeconds(1f * Time.timeScale);
				Time.timeScale = 1.0f;
			} while ( --counter > 0 );
			countDown.enabled = false;

			/*while ( count > 0 ) {
				yield return new WaitForSeconds(countTime);
			    if (!gameStateManager.IsGameFrozen()) {

					count--;
			    }
			}*/
			StartCoroutine(EnableThePenguins());
		}

		private IEnumerator EnableThePenguins(){
			EnableAPenguin();
			count--;
			while (count > 0) {
				yield return new WaitForSeconds(countTime);
				if (!gameStateManager.IsGameFrozen()) {
					EnableAPenguin();
					count--;
				}
			}
		}

		private void EnableAPenguin(){
			penguins[spawned - count].GetComponent<Penguin>().ExecuteAction(Assets.scripts.controllers.ControllableActions.Start);
			penguins[spawned - count].transform.parent = null;
		}

		public void SpawnPenguin() {
			// Create an instance of the penguin at the objects position
			var go = (GameObject)Instantiate(penguinObject, new Vector3(transform.position.x - 1*spawned, transform.position.y, transform.position.z), Quaternion.identity);
			go.transform.parent = transform;
			penguins.Add(go);
		    penguinCounter.text = (int.Parse(penguinCounter.text) + 1).ToString();
			go.SetActive(true);
			go.tag = TagConstants.PENGUIN;
			InjectionRegister.Redo();
			go.GetComponent<Penguin>().ExecuteAction(Assets.scripts.controllers.ControllableActions.Stop);
		    AkSoundEngine.PostEvent(SoundConstants.PenguinSounds.SPAWN, penguinObject);
			spawned++;
		}

		public void SpawnPenguin(Vector3 pos) {
			// Create an instance of the penguin at the objects position
			var go = (GameObject)Instantiate(penguinObject, pos, Quaternion.identity);
			penguins.Add(go);
			penguinCounter.text = (int.Parse(penguinCounter.text) + 1).ToString();
			go.SetActive(true);
			go.tag = TagConstants.PENGUIN;
			InjectionRegister.Redo();
			AkSoundEngine.PostEvent(SoundConstants.PenguinSounds.SPAWN, penguinObject);
		}

	/*	private void GetPreviousPositions(){
			for (int i = 0; i < count; i++) {
				if (PlayerPrefs.GetFloat("penguin_" + i + "_x") != null) {
					Vector3 vec = new Vector3(PlayerPrefs.GetFloat("penguin_" + i + "_x"), PlayerPrefs.GetFloat("penguin_" + i + "_y"), PlayerPrefs.GetFloat("penguin_" + i + "_z"));
					SpawnPenguin(vec);
				}
			}
			PlayerPrefs.SetInt("backFromSecret", 0);
			countDown.enabled = false;
		}*/

		public List<GameObject> GetAllPenguins(){
			return penguins;
		}

		public int GetInitialPenguinCount() {
			return penguinCount;
		}

		public float GetSpeedUp(){
			return speedUpFactor;
		}



	}

}
