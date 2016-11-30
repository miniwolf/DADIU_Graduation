using System;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using Assets.scripts.controllers;
using Assets.scripts.shop.item;
using Assets.scripts.UI.screen.ingame;

namespace Assets.scripts.eggHatching {
	public class PenguinEgg : ActionableGameEntityImpl<PickupActions> {
		public bool Hatchable { get; set; }
		public bool IsReady { get; set; }
		public int shakeInterval = 2;
		public int Idx { get; set; }
		public DateTime HatchTime { get; set; }
		public EggTimer Timer { get; set; }

		protected void Update() {
			if ( Hatchable || DateTime.Now < HatchTime ) {
				return;
			}

			Hatchable = true;
			Destroy(Timer); // TODO: Might not be needed as it should be deleted below
			Destroy(transform.GetChild(1).gameObject);
			//var penguin = gameObject.AddComponent<HatchablePenguin>();
			//penguin.SetEgg(this);
			InjectionRegister.Redo();
			ExecuteAction(PickupActions.ShakeEgg);
		}

		public void HatchEgg() {
			ExecuteAction(PickupActions.HatchEgg);
		}

		public override string GetTag() {
			return TagConstants.PENGUINEGG;
		}
	}
}
