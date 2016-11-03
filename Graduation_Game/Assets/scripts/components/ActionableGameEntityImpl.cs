using System.Collections.Generic;
using Assets.scripts.components.registers;
using Assets.scripts.controllers.handlers;
using UnityEngine;

namespace Assets.scripts.components {
	public abstract class ActionableGameEntityImpl<T> : MonoBehaviour, Actionable<T>, GameEntity {
		private readonly Dictionary<T, Handler> actions = new Dictionary<T, Handler>();

		// Use this for initialization
		protected void Awake() {
			InjectionRegister.Register(this);
		}

		public void AddAction(T actionName, Handler action) {
			actions.Add(actionName, action);
		}

		public void ExecuteAction(T actionName) {
			if ( actions.ContainsKey(actionName) ) {
				actions[actionName].DoAction();
			} else {
				Debug.Log("Cannot execute action " + actionName + " on " + this);
			}
		}

		public abstract string GetTag();

		public void SetupComponents() {
			foreach (var handlers in actions.Values) {
				handlers.SetupComponents(gameObject);
			}
		}
	}
}
