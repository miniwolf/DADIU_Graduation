using System.Linq;
using System.Collections.Generic;
using Assets.scripts.UI.inventory;
using Assets.scripts.UI.screen.ingame;
using UnityEngine;

namespace Assets.scripts.eggHatching {
	public class EggCollection : MonoBehaviour {
		private readonly List<GameObject> eggs = new List<GameObject>();
		private int idx;

		protected void Start() {
			GetComponentsInChildren<Transform>().ToList()
				.Where(s => s.tag == TagConstants.PENGUINEGG).ToList()
				.ForEach(egg => eggs.Add(egg.gameObject));

			AddAndStartTimers();
		}

		private void AddAndStartTimers() {
			Inventory.eggHatchTime.GetValue().Split(',').ToList().ForEach(AddEgg);
		}

		public void AddEgg(string eggTime) {
			var nextEgg = eggs[idx++];
			var timer = nextEgg.AddComponent<EggTimer>();
			var egg = nextEgg.AddComponent<PenguinEgg>();

			egg.Timer = timer;
			timer.SetEgg(egg);
			timer.SetTimer(eggTime);
		}
	}
}
