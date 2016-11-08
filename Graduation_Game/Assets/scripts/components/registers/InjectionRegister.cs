using System;
using System.Collections.Generic;
using Assets.scripts.components.factory;
using Assets.scripts.controllers;
using Assets.scripts.UI;
using UnityEngine;
using Assets.scripts.UI.screen.ingame;
using Assets.scripts.level;

namespace Assets.scripts.components.registers {
	public class InjectionRegister : MonoBehaviour {
		private static readonly List<GameEntity> components = new List<GameEntity>();
		private static bool finished;
		private static GameObject levelSettings;
		private static SnappingToolInterface snap;
	    private static InputManager inputManager;

		protected void Awake() {
			snap = new SnappingTool();
			levelSettings = GameObject.FindGameObjectWithTag(TagConstants.LEVELSETTINGS);
		    inputManager = GetComponent<InputManager>();
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
			foreach ( var component in components ) {
				InitializeComponent(component);
				component.SetupComponents();
			}
		}

		private static void InitializeComponent(GameEntity component) {
			switch ( component.GetTag() ) {
				case TagConstants.PENGUIN:
					new PlayerFactory(component, component.GetGameObject(), levelSettings).Build();
					break;
				case TagConstants.PLUTONIUM_PICKUP:
					new PickupFactory((Actionable<PickupActions>) component).Build();
					break;
				case TagConstants.PRESSURE_PLATE:
					new PressurePlateFactory((Actionable<PressurePlateActions>) component).BuildActionOnLinkingObject((LinkingComponent) component);
					break;
				case TagConstants.SNAPPING:
					component.GetGameObject().GetComponent<SetSnappingTool>().SetSnap(snap);
					snap.SetCenter(levelSettings.GetComponent<LevelSettings>().GetSceneCenter());
					break;
			default:
					throw new NotImplementedException("Tag has no specific behaviour yet: <" + component.GetTag() + "> this does maybe not need to be registered");
			}
		}

		public static void Redo() {
			if ( !finished ) {
				return;
			}
			InitializeComponents();
			components.Clear();
		}
	}
}
