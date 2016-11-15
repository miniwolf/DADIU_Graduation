using System;
using System.Collections.Generic;
using Assets.scripts.components;
using UnityEngine;

namespace Assets.scripts.components.registers {
	public class NotifierSystem : MonoBehaviour {
		public enum Events { PenguinDied };

		private Dictionary<Events, List<Notifiable>> notifiers;

		public void Register(Events eve, Notifiable n) {
			notifiers[eve].Add(n);
		}

		public void PenguinDied(GameObject penguin) {
			foreach (var notifiable in notifiers[Events.PenguinDied]) {
				notifiable.Notify(penguin);
			}
		}
	}
}

