using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.controllers;
using Assets.scripts.components;
using Assets.scripts.sound;
using Assets.scripts.UI.inventory;
using System.Collections;
using Assets.scripts.level;
using Assets.scripts.UI.input;

namespace Assets.scripts.UI {
	public class CanvasController : ActionableGameEntityImpl<GameActions>, TouchInputListener, MouseInputListener {
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
		[HideInInspector]
		public GameObject gameOverPanel, clickBlocker, failSceneObject, endSceneObject;
		private GameObject[] holderThisLevel, holderTotal;
		private Text plutoniumCounter;
		private Text[] plutoniumThisLevel, plutoniumTotal;
		public Sprite penguinIsDead, key;
		private InputManager inputManager;
		private GameObject[] tooltips;

		void Awake() {
			base.Awake();
			gameOverPanel = GameObject.FindGameObjectWithTag(TagConstants.UI.GAME_OVER_PANEL);
			plutoniumCounter = GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
			holderThisLevel = GameObject.FindGameObjectsWithTag(TagConstants.PLUTONIUM_THIS_LEVEL);
			holderTotal = GameObject.FindGameObjectsWithTag(TagConstants.PLUTONIUM_TOTAL);

			AssignTotalPlutonium(holderTotal);
			AssignPlutoniumThisLevel(holderThisLevel);

			clickBlocker = GameObject.FindGameObjectWithTag(TagConstants.UI.BLOCKCLICKS);
			failSceneObject = GameObject.FindGameObjectWithTag(TagConstants.UI.FAILSCENEOBJECT);
			endSceneObject = GameObject.FindGameObjectWithTag(TagConstants.UI.ENDSCENEOBJECT);
		}

		void Start() {
			penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			inputManager = FindObjectOfType<InputManagerImpl>(); // not registering in injection system yet
			inputManager.SubscribeForMouse(this);
			inputManager.SubscribeForTouch(this);
			tooltips = GameObject.FindGameObjectsWithTag(TagConstants.TOOLTIP);
		}

		void Update () {
			if(Input.GetKeyDown(KeyCode.U)){
				ExecuteAction(GameActions.EndLevel);
			}
			if(Input.GetKeyDown(KeyCode.A)){
				ExecuteAction(GameActions.EndLevelLoss);
			}

			if(int.Parse(penguinCounter.text) <= 0 && !over) {
				print("hej");
				ExecuteAction(GameActions.EndLevelLoss);
				over = true;
			}
		}

		private void AssignTotalPlutonium(GameObject[] holder){
			plutoniumTotal = new Text[holder.Length];
			for (int i = 0; i < plutoniumTotal.Length; i++) {
				plutoniumTotal[i] = holder[i].GetComponent<Text>();
				plutoniumTotal[i].text = PlayerPrefs.GetInt("Plutonium").ToString();
			}
		}

		private void AssignPlutoniumThisLevel(GameObject[] holder){
			plutoniumThisLevel = new Text[holder.Length];
			for (int i = 0; i < plutoniumTotal.Length; i++) {
				plutoniumThisLevel[i] = holder[i].GetComponent<Text>();
			}
		}



		public override string GetTag() {
			return TagConstants.CANVAS;
		}

		public void EndLevel() {
			PlayerPrefs.DeleteKey("hasVisited");
			ExecuteAction(GameActions.EndLevel);
		}

		public int[] GetAmountOfPenguinsForStars(){
			int[] temp = new int[3];
			temp[0] = penguinsRequiredFor1Stars;
			temp[1] = penguinsRequiredFor2Stars;
			temp[2] = penguinsRequiredFor3Stars;
			return temp;
		}

		/// <summary>
		/// Should only be used during end screen
		/// </summary>
		/// <returns>The survived penguins. Should only be used during end screen</returns>
		public int GetSurvivedPenguins(){
			return int.Parse(penguinCounter.text);
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

		public Text[] GetPlutoniumTotal(){
			return plutoniumTotal;
		}

		public Text[] GetPlutoniumThisLevel(){
			return plutoniumThisLevel;
		}

		public bool GetActiveClickBlocker(){
			return clickBlocker.activeInHierarchy;
		}
		public void SetActiveClickBlocker(bool active){
			clickBlocker.SetActive(active);
		}
			
		public void CheckTooltips() {
			foreach ( GameObject t in tooltips ) {
				Tooltip tooltip = t.GetComponent<Tooltip>();
				if ( tooltip.GetType() == Tooltip.Type.Newtool && tooltip.IsActive()) {
					tooltip.SetNewtool(true);
					return;
				}
			}
		}

		public void OnTouch(Touch[] allTouches) {
			CheckTooltips();
		}

		public void OnMouseRightDown() {
			CheckTooltips();
		}

		public void OnMouseRightUp() {
		}

		public void OnMouseRightPressed() {
		}

		public void OnMouseLeftDown() {
			CheckTooltips();
		}

		public void OnMouseLeftUp() {
		}

		public void OnMouseLeftPressed() {
		}

	}
}
