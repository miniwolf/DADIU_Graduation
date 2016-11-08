using UnityEngine;

namespace Assets.scripts.components {
	public interface LinkingComponent {

		/// <summary>
		/// Gets the linking object.
		/// </summary>
		/// <returns>The linking object.</returns>
		ObjectControlledByPressurePlate GetLinkingObject();
	}
}
