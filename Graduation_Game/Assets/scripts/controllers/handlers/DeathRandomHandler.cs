using System.Collections.Generic;
using Assets.scripts.controllers.actions;
using UnityEngine;

namespace Assets.scripts.controllers.handlers {
	public class DeathRandomHandler : Handler {
		protected List<Action> actions = new List<Action>();

		public virtual void SetupComponents(GameObject obj) {
			foreach ( Action action in actions ) {
				action.Setup(obj);
			}
		}

		public virtual void DoAction() {
			var rnd = Random.Range(0, actions.Count);
			actions[rnd].Execute();
		}

		public void AddAction(Action action) {
			actions.Add(action);
		}
	}
}
