using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.controllers;
using Assets.scripts.components;
using Assets.scripts.sound;
using Assets.scripts.UI.inventory;
using UnityEngine.SceneManagement;

namespace Assets.scripts.UI {
	public class CanvasController : ActionableGameEntityImpl<GameActions> {
		public int timeForRetry;

		// Use this for initialization
		public int penguinsRequiredFor3Stars;
		public int penguinsRequiredFor2Stars;
		public int penguinsRequiredFor1Stars;
		private Text penguinCounter;
		private GameObject endScene;
		private bool over; //game over (0 penguins alive)
		private bool endLevel; //game finished
		private Button retryCircle;
		private Image retryCircleImage;
		private Button retryButton; 
		private Image retryPrize;
		private GameObject gameOverPanel;

<<<<<<< HEAD
	
		void Awake() {
			base.Awake();
			gameOverPanel = GameObject.FindGameObjectWithTag(TagConstants.UI.GAME_OVER_PANEL);
		}

=======
		void Awake() {
			gameOverPanel = GameObject.FindGameObjectWithTag (TagConstants.UI.GAME_OVER_PANEL);
		}
>>>>>>> develop

		void Start() {
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			// retry button/images
			retryCircle = GameObject.FindGameObjectWithTag(TagConstants.UI.RETRY_CIRCLE).GetComponent<Button>();
			retryCircleImage = GameObject.FindGameObjectWithTag(TagConstants.UI.RETRY_CIRCLE).GetComponent<Image>();
			retryButton = GameObject.FindGameObjectWithTag(TagConstants.UI.RETRY_BUTTON).GetComponent<Button>();
			retryPrize = GameObject.FindGameObjectWithTag(TagConstants.UI.RETRY_PRIZE).GetComponent<Image>();
			// not enabled during game
			DisableRetry();

			foreach (Transform g in gameObject.GetComponentsInChildren<Transform>(true))
				if (g.tag == TagConstants.ENDSCENE)
					endScene = g.gameObject;
		}

		void Update () {
			if(int.Parse(penguinCounter.text) < 1 && !over) {
				EnableRetry();
				EnableGameOverPanel();
				over = true;
			}

			// image being filled for timeForRetry seconds
			if ( over ) {
				retryCircleImage.fillAmount -= 1.0f/timeForRetry * Time.deltaTime;
			}

			// when it disappears, disable retry buttons
			if ( retryCircleImage.fillAmount == 0 ) {
				DisableRetry();
			}

			// if penguins reached the win zone (the ones alive) show stars
			if ( endLevel ) {
				ExecuteAction(GameActions.EndLevel);
			}
		}		

		public override string GetTag() {
			return TagConstants.CANVAS;
		}

		public void EndLevel() {
			endLevel = true;
		}

		private void DisableRetry() {
			retryCircle.enabled = false;
			retryButton.enabled = false;
			retryPrize.enabled = false;
			retryCircle.transform.localScale = Vector3.zero;
			retryButton.transform.localScale = Vector3.zero;
			retryPrize.transform.localScale = Vector3.zero;
			int numKeys = Inventory.key.GetValue();
			if ( numKeys > 0 ) {
				retryPrize.GetComponentInChildren<Text> ().text = "0";
			} else {
				// TODO take the prize from the store
				retryPrize.GetComponentInChildren<Text>().text = "10";
			}

		}

		private void EnableRetry() {
			retryCircle.enabled = true;
			retryButton.enabled = true;
			retryPrize.enabled = true;
			retryCircle.transform.localScale = Vector3.one;
			retryButton.transform.localScale = Vector3.one;
			retryPrize.transform.localScale = Vector3.one;
		}

		private void EnableGameOverPanel() {
			gameOverPanel.SetActive(true);
			gameOverPanel.transform.localScale = Vector3.one;
		}

	    public void SoundButtonClick() {
	        AkSoundEngine.PostEvent(SoundConstants.FeedbackSounds.BUTTON_PRESS, Camera.main.gameObject);
	    }
	}
}
