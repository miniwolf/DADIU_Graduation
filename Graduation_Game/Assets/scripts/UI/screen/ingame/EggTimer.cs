using System;
using Assets.scripts.shop.item;
using UnityEngine;

namespace Assets.scripts.UI.screen.ingame {
	public class EggTimer : MonoBehaviour {
		private PenguinEgg egg;
		private TextMesh text;

		protected void Start() {
			var childObject = transform.GetChild(0);

			text = childObject.GetComponent<TextMesh>();
			childObject.GetComponent<MeshRenderer>().enabled = true;
		}

		protected void Update() {
			var span = egg.HatchTime.Subtract(DateTime.Now);
			text.text = span.Minutes + " : " + span.Seconds;
		}

		public void SetEgg(PenguinEgg egg) {
			this.egg = egg;
			egg.IsReady = false;
			egg.Hatchable = false;
		}

		/// <summary>
		/// Assumes SetEgg has been called before this
		/// </summary>
		/// <param name="hatchTime">string representation of a DateTime</param>
		public void SetTimer(string hatchTime) {
			egg.HatchTime = Convert.ToDateTime(hatchTime);
		}
	}
}
