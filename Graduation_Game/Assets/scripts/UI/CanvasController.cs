using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.controllers;
using Assets.scripts.components;
using Assets.scripts.sound;
using Assets.scripts.UI.inventory;

namespace Assets.scripts.UI {
	public class CanvasController : ActionableGameEntityImpl<GameActions> {
		public float timeBeforeStarSpawn;
		public float timeBeforeScoreFlow;
		public float timeBewteenStarSpawn = 2f;
		public float scoreFlowScalingFactor;

		public int timeForRetry;
		// Use this for initialization
		public int penguinsRequiredFor3Stars;
		public int penguinsRequiredFor2Stars;
		public int penguinsRequiredFor1Stars;
		private Text penguinCounter;
		private bool over; //game over (0 penguins alive)
		private bool endLevel; //game finished
		private Button retryCircle;
		private Image retryCircleImage;
		private Button retryButton; 
		private Image retryPrize;
		private GameObject gameOverPanel;
		private bool retryIsLive = false;
		private Text plutoniumCounter, plutoniumThisLevel, plutoniumTotal;
	
		void Awake() {
			base.Awake();
			gameOverPanel = GameObject.FindGameObjectWithTag(TagConstants.UI.GAME_OVER_PANEL);
			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
			plutoniumThisLevel = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_THIS_LEVEL).GetComponent<Text>();
			plutoniumTotal = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_TOTAL).GetComponent<Text>();
		}

		void Start() {
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			// retry button/images
			retryCircle = GameObject.FindGameObjectWithTag(TagConstants.UI.RETRY_CIRCLE).GetComponent<Button>();
			retryCircleImage = GameObject.FindGameObjectWithTag(TagConstants.UI.RETRY_CIRCLE).GetComponent<Image>();
			retryButton = GameObject.FindGameObjectWithTag(TagConstants.UI.RETRY_BUTTON).GetComponent<Button>();
			retryPrize = GameObject.FindGameObjectWithTag(TagConstants.UI.RETRY_PRIZE).GetComponent<Image>();
			// not enabled during game
			//gameOverPanel.SetActive(true);
			DisableRetry();
			

		}

		void Update () {
			if(int.Parse(penguinCounter.text) < 0 && !over) {
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
		}		

		public override string GetTag() {
			return TagConstants.CANVAS;
		}

		public void EndLevel() {
			PlayerPrefs.DeleteKey("hasVisited");
			ExecuteAction(GameActions.EndLevel);
		}

		private void DisableRetry() {
			retryIsLive = false;
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

		public int[] GetAmountOfPenguinsForStars(){
			int[] temp = new int[3];
			temp[0] = penguinsRequiredFor1Stars;
			temp[1] = penguinsRequiredFor2Stars;
			temp[2] = penguinsRequiredFor3Stars;
			return temp;
		}

		private void EnableRetry() {
			retryIsLive = true;
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

		public Text GetPlutoniumCounter(){
			return plutoniumCounter;
		}

		public Text GetPlutoniumTotal(){
			return plutoniumTotal;
		}

		public Text GetPlutoniumThisLevel(){
			return plutoniumThisLevel;
		}
	}
}
