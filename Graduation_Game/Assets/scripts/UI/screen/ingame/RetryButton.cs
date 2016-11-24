using Assets.scripts.UI.inventory;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
	public class RetryButton : MonoBehaviour {
		public void Retry() {
			PlayerPrefs.DeleteKey("hasVisited");
			//check if user has key to retry and if not update inventory
			int numKeys = Inventory.key.GetValue();
			if (numKeys > 0) {
				Inventory.key.SetValue(--numKeys);
				// reload level
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			} else {
				//TODO show popup letting user know 
				// update inventory
				Inventory.UpdateCount();
				//go to main menu
				SceneManager.LoadScene("MainMenuScene");
			}
		}
	}
}
