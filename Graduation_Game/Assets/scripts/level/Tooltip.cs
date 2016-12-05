using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.gamestate;
using System.Collections;
using Assets.scripts.UI.translations;
using Assets.scripts.components;
using Assets.scripts.controllers;
using Assets.scripts.character;
using System.Collections.Generic;

namespace Assets.scripts.level {
	public class Tooltip : ActionableGameEntityImpl<ControllableActions>  {
		public enum Type {Place, Move, Remove, Newtool};
		public enum TypeTool {Jump, Switchlane};

		[Tooltip("type of tooltip: place, move, remove or newtool")]
		public Type type = Type.Place;
		[Tooltip("type of tool for newtool tooltip: jump or switchlane")]
		public TypeTool typeNewtool = TypeTool.Jump;

		private GameObject panel;
		private bool isPlaced;
		private bool isMoved;
		private bool isRemoved;
		private bool isNewtool;
		private bool active;
		private List<PenguinSpawner> penguinSpawner = new List<PenguinSpawner>();
		private List<GameObject> penguins = new List<GameObject>();

		void Start() {
			//Prefs.SetTooltips(1); // ONLY FOR TESTING
			if ( !Prefs.IsTooltipsOn() ) {
				gameObject.SetActive(false);
			}
			active = true;

			GameObject[] penguinSpawners = GameObject.FindGameObjectsWithTag(TagConstants.PENGUIN_SPAWNER);
			for( int i=0; i < penguinSpawners.Length; i++ ) {
				penguinSpawner.Add(penguinSpawners[i].GetComponent<PenguinSpawner>());
			}
		}

		void Update() {
			//check if the user finished with the tooltip
			if ( active && (isPlaced || isMoved || isRemoved || isNewtool )) {
				UnFreeze();
			}
		}

		void OnTriggerEnter(Collider collider) {
			if ( active && collider.transform.tag == TagConstants.PENGUIN ) {
				StopPenguins(); 
				TooltipAction();
			}
		}

		private void TooltipAction() {
			panel.SetActive(true);
			switch(type) {
				case Type.Place:
					panel.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.place);
				break;
				case Type.Move:
					panel.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.move);
					break;
				case Type.Remove:
					panel.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.remove);
					break;
				case Type.Newtool:
					switch ( typeNewtool ) {
						case TypeTool.Jump: 
							panel.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.jump);
							break;
						case TypeTool.Switchlane: 
							panel.GetComponentInChildren<Text>().text = TranslateApi.GetString(LocalizedString.switchlane);
							break;
					}

					break;
			}
		}
			

		public Type GetType() {
			return type;	
		}

		public override string GetTag() {
			return TagConstants.TOOLTIP;
		}

		public void SetPanel(GameObject panel) {
			this.panel = panel;
		}

		private void StopPenguins(){
			for (int u = 0; u < penguinSpawner.Count; u++) {
				penguins = penguinSpawner[u].GetAllPenguins();
				for (int i = 0; i < penguins.Count; i++) {
					penguins[i].GetComponent<Penguin>().ExecuteAction(Assets.scripts.controllers.ControllableActions.Stop);
				}
			}
		}

		private void StartPenguins(){
			for (int u = 0; u < penguinSpawner.Count; u++) {
				penguins = penguinSpawner[u].GetAllPenguins();
				for (int i = 0; i < penguins.Count; i++) {
					penguins[i].GetComponent<Penguin>().ExecuteAction(Assets.scripts.controllers.ControllableActions.Start);
				}
			}
		}

		private void UnFreeze() {
			active = false; 
			StartPenguins();
			panel.SetActive(false);
		}

		public void SetPlace(bool place) {
			isPlaced = place;
		}

		public bool IsPlaced() {
			return isPlaced;
		}

		public void SetMove(bool move) {
			isMoved = move;
		}

		public bool IsMoved() {
			return isMoved;
		}

		public void SetRemove(bool remove) {
			isRemoved = remove;
		}

		public void SetNewtool(bool newtool) {
			isNewtool = newtool;
		}

		public bool IsNewtool() {
			return isNewtool;
		}

		public bool IsRemoved() {
			return isRemoved;
		}

		public bool IsActive() {
			return active;
		}
	}
}

