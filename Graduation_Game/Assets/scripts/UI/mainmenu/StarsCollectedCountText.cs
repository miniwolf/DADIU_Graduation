using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.scripts.UI.mainmenu {
	public class StarsCollectedCountText : MonoBehaviour {

		protected void Start() {
			var text = GetComponent<Text>();
			text.text = Prefs.GetTotalStars().ToString();
		}
	}
}