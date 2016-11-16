using System;
using System.Collections;
using Assets.scripts.components;
using Assets.scripts.controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.shop.item {
	[Serializable]
	public class PenguinEgg : ActionableGameEntityImpl<PickupActions> {
		public bool hatchable;
		public bool IsReady { get; set; }
		[SerializeField]
		public DateTime HatchTime { get; set; }
		public int shakeInterval = 2;

		private GameObject button;

		protected void Start() {
			button = GetComponentInChildren<Button>().gameObject;
			button.SetActive(false);
		}

		protected void Update() {
			if ( hatchable || DateTime.Now < HatchTime ) {
				return;
			}

			hatchable = true;
			button.SetActive(true);
			StartCoroutine(Hatchable());
		}

		private IEnumerator Hatchable() {
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

		public void HideButton() {
			button.SetActive(false);
		}
	}
}
