using System;
using Assets.scripts.eggHatching;
using Assets.scripts.shop.item;
using Assets.scripts.UI.inventory;
using UnityEngine;
using Assets.scripts.character;
using System.Collections.Generic;

namespace Assets.scripts.controllers.actions.pickups {
	public class HatchEgg : Action {
		private GameObject go;
		private GameObject penguin;
		private Rigidbody[] eggShells;

		public HatchEgg(GameObject penguin){
			this.penguin = penguin;
		}

		public void Setup(GameObject gameObject) {
			go = gameObject;
		}

		public void Execute() {
			Inventory.penguinCount.SetValue(Inventory.penguinCount.GetValue() + 1);
			eggShells = go.GetComponentsInChildren<Rigidbody>();
			for (int i = 0; i < eggShells.Length; i++) {
				eggShells[i].isKinematic = false;
			}
			penguin.GetComponent<Penguin>().enabled = false;
			penguin = (GameObject)MonoBehaviour.Instantiate(penguin, go.transform.position, Quaternion.identity);
			penguin.transform.Rotate(new Vector3(0,90f,0));
			penguin.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
			penguin.GetComponentInChildren<Animator>().SetBool(AnimationConstants.CELEBRATE[UnityEngine.Random.Range(0, AnimationConstants.CELEBRATE.Length)], true);

			var penguinEgg = go.GetComponent<PenguinEgg>();
			penguinEgg.HatchTime = DateTime.Now.AddMinutes(30);
			penguinEgg.Hatchable = false;
		}
	}
}
