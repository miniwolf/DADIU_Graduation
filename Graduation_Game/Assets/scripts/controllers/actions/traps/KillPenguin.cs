using Assets.scripts.components;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.traps {
	public class KillPenguin : Action {
		private Killable killable;

		public KillPenguin(Killable killable) {
			this.killable = killable;
		}

		public void Setup(GameObject gameObject) {
		}

		public void Execute() {
			var penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			penguinCounter.text = (int.Parse(penguinCounter.text) - 1).ToString();
			killable.Kill();
		}
	}
}
