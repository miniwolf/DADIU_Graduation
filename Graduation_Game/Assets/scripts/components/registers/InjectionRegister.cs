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
		private static readonly Queue<GameEntity> components = new Queue<GameEntity>();
		private static bool finished;
		private static LevelSettings levelSettings;
		private static CouroutineDelegateHandler handler;
		private static SnappingToolInterface snap;
		private static InputManager inputManager;
		private static GameStateManager gameStateManager;
		private static PickupFactory pickupFactory;
		private static NotifierSystem notifierSystem;
		private static GameObject splat;
		private static GameFactory gameFactory;
		public GameObject penguin;
		private static GameObject tooltipPanel;

		private static System.Object locc = new System.Object();

		protected void Awake() {
			snap = new SnappingTool();
			var levelObj = GameObject.FindGameObjectWithTag(TagConstants.LEVELSETTINGS);
			if ( levelObj != null ) {
				levelSettings = levelObj.GetComponent<LevelSettings>();
			}
			tooltipPanel = GameObject.FindGameObjectWithTag(TagConstants.UI.TOOLTIP_PANEL);
			handler = gameObject.GetComponentInChildren<CouroutineDelegateHandler>();
			pickupFactory = new PickupFactory(handler, penguin);
			gameFactory = new GameFactory(handler);
			inputManager = GetComponent<InputManager>();
			gameStateManager = GetComponent<GameStateManager>();
			notifierSystem = GetComponent<NotifierSystem>();
			splat = (GameObject)Resources.Load("BloodSplatter/splatSpot");
		}

		protected void Start() {
			InitializeComponents();
			finished = true;
		}

		protected void OnDestroy() {
			lock ( locc ) {
				components.Clear();
			}
		}

		private static void InitializeComponents() {
			lock ( locc ) {
				while ( components.Count != 0 ) {
					var component = components.Dequeue();
					InitializeComponent(component);
					component.SetupComponents();
				}
			}
		}

		private static void InitializeComponent(GameEntity component) {
			switch ( component.GetTag() ) {
				case TagConstants.PENGUIN:
					new PlayerFactory(component.GetActionable<ControllableActions>(), component.GetGameObject(), levelSettings.gameObject, gameStateManager, notifierSystem, splat, handler).Build();
					break;
				case TagConstants.PLUTONIUM_PICKUP:
					pickupFactory.BuildPlutonium(component.GetActionable<PickupActions>());
					break;
				case TagConstants.PRESSURE_PLATE:
					new PressurePlateFactory(component.GetActionable<PressurePlateActions>()).BuildActionOnLinkingObject((LinkingComponent)component);
					break;
				case TagConstants.WIRE:
					TrapFactory.BuildWire(component.GetActionable<TrapActions>(), component.GetGameObject().GetComponent<Wire>(), handler);
					break;
				case TagConstants.WEIGHTBASED:
					TrapFactory.BuildWeightBasedTrap(component.GetActionable<TrapActions>(), component.GetGameObject());
					break;
				case TagConstants.CANVAS:
					gameFactory.BuildCanvas(component.GetActionable<GameActions>());
					break;
				case TagConstants.STAR1: case TagConstants.STAR2: case TagConstants.STAR3:
					gameFactory.BuildStar(component.GetActionable<GameActions>());
					break;
				case TagConstants.TOOLBUTTON:
					snap.SetCenter(levelSettings.GetSceneCenter());
					component.GetGameObject().GetComponent<SetSnappingTool>().SetSnap(snap);
					component.GetGameObject().GetComponent<SetSnappingTool>().SetInputManager(inputManager);
					component.GetGameObject().GetComponent<GameFrozenChecker>().SetGameStateManager(gameStateManager);
					break;
				case TagConstants.PENGUINEGG:
					pickupFactory.BuildEgg(component.GetActionable<PickupActions>(), component.GetGameObject());
					break;
				case TagConstants.SEAL_SPAWN:
					component.GetGameObject().GetComponent<SetSnappingTool>().SetInputManager(inputManager);
					break;
				case TagConstants.SEAL:
					new SealFactory(component.GetActionable<ControllableActions>(), component.GetGameObject(), splat).Build();
					break;
				case TagConstants.HATCHABLE_PENGUIN:
					pickupFactory.BuildHatchableEgg(component.GetActionable<PickupActions>());
					break;
				case TagConstants.JUMP:
					ToolFactory.BuildJump(component.GetActionable<ToolActions>(),
						component.GetGameObject().transform.parent.gameObject.GetComponentInChildren<Animator>());
					break;
				case TagConstants.CUTSCENE:
					break;
				case TagConstants.TOOLTIP:
					component.GetGameObject().GetComponent<Tooltip>().SetPanel(tooltipPanel);
					break;
				case TagConstants.UI.PENGUINSPEEDUPBUTTON:
					gameFactory.BuildSpeedButton(component.GetActionable<GameActions>());
					break;
				default:
					throw new NotImplementedException("Tag has no specific behaviour yet: <" + component.GetTag() + "> this does maybe not need to be registered");
			}
		}

		public static void Register(GameEntity component) {
			lock ( locc ) {
				components.Enqueue(component);
			}
		}

		public static void Redo() {
			lock ( locc ) {
				InitializeComponents();
			}
		}
	}
}
