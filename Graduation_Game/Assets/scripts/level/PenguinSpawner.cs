using System.Collections;
using Assets.scripts.components.registers;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.Threading;

namespace Assets.scripts.level {
	public class PenguinSpawner : MonoBehaviour {
		[Tooltip("How many penguins should spawn")]
		public int penguinCount = 3;

		[Tooltip("Time between penguins")]
		public float countTime = 3;

		[Tooltip("Time for the player to get ready")]
		public int countDownTime = 3000;

		private GameObject penguinObject;
		private Text penguinCounter;

		public void Start() {
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
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
			yield return StartCoroutine(SpawnRest());
		}

		private IEnumerator SpawnRest() {
			yield return new WaitForSeconds(0.1f);
			Time.timeScale = 0.0f;
			Thread.Sleep(countDownTime);
			Time.timeScale = 1.0f;
			while ( penguinCount > 0 ) {
				yield return new WaitForSeconds(countTime);
				SpawnPenguin();
			}
		}

		void SpawnPenguin() {
			// Create an instance of the penguin at the objects position
			var go = (GameObject)Instantiate(penguinObject, transform.position, Quaternion.identity);
			penguinCounter.text = (int.Parse(penguinCounter.text) + 1).ToString();
			go.SetActive(true);
			go.tag = TagConstants.PENGUIN;
			InjectionRegister.Redo();
			penguinCount--;
		}
	}
}
