using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.mainmenu {
	public class StarsCollectedCountText : MonoBehaviour {
		private MainMenuScript.LvlData[] levels;

		private int totalStars;

		void Start() {
			SetAllWonStars();
			var text = GetComponent<Text>();
			text.text = Prefs.GetTotalStars().ToString();
		}

		void SetAllWonStars() {
			levels = GetComponentInParent<MainMenuScript>().levels;
			for (int i = 0; i < levels.Length; i++) {
				totalStars += Prefs.GetLevelWonStars(levels[i].sceneFileName);
			}
			Prefs.SetTotalStars(totalStars);
		}
	}
}