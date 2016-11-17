using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components.registers;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading;
using Assets.scripts.gamestate;

namespace Assets.scripts.level {
	public class PenguinSpawner : MonoBehaviour {
		[Tooltip("How many penguins should spawn")]
		public int penguinCount = 3;

		[Tooltip("Time between penguins")]
		public float countTime = 3;

		[Tooltip("Time for the player to get ready")]
		public int waitTimeMilisecondsInterval = 1000; //the waiting time would be waitTimeMilisecondsInterval * numIntervals
		public int numIntervals = 3;

		private GameObject penguinObject;
		private Text countDown;
		private Text penguinCounter;
	    private GameStateManager gameStateManager;
		private List<GameObject> penguins = new List<GameObject>();
		private int count;

		public void Start() {
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			countDown = GameObject.FindGameObjectWithTag(TagConstants.COUNT_DOWN_TEXT).GetComponent<Text>();
		    gameStateManager = FindObjectOfType<GameStateManager>();
			count = penguinCount;

			for ( var i = 0; i < transform.childCount; i++ ) {
				var child = transform.GetChild(i);
				if ( child.tag != TagConstants.PENGUIN_TEMPLATE ) {
					continue;
				}
				penguinObject = child.gameObject;
				break;
			}
			StartCoroutine(Spawn());
		}

		private IEnumerator Spawn() {
			// spawn first penguin and freeze time
			SpawnPenguin();
			count--;
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

			while ( count > 0 ) {
				yield return new WaitForSeconds(countTime);
			    if (!gameStateManager.IsGameFrozen()) {
			        SpawnPenguin();
					count--;
			    }
			}
		}

		public void SpawnPenguin() {
			// Create an instance of the penguin at the objects position
			var go = (GameObject)Instantiate(penguinObject, transform.position, Quaternion.identity);
		    penguins.Add(go);
		    penguinCounter.text = (int.Parse(penguinCounter.text) + 1).ToString();
			go.SetActive(true);
			go.tag = TagConstants.PENGUIN;
			InjectionRegister.Redo();
		}

		public List<GameObject> GetAllPenguins(){
			return penguins;
		}

		public int GetInitialPenguinCount() {
			return penguinCount;
		}
	}

}
