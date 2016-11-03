using System.Collections.Generic;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using Assets.scripts.controllers;
using Assets.scripts.controllers.handlers;
using UnityEngine;

namespace Assets.scripts.character {
	public class Penguin : ActionableGameEntityImpl<ControllableActions>, Directionable {
		public enum Lane {Left, Right};

		private Vector3 direction;
		private Lane lane = Lane.Left;

		void Start() {
			direction = new Vector3(1,0,0);
		}

		void Update() {
			ExecuteAction(ControllableActions.Move);
		}
		public override string GetTag() {
			return TagConstants.PLAYER;
		}

		public Vector3 GetDirection() {
			return direction;
		}

		public void SetDirection(Vector3 direction) {
			this.direction = direction;
		}

		public Lane GetLane() {
			return lane;
		}

		public void SetLane(Lane lane) {
			this.lane = lane;
		}
	}
}
