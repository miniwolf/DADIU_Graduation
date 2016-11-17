using Assets.scripts.UI.screen;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.scripts.UI.mainmenu {
	public class MainMenuScript : UIController {
		public string scene1Name;
		public string scene2Name;
		public string scene3Name;
		public string scene4Name;
		public string scene5Name;

		private Button lvl1, lvl2, lvl3, lvl4, lvl5;

		protected void Start() {
			lvl1 = GameObject.FindGameObjectWithTag(TagConstants.UI.LVL_1).GetComponent<Button>();
			lvl2 = GameObject.FindGameObjectWithTag(TagConstants.UI.LVL_2).GetComponent<Button>();
			lvl3 = GameObject.FindGameObjectWithTag(TagConstants.UI.LVL_3).GetComponent<Button>();
			lvl4 = GameObject.FindGameObjectWithTag(TagConstants.UI.LVL_4).GetComponent<Button>();
			lvl5 = GameObject.FindGameObjectWithTag(TagConstants.UI.LVL_5).GetComponent<Button>();

			lvl1.onClick.AddListener(() => LoadLevel(scene1Name));
			lvl2.onClick.AddListener(() => LoadLevel(scene2Name));
			lvl3.onClick.AddListener(() => LoadLevel(scene3Name));
			lvl4.onClick.AddListener(() => LoadLevel(scene4Name));
			lvl5.onClick.AddListener(() => LoadLevel(scene5Name));
		}

		private void LoadLevel(string sceneName) {
			SceneManager.LoadScene(sceneName);
		}
	}
}