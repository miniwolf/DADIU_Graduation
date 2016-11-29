using System;
using System.Linq;
using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.eggHatching {
	public class TimerCheck : MonoBehaviour {
		private Text text;

		protected void Start() {
			text = GetComponent<Text>();
		}

		protected void Update() {
			var eggTimerList = Inventory.eggHatchTime.GetValue().Split(',').ToList();
			var count = eggTimerList.FindAll(s => Convert.ToDateTime(s) < DateTime.Now).Count;
			if ( count > 0 ) {
				text.text = count.ToString();
			} else {
				var smallest = eggTimerList.Aggregate((a, b) => Convert.ToDateTime(a) < Convert.ToDateTime(b) ? a : b);
				var convertedSmallest = Convert.ToDateTime(smallest);
				text.text = convertedSmallest.Minute + ":" + convertedSmallest.Second;
			}
		}
	}
}
