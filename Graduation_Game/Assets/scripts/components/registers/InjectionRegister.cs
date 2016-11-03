using System.Collections;
using System.Collections.Generic;
using Assets.scripts.components.factory;
using Assets.scripts.controllers;
using UnityEngine;

namespace Assets.scripts.components.registers {
	public class InjectionRegister : MonoBehaviour {
		private static readonly List<GameEntity> components = new List<GameEntity>();
		private static GameObject levelSettings;

		protected void Awake() {
			levelSettings = GameObject.FindGameObjectWithTag(TagConstants.LEVELSETTINGS);
		}

		protected void Start() {
			InitializeComponents();
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
				case TagConstants.PLAYER:
					new PlayerFactory((Actionable<ControllableActions>) component, levelSettings).Build();
					break;
				case TagConstants.PLUTONIUM_PICKUP:
					new PickupFactory((Actionable<PickupActions>) component, levelSettings).Build();
					break;
			}
		}
	}
}