using System.Collections.Generic;
using UnityEngine;

using Assets.scripts.controllers.actions;

namespace Assets.scripts.controllers.handlers {
	public class ActionHandler : Handler {
		protected List<Action> actions = new List<Action>();

		public virtual void SetupComponents(GameObject obj) {
			foreach ( var action in actions ) {
				action.Setup(obj);
			}
		}

		public virtual void DoAction() {
			foreach ( var action in actions ) {
				action.Execute();
			}
		}

		public void AddAction(Action action) {
			actions.Add(action);
		}
	}
}
