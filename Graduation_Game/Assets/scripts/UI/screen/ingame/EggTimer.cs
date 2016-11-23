using System;
using Assets.scripts.eggHatching;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.screen.ingame {
	public class EggTimer : MonoBehaviour {
		private PenguinEgg egg;
		private Text text;

		protected void Start() {
			//var childObject = transform.GetChild(1);

			//text = childObject.GetComponent<TextMesh>();
			text = GameObject.FindGameObjectWithTag("EggTimer").GetComponent<Text>();
			//childObject.GetComponent<MeshRenderer>().enabled = true;
		}

		protected void Update() {
			var span = egg.HatchTime.Subtract(DateTime.Now);
			text.text = "Next egg ready for hatching: " +span.Minutes + " : " + span.Seconds;
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
