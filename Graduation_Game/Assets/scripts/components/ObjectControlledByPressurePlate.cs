using UnityEngine;

namespace Assets.scripts.components{
	
	public abstract class ObjectControlledByPressurePlate : MonoBehaviour {

		/// <summary>
		/// Trigger the Object.
		/// </summary>
		public abstract void Trigger();
	}
}

