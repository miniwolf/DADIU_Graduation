using System;
using System.Globalization;
using Assets.scripts.shop.item;
using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.screen.ingame {
	public class EggTimer : MonoBehaviour {
		private readonly Item<string> eggHatchTime = Inventory.eggHatchTime;
		public static PenguinEgg egg;
		private Text text;

		protected void Start() {
			text = GetComponent<Text>();
			if ( egg != null ) {
				return;
			}

			egg = GetComponentInChildren<PenguinEgg>();
			egg.IsReady = false;
			egg.hatchable = false;

			var stringTime = eggHatchTime.GetValue();
			egg.HatchTime = stringTime == null || "".Equals(stringTime)
				? DateTime.Now.AddMinutes(1)
				: Convert.ToDateTime(stringTime);

			eggHatchTime.SetValue(egg.HatchTime.ToString(CultureInfo.CurrentCulture));
		}

		protected void Update() {
			var span = egg.HatchTime.Subtract(DateTime.Now);
			text.text = span.Minutes + " : " + span.Seconds;
		}
	}
}
