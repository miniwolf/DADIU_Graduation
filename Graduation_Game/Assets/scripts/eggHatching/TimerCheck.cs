using System;
using System.Linq;
using Assets.scripts.UI.inventory;

namespace Assets.scripts.eggHatching {
	public class TimerCheck {
		protected void Update() {
			var exists = Inventory.eggHatchTime.GetValue().Split(',').ToList().Exists(s => Convert.ToDateTime(s) < DateTime.Now);
			if ( exists ) {

			}
		}
	}
}