using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.UI.inventory;
using UnityEngine.SceneManagement;

namespace Assets.scripts.UI{
	public class GameOverPanel: MonoBehaviour {

		void Start() {
			// disable panel
			gameObject.SetActive(false);
			transform.localScale = Vector3.zero;
		}
		public void Finish() {
			Inventory.UpdateCount();
			SceneManager.LoadScene("MainMenuScene");
		}
	}
}

