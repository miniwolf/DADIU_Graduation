using Assets.scripts.components;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.components.registers;

namespace Assets.scripts.controllers.actions.traps {
	public class KillPenguin : Action {
		private readonly Killable killable;
	    private readonly NotifierSystem notifierSystem;
	    private GameObject penguin;
		private ParticleSystem ps;

	    public KillPenguin(Killable killable, NotifierSystem s) {
			this.killable = killable;
		    this.notifierSystem = s;
		}

		public void Setup(GameObject gameObject) {
			penguin = gameObject;
			ps = penguin.GetComponent<ParticleSystem>();
		}

		public void Execute() {
			ps.Play();
			var penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			penguinCounter.text = (int.Parse(penguinCounter.text) - 1).ToString();
			killable.Kill();
			notifierSystem.PenguinDied(penguin);
			
			penguin.GetComponent<Collider>().enabled = false;
		}
	}
}
