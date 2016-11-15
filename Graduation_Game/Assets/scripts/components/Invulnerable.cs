using System;
using Assets.scripts.components.registers;
using UnityEngine;
using Assets.scripts.level;

namespace Assets.scripts.components {
	public class Invulnerable : Notifiable {
		void OnClick() {
			NotifierSystem notifierSystem = GameObject.FindGameObjectWithTag(TagConstants.NOTIFIER_SYSTEM).GetComponent<NotifierSystem>();
			notifierSystem.Register(NotifierSystem.Events.PenguinDied, this);
		}

		public void Notify(GameObject penguin) {
			// Handle penguin death
			// Find safe position

			PenguinSpawner spawner = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_SPAWNER).GetComponent<PenguinSpawner>();
			// Spawn new penguin
			spawner.SpawnPenguin();
		}
	}
}

