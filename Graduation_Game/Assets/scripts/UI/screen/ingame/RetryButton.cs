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
				//gameObject.SetActive(false);
				print("KEY SHOULD BE BOUGHT NOW!!!!");
				//TODO Should pay for a new key? Should ask if they want to buy a key and use it
				//TODO show popup letting user know 
				// update inventory
			//	Inventory.UpdateCount();
				//go to main menu
			//	SceneManager.LoadScene("MainMenuScene");
			}
		}
	}
}
