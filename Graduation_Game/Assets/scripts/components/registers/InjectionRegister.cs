using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.components.factory;
using Assets.scripts.controllers;
using Assets.scripts.gamestate;
using Assets.scripts.traps;
using Assets.scripts.UI;
using Assets.scripts.UI.screen.ingame;
using Assets.scripts.level;

namespace Assets.scripts.components.registers {
	public class InjectionRegister : MonoBehaviour {
		private static readonly List<GameEntity> components = new List<GameEntity>();
		private static bool finished;
		private static GameObject levelSettings;
		private static CouroutineDelegateHandler handler;
		private static SnappingToolInterface snap;
		private static InputManager inputManager;
		private static GameStateManager gameStateManager;

		protected void Awake() {
			snap = new SnappingTool();
			levelSettings = GameObject.FindGameObjectWithTag(TagConstants.LEVELSETTINGS);
			handler = gameObject.GetComponentInChildren<CouroutineDelegateHandler>();
			inputManager = GetComponent<InputManager>();
			gameStateManager = GetComponent<GameStateManager>();
		}

		protected void Start() {
			InitializeComponents();
			components.Clear();
			finished = true;
		}

		protected void OnDestroy() {
			components.Clear();
		}

		public static void Register(GameEntity component) {
			components.Add(component);
		}

		private static void InitializeComponents() {
			foreach(var component in components) {
				InitializeComponent(component);
				component.SetupComponents();
			}
		}

		private static void InitializeComponent(GameEntity component) {
			switch(component.GetTag()) {
			case TagConstants.PENGUIN:
				new PlayerFactory(component.GetActionable<ControllableActions>(), component.GetGameObject(), levelSettings, gameStateManager).Build();
				break;
			case TagConstants.PLUTONIUM_PICKUP:
				new PickupFactory(component.GetActionable<PickupActions>()).Build();
				break;
			case TagConstants.PRESSURE_PLATE:
				new PressurePlateFactory(component.GetActionable<PressurePlateActions>()).BuildActionOnLinkingObject((LinkingComponent)component);
				break;
			case TagConstants.WIRE:
				TrapFactory.BuildWire(component.GetActionable<TrapActions>(), component.GetGameObject().GetComponent<Wire>(), handler);
				break;
			//case TagConstants.SNAPPING:
			//	
			//	break;
			case TagConstants.WEIGHTBASED:
				TrapFactory.BuildWeightBasedTrap(component.GetActionable<TrapActions>(), component.GetGameObject());
				break;
			case TagConstants.CANVAS:
				new GameFactory(component.GetActionable<GameActions>()).BuildCanvas(handler);
				break;
			case TagConstants.STAR1: case TagConstants.STAR2: case TagConstants.STAR3:
				new GameFactory(component.GetActionable<GameActions>()).BuildStar();
				break;
			case TagConstants.TOOLBUTTON:
				snap.SetCenter(levelSettings.GetComponent<LevelSettings>().GetSceneCenter());
				component.GetGameObject().GetComponent<SetSnappingTool>().SetSnap(snap);
				component.GetGameObject().GetComponent<SetSnappingTool>().SetInputManager(inputManager);
				component.GetGameObject().GetComponent<GameFrozenChecker>().SetGameStateManager(gameStateManager);
				break;
			default:
				throw new NotImplementedException("Tag has no specific behaviour yet: <" + component.GetTag() + "> this does maybe not need to be registered");
			}
		}

		public static void Redo() {
			if(!finished) {
				return;
			}
			InitializeComponents();
			components.Clear();
		}
	}
}
