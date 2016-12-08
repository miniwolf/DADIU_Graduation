using System.Collections.Generic;
using Assets.scripts.character;
using Assets.scripts.level;
using UnityEngine;

namespace Assets.scripts.controllers.actions.game {
	public class ResetPenguinSpeed : Action {
		private readonly List<Penguin> penguins = new List<Penguin>();
		private float origSpeed = 0.6f;
		private GameObject[] penguinSpawners;

		public void Setup (GameObject gameObject) {
			origSpeed = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_TEMPLATE).GetComponent<Penguin>().GetWalkSpeed();
			penguinSpawners = GameObject.FindGameObjectsWithTag(TagConstants.PENGUIN_SPAWNER);
		}

		public void Execute () {
			foreach ( var t1 in penguinSpawners ) {
				var penguinsGO = t1.GetComponent<PenguinSpawner>().GetAllPenguins();
				foreach ( var t in penguinsGO ) {
					penguins.Add(t.GetComponent<Penguin>());
				}
			}
			foreach (var penguin in penguins) {
				if ( penguin.IsDead() ) {
					continue;
				}
				penguin.SetWalkSpeed(origSpeed);
				if ( penguin.GetJump() ) {
					continue;
				}

				penguin.SetSpeedUp(false);
				penguin.SetSpeed(origSpeed);
			}
		}
	}
}
