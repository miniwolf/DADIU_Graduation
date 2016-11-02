﻿using System.Collections.Generic;
using Assets.scripts.controllers.actions;
using UnityEngine;

namespace Assets.scripts.controllers.handlers {
	public class ActionHandler : Handler {
		protected List<Action> actions = new List<Action>();

		public virtual void SetupComponents(GameObject obj) {
			foreach ( Action action in actions ) {
				action.Setup(obj);
			}
		}

		public virtual void DoAction() {
			foreach ( Action action in actions ) {
				action.Execute();
			}
		}

		public void AddAction(Action action) {
			actions.Add(action);
		}
	}
}
