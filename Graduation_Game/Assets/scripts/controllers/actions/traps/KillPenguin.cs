using Assets.scripts.components;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.components.registers;
using System.Collections.Generic;

namespace Assets.scripts.controllers.actions.traps {
	public class KillPenguin : Action {
		private readonly Killable killable;
	    private readonly NotifierSystem notifierSystem;
	    private GameObject penguin;
		private ParticleSystem ps;
		private string animation;

		public KillPenguin(Killable killable, NotifierSystem s, string animation) {
			this.killable = killable;
		    this.notifierSystem = s;
			this.animation = animation;
		}

		public void Setup(GameObject gameObject) {
			penguin = gameObject;
			ps = penguin.GetComponent<ParticleSystem>();
		}

		public void Execute() {
			string[] drown = AnimationConstants.DROWNDEATH;
			List<string> list = new List<string>();
			list.AddRange(drown);

			if ( !list.Contains(animation) ) {
				ps.Play();
			}
			var penguinCounter = GameObject.FindGameObjectWithTag(TagConstants.PENGUIN_COUNTER_TEXT).GetComponent<Text>();
			penguinCounter.text = (int.Parse(penguinCounter.text) - 1).ToString();
			killable.Kill();
			notifierSystem.PenguinDied(penguin);
			
			penguin.GetComponent<Collider>().enabled = false;
		}
	}
}
