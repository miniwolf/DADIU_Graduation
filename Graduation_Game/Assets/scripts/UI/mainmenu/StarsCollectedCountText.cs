using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.mainmenu {
	public class StarsCollectedCountText : MonoBehaviour {
		private MainMenuScript.LvlData[] levels;

		public static int totalStars;

		void Awake() {
			SetAllWonStars();
			var text = GetComponent<Text>();
			text.text = Prefs.GetTotalStars().ToString();
		}

		void SetAllWonStars() {
			totalStars = 0;
			levels = GetComponentInParent<MainMenuScript>().levels;
			for (int i = 0; i < levels.Length; i++) {
				totalStars += Prefs.GetLevelWonStars(levels[i].sceneFileName);
			}
			Prefs.SetTotalStars(totalStars);
		}
	}
}