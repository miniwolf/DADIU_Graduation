using Assets.scripts.components;
using UnityEngine;
using System.Collections;
using Assets.scripts.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.game {
	public class CutScene : Action {

		private readonly CouroutineDelegateHandler delegator;
		private GameObject gameObject;
		private CutSceneController cutSceneObject;

		public CutScene(CouroutineDelegateHandler d) {
			delegator = d;
		}
		public void Setup(GameObject gameObject) {
			this.gameObject = gameObject;
			cutSceneObject = gameObject.GetComponentInChildren<CutSceneController>();
		}

		public void Execute() {
			if(!cutSceneObject.GetDisplayCutScene()) {
				return;
			}
			SceneManager.LoadScene("CutScene");
		}

		private IEnumerator TriggerSpawn(float time) {
			yield return new WaitForSeconds(time);
			GameObject.FindGameObjectWithTag(TagConstants.CANVAS).GetComponent<CanvasController>().EndLevel();
			yield return null;
		}
	}
}
