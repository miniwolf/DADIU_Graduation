using System;
using System.Collections;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using Assets.scripts.controllers;
using Assets.scripts.UI.screen.ingame;
using UnityEngine;

namespace Assets.scripts.shop.item {
	[Serializable]
	public class PenguinEgg : ActionableGameEntityImpl<PickupActions> {
		public bool Hatchable { get; set; }
		public bool IsReady { get; set; }
		[SerializeField]
		public DateTime HatchTime { get; set; }
		public int shakeInterval = 2;

		public int Idx { get; set; }
		public EggTimer Timer { get; set; }

		protected void Update() {
			if ( Hatchable || DateTime.Now < HatchTime ) {
				return;
			}

			Hatchable = true;
			Destroy(Timer);
			Destroy(transform.GetChild(0).gameObject);
			var penguin = gameObject.AddComponent<HatchablePenguin>();
			penguin.SetEgg(this);
			InjectionRegister.Redo();
			StartCoroutine(Hatch());
		}

		private IEnumerator Hatch() {
			while ( true ) {
				ExecuteAction(PickupActions.ShakeEgg);
				yield return new WaitForSeconds(shakeInterval);
			}
		}

		public void HatchEgg() {
			ExecuteAction(PickupActions.HatchEgg);
		}

		public override string GetTag() {
			return TagConstants.PENGUINEGG;
		}
	}
}
